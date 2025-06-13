using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AsyncLibrary;
using System.Net.Mail;
using System.Data;
using System.Net;

namespace EmailEventAlert
{
    public class Main
    {
        private static Database DB = new Database("spa_portal");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        public static void OnExecute(string[] Args)
        {
            logger.Info("EmailEventAlert: Starting");

            execution_id = Args[0];
            logger.Info("EmailEventAlert: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("EmailEventAlert: Context=" + context);

            string EventName = utils.GetParameter(context, "EventName");

            GetEventDetails(EventName);

            logger.Info("EmailEventAlert: Ending");
        }

        private static void GetEventDetails(string eventName)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event", typeof(string), eventName));

            string SQL = @"
SELECT          e.*, a.event_id, a.event_details, a.client_id, c.event_category_name 
FROM            sys_event_type e
JOIN            sys_event_category c
ON              e.event_category_id = c.event_category_id
JOIN            sys_event a
ON              e.event_type_id = a.event_type_id
AND             a.email_date is null
WHERE           e.event_name = @event
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                EmailSubscribers(eventName, ds.Tables[0].Rows[x]["event_type_id"].ToString(), ds.Tables[0].Rows[x]["event_id"].ToString(), ds.Tables[0].Rows[x]["event_details"].ToString(), ds.Tables[0].Rows[x]["client_id"].ToString());
                SetEventEmailDate(ds.Tables[0].Rows[x]["event_id"].ToString());
            }
        }

        private static void SetEventEmailDate(string event_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_id", typeof(string), event_id));

            string SQL = @"
UPDATE          sys_event
SET             email_date = getdate()
WHERE           event_id = @event_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public static void EmailSubscribers(string event_name, string eventTypeID, string event_id, string event_details, string client_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_type_id", typeof(string), eventTypeID));


            string SQL = @"
SELECT      *
FROM        sys_event_subscription s
JOIN        sys_user u
ON          s.user_id = u.user_id
WHERE       event_type_id = @event_type_id
";

            DataSet ds = DB.GetDataSet(SQL, myParams);

            logger.Info("EmailEventAlert: Emails to Send =" + ds.Tables[0].Rows.Count.ToString());

            async.Notify(execution_id, "Emails to Send =" + ds.Tables[0].Rows.Count.ToString());

            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                SendMail(ds.Tables[0].Rows[x]["email"].ToString(), event_name, event_details);
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
