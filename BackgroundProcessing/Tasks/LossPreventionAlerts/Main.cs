using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncLibrary;
using System.Collections;

namespace LossPreventionAlerts
{
    public class Main
    {
        private static Database portalDB = new Database("spa_portal");
        private static Database customDB = new Database("spa_custom");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        private static string pEventNames = string.Empty;
        private static string pClientID = string.Empty;

        private static int pDaysDeliquent = 5;
        private static int pNoSalesThreshold = 0;
        private static Decimal pDriveOffThreshold = (Decimal)0.00;
        private static Decimal pRefundThreshold = (Decimal)10.00;
        private static Decimal pOverShortThreshold = (Decimal)3.00;
        private static Decimal pCancelThreshold = (Decimal)0.00;
        

        public static void OnExecute(string[] Args)
        {
            logger.Info("Starting");

            execution_id = Args[0];
            logger.Info("Execution_id=" + execution_id);

            string context = async.GetJobContext(Args[0]);
            logger.Info("Context=" + context);

            pEventNames = utils.GetParameter(context, "EventNames");
            pClientID = utils.GetParameter(context, "ClientID");

            String[] eventNames = pEventNames.Split('&');

            foreach (string eventName in eventNames)
            {
                async.Notify(execution_id, "Alert " + eventName);
                switch (eventName)
                {
                    case "Delinquent Business Units":
                        async.Notify(execution_id, "Event Name " + eventName);
                        try
                        {
                            pDaysDeliquent = int.Parse(utils.GetParameter(context, "DaysDeliquent"));
                        }
                        catch { };

                        alertDelinquentBU(pDaysDeliquent, pClientID);
                        break;

                    case "Drive Offs":
                        async.Notify(execution_id, "Event Name " + eventName);
                        try
                        {
                            pDriveOffThreshold = Decimal.Parse(utils.GetParameter(context, "DriveOffThreshold"));
                        }
                        catch { };

                        alertDriveOff(pDriveOffThreshold, pClientID);
                        break;

                    case "No Sales":
                        async.Notify(execution_id, "Event Name " + eventName);
                        try
                        {
                            pNoSalesThreshold = int.Parse(utils.GetParameter(context, "NoSalesThreshold"));
                        }
                        catch { };

                        alertNoSale(pNoSalesThreshold, pClientID);
                        break;

                    case "Refunds":
                        async.Notify(execution_id, "Event Name " + eventName);
                        try
                        {
                            pRefundThreshold = Decimal.Parse(utils.GetParameter(context, "RefundThreshold"));
                        }
                        catch { };

                        alertRefunds(pRefundThreshold, pClientID);
                        break;

                    case "Shift Over/Short":
                        async.Notify(execution_id, "Event Name " + eventName);
                        try
                        {
                            pOverShortThreshold = Decimal.Parse(utils.GetParameter(context, "OverShortThreshold"));
                        }
                        catch { };

                        alertOverShort(pOverShortThreshold, pClientID);
                        break;

                    case "Transaction Cancels":
                        async.Notify(execution_id, "Event Name " + eventName);
                        try
                        {
                            pCancelThreshold = Decimal.Parse(utils.GetParameter(context, "CancelThreshold"));
                        }
                        catch { };

                        alertCancels(pCancelThreshold, pClientID);
                        break;

                    default:
                        async.Notify(execution_id, "Unknown Event Name " + eventName);
                        break;

                }

            }

            logger.Info("Ending");
        }

        private static void alertDelinquentBU(int daysDelinquent, string clientID)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(customDB.CreateParameter("days_delinquent", typeof(int), daysDelinquent));
            myParams.Add(customDB.CreateParameter("client_id", typeof(string), clientID));

            string SQL = @"
DECLARE     @date  as datetime
SET         @date = DATEADD(dd, DATEDIFF(dd, @day_deliquent, getdate()), 0)

SELECT		bu_name, bu_date 
FROM		custom_shifts
WHERE		eod_time is null
AND			bu_date < @date
GROUP BY	bu_name, bu_date 
";

            customDB.ExecuteSQL(SQL, myParams);
        }

        private static void alertDriveOff(Decimal driveOffThreshold, string clientID)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(customDB.CreateParameter("driveOff_Threshold", typeof(int), driveOffThreshold));
            myParams.Add(customDB.CreateParameter("client_id", typeof(string), clientID));

            string SQL = @"
DECLARE     @date  as datetime
SET         @date = DATEADD(dd, DATEDIFF(dd, 1, getdate()), 0)

SELECT		bu_name, bu_date, SUM(gross_amt) as drive_off_amt
FROM		custom_sales_lines
WHERE		sales_type_id = 6
AND			bu_date = @date
GROUP BY	bu_name, bu_date
HAVING		SUM(gross_amt) > @driveOff_Threshold
";

            customDB.ExecuteSQL(SQL, myParams);
        }
        
        private static void alertNoSale(int noSalesThreshold, string clientID)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(customDB.CreateParameter("noSales_Threshold", typeof(int), noSalesThreshold));
            myParams.Add(customDB.CreateParameter("client_id", typeof(string), clientID));

            string SQL = @"
DECLARE     @date  as datetime
SET         @date = DATEADD(dd, DATEDIFF(dd, 1, getdate()), 0)

SELECT		bu_name, bu_date, SUM(no_sale_qty) as no_sale 
FROM		custom_shifts
WHERE		bu_date = @date
GROUP BY	bu_name, bu_date 
HAVING		SUM(no_sale_qty) > @noSales_Threshold
";

            customDB.ExecuteSQL(SQL, myParams);
        }
        
        private static void alertRefunds(Decimal refundThreshold, string clientID)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(customDB.CreateParameter("refund_Threshold", typeof(int), refundThreshold));
            myParams.Add(customDB.CreateParameter("client_id", typeof(string), clientID));

            string SQL = @"
DECLARE     @date  as datetime
SET         @date = DATEADD(dd, DATEDIFF(dd, 1, getdate()), 0)

SELECT		bu_name, bu_date, SUM(refund_amt) as refund_amt 
FROM		custom_shifts
WHERE		bu_date = @date
GROUP BY	bu_name, bu_date 
HAVING		SUM(refund_amt) >  @refund_Threshold
";

            customDB.ExecuteSQL(SQL, myParams);
        }
        
        private static void alertOverShort(Decimal overShortThreshold, string clientID)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(customDB.CreateParameter("overShort_Threshold", typeof(int), overShortThreshold));
            myParams.Add(customDB.CreateParameter("client_id", typeof(string), clientID));

            string SQL = @"
DECLARE     @date  as datetime
SET         @date = DATEADD(dd, DATEDIFF(dd, 1, getdate()), 0)

SELECT		bu_name, bu_date, employee_name, SUM(over_short_amt) as over_short_amt 
FROM		custom_shifts
WHERE		bu_date = @date
GROUP BY	bu_name, bu_date, employee_name 
HAVING		ABS(SUM(over_short_amt)) > @overShort_Threshold
";

            customDB.ExecuteSQL(SQL, myParams);
        }

        private static void alertCancels(Decimal cancelThreshold, string clientID)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(customDB.CreateParameter("cancel_Threshold", typeof(int), cancelThreshold));
            myParams.Add(customDB.CreateParameter("client_id", typeof(string), clientID));

            string SQL = @"
DECLARE     @date  as datetime
SET         @date = DATEADD(dd, DATEDIFF(dd, 1, getdate()), 0)

SELECT		bu_name, bu_date, SUM(trans_cancel_amt) as trans_cancel_amt 
FROM		custom_shifts
WHERE		bu_date = @date
GROUP BY	bu_name, bu_date
HAVING		SUM(trans_cancel_amt) > @cancel_Threshold
";

            customDB.ExecuteSQL(SQL, myParams);
        }
    }
}
