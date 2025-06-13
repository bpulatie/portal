using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using AsyncLibrary;

namespace MetricAlert
{
    public class Main
    {
        private static Database portalDB = new Database("spa_portal");
        private static Database metricDB = new Database("spa_metric");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        private static string eventName = string.Empty;
        private static string eventTypeID = string.Empty;
        private static string eventCategoryID = string.Empty;
        private static string eventCategoryName = string.Empty;

        private static string pEventName = string.Empty;
        private static int pLookBack_Days = 5;
        private static int pAverage_Days = 30;
        private static int pThreshold_Percentage = 50;
        private static int pDimension_Level = 2;
        private static string pClientID = string.Empty;

        public static void OnExecute(string[] Args)
        {
            logger.Info("MurphyAlert: Starting");

            execution_id = Args[0];
            logger.Info("MurphyAlert: Execution_id=" + execution_id);

            string context = async.GetJobContext(Args[0]);
            logger.Info("MurphyAlert: Context=" + context);

            pEventName = utils.GetParameter(context, "EventName");
            pLookBack_Days = int.Parse(utils.GetParameter(context, "LookBack_Days"));
            pAverage_Days = int.Parse(utils.GetParameter(context, "Average_Days"));
            pThreshold_Percentage = int.Parse(utils.GetParameter(context, "Threshold_Percentage"));
            pDimension_Level = int.Parse(utils.GetParameter(context, "Dimension_Level"));
            pClientID = utils.GetParameter(context, "ClientID");

            GetEventDetails(pEventName);

            ExecuteQueries(pLookBack_Days, pAverage_Days, pThreshold_Percentage, pDimension_Level, pClientID);

            logger.Info("MurphyAlert: Ending");
        }

        private static void GetEventDetails(string event_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(portalDB.CreateParameter("event", typeof(string), event_name));

            string SQL = @"
SELECT          e.*, c.event_category_name 
FROM            sys_event_type e
JOIN            sys_event_category c
ON              e.event_category_id = c.event_category_id
WHERE           e.event_name = @event
";

            DataSet ds = portalDB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count == 1)
            {
                eventTypeID = ds.Tables[0].Rows[0]["event_type_id"].ToString();
                eventCategoryID = ds.Tables[0].Rows[0]["event_category_id"].ToString();
                eventCategoryName = ds.Tables[0].Rows[0]["event_category_name"].ToString();
            }
            else
            {
                throw new Exception("Event not configured: " + eventName);
            }

            logger.Info("MurphyAlert: eventTypeID=" + eventTypeID);
            logger.Info("MurphyAlert: eventCategoryID=" + eventCategoryID);
            logger.Info("MurphyAlert: eventCategoryName=" + eventCategoryName);

        }


        private static void ExecuteQueries(int LookBack, int AverageDays, int Threshold, int dimension_level, string client_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(metricDB.CreateParameter("lookback_days", typeof(int), LookBack));
            myParams.Add(metricDB.CreateParameter("average_days", typeof(int), AverageDays));
            myParams.Add(metricDB.CreateParameter("factor", typeof(int), Threshold));

            string SQL = @"
DECLARE     @date  as datetime
DECLARE		@lookback_date as datetime
DECLARE		@average_date as datetime
DECLARE		@yesterday as datetime

SET         @date = DATEADD(dd, DATEDIFF(dd, 0, getdate()), 0)
SET			@lookback_date = DATEADD(dd, -1 * @lookback_days, @date) 
SET			@average_date = DATEADD(dd, -1 * @average_days, @date) 
SET			@yesterday = DATEADD(dd, -2, @date) 

SELECT		dimension_1_name as store, business_date, DATENAME(DW, business_date) as DOW, avg_qty, quantity
FROM		(
    			SELECT		m.metric_id, m.client_id,  m.business_date, a.avg_qty, (a.avg_qty/100 * @factor) as threshold, sum(m.value_1) as quantity
";

            if (dimension_level > 0)
            {
                SQL += @"   ,m.dimension_1_id, m.dimension_1_name
";
            }

            if (dimension_level > 1)
            {
                SQL += @"   ,m.dimension_2_id, m.dimension_2_name
";
            }

            if (dimension_level > 2)
            {
                SQL += @"   ,m.dimension_3_id, m.dimension_3_name
";
            }

            SQL += @"
				FROM          metric m
				JOIN          (
									 SELECT			metric_id, client_id, DATENAME(DW, business_date) AS Day, AVG(value_1) AS avg_qty 
";

            if (dimension_level > 0)
            {
                SQL += @"   ,dimension_1_id
";
            }

            if (dimension_level > 1)
            {
                SQL += @"   ,dimension_2_id
";
            }

            if (dimension_level > 2)
            {
                SQL += @"   ,dimension_3_id
";
            }

            SQL += @"
									 FROM			metric
									 WHERE			business_date > @average_date
									 GROUP BY		metric_id, client_id, DATENAME(DW, business_date)
";

            if (dimension_level > 0)
            {
                SQL += @"   ,dimension_1_id
";
            }

            if (dimension_level > 1)
            {
                SQL += @"   ,dimension_2_id
";
            }

            if (dimension_level > 2)
            {
                SQL += @"   ,dimension_3_id
";
            }

            SQL += @"
								) a
				ON          m.metric_id = a.metric_id
				AND			m.client_id = a.client_id
				AND			DATENAME(DW, m.business_date) = a.Day
";

            if (dimension_level > 0)
            {
                SQL += @"   AND m.dimension_1_id = a.dimension_1_id
";
            }

            if (dimension_level > 1)
            {
                SQL += @"   AND m.dimension_2_id = a.dimension_2_id
";
            }

            if (dimension_level > 2)
            {
                SQL += @"   AND m.dimension_3_id = a.dimension_3_id
";
            }

            SQL += @"
				WHERE		m.business_date between @lookback_date and @yesterday
				GROUP BY	m.metric_id, m.client_id,  m.business_date, a.avg_qty
";

            if (dimension_level > 0)
            {
                SQL += @"   ,m.dimension_1_id, m.dimension_1_name
";
            }

            if (dimension_level > 1)
            {
                SQL += @"   ,m.dimension_2_id, m.dimension_2_name
";
            }

            if (dimension_level > 2)
            {
                SQL += @"   ,m.dimension_3_id, m.dimension_3_name
";
            }

            SQL += @"
			) r
WHERE         quantity < threshold
ORDER BY	dimension_1_name, business_date
";

            logger.Info("MurphyAlert: SQL = " + SQL);
            
            DataSet ds = metricDB.GetDataSet(SQL, myParams);

            logger.Info("MurphyAlert: Rows found=" + ds.Tables[0].Rows.Count.ToString());

            string event_details = string.Empty;
            string event_summary = string.Empty;

            event_details = "<h4>" + eventName + "</h4>";
            event_details += "<div>Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "</div>";
            event_details += "<br/>";
            event_details += "<table width='100%'>";
            event_details += @"<tr>
                                    <th style='text-align:left;'>Store No</th>
                                    <th style='text-align:left;'>Date</th>
                                    <th style='text-align:left;'>Day of Week</th>
                                    <th style='text-align:left;'>Avg Trx Count</th>
                                    <th style='text-align:left;'>Actual Count</th>
                              </tr>
";

            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                event_details += "<tr>";
                event_details += "<td>" + ds.Tables[0].Rows[x]["store"].ToString() + "</td>";
                event_details += "<td>" + ds.Tables[0].Rows[x]["business_date"].ToString() + "</td>";
                event_details += "<td>" + ds.Tables[0].Rows[x]["DOW"].ToString() + "</td>";
                event_details += "<td>" + ds.Tables[0].Rows[x]["avg_qty"].ToString() + "</td>";
                event_details += "<td>" + ds.Tables[0].Rows[x]["quantity"].ToString() + "</td>";
                event_details += "</tr>";
            }

            event_details += "</table>";

            async.Notify(execution_id, "Stores with Missing Data = " + ds.Tables[0].Rows.Count.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                event_summary = "Stores with Missing Data = " + ds.Tables[0].Rows.Count.ToString();

                myParams.Add(portalDB.CreateParameter("client_id", typeof(string), client_id));
                myParams.Add(portalDB.CreateParameter("event_details", typeof(string), event_details));
                myParams.Add(portalDB.CreateParameter("event_summary", typeof(string), event_summary));
                myParams.Add(portalDB.CreateParameter("event_type_id", typeof(string), eventTypeID));
                myParams.Add(portalDB.CreateParameter("event_category_id", typeof(string), eventCategoryID));
                myParams.Add(portalDB.CreateParameter("event_category_name", typeof(string), eventCategoryName));

                SQL = @"
INSERT into sys_event
        (   event_id, 
            event_date, 
            event_category,       
            event_type, 
            event_details,  
            event_summary,  
            creation_user_id, 
            creation_date, 
            modified_user_id, 
            modified_date, 
            client_id,  
            event_category_id,  
            event_type_id
        )
VALUES  (
            newid(),  
            getdate(),  
            @event_category_name, 
            'i',        
            @event_details, 
            @event_summary, 
            null,             
            getdate(),     
            null,             
            getdate(),     
            @client_id, 
            @event_category_id, 
            @event_type_id
        )
";

                portalDB.ExecuteSQL(SQL, myParams);

               // EmailSubscribers(eventName, event_details, client_id);
            }

        }

        public static void EmailSubscribers(string event_category, string event_details, string client_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(portalDB.CreateParameter("event_type_id", typeof(string), eventTypeID));


            string SQL = @"
SELECT      *
FROM        sys_event_subscription s
JOIN        sys_user u
ON          s.user_id = u.user_id
WHERE       event_type_id = @event_type_id
";

            DataSet ds = portalDB.GetDataSet(SQL, myParams);

            logger.Info("MurphyAlert: Emails to Send =" + ds.Tables[0].Rows.Count.ToString());

            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                SendMail(ds.Tables[0].Rows[x]["email"].ToString(), event_category, event_details);
            }

        }

        public static void SendMail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("postmaster@virtuallyracing.com", "Do Not Reply");
            mail.To.Add(new MailAddress(email));

            //set the content 
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("mail.virtuallyracing.com");

            NetworkCredential Credentials = new NetworkCredential("postmaster@virtuallyracing.com", "Kirstie@92");

            smtp.Credentials = Credentials;
            smtp.Port = 587;
            smtp.Send(mail);
        }

    }
}
