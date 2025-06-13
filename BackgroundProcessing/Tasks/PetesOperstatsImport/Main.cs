using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Collections;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using AsyncLibrary;
using System.Xml;
using System.IO;

namespace PetesOperstatsImport
{
    public class Main
    {
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        private static string pFTPResult = string.Empty;

        public static void OnExecute(string[] Args)
        {
            logger.Info("PetesOperstatsImport: Starting");

            execution_id = Args[0];
            logger.Info("PetesOperstatsImport: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("PetesOperstatsImport: Context=" + context);

            string pFTPResult = utils.GetParameter(context, "RetrievedFiles");

            String[] files = pFTPResult.Split('&');

            foreach (string file in files)
            {
                async.Notify(execution_id, "Importing file " + file);
                ImportData(file);
            }

            logger.Info("PetesOperstatsImport: Ending");
        }

        private static void ImportData(string file)
        {
            logger.Info("PetesOperstatsImport: File=" + file);

            int count = 0;
            using (StreamReader sr = File.OpenText(file))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] row = s.Split('|');

                    shift oShift = new shift();
                    
                    oShift.bu_id = int.Parse(row[0]);
                    oShift.bu_date = DateTime.Parse(row[1]);
                    oShift.employee_id = int.Parse(row[2]);
                    oShift.shift_id = int.Parse(row[3]);
                    oShift.drawer_id = int.Parse(row[4]);

                    try
                    {
                        oShift.shift_open_time = DateTime.Parse(row[5]);
                    }
                    catch { };
                    try
                    {
                        oShift.shift_close_time = DateTime.Parse(row[6]);
                    }
                    catch { };

                    oShift.shift_status_code = row[7];

                    try
                    {
                        oShift.open_balance_amt = Decimal.Parse(row[8]);
                    }
                    catch { };
                    try
                    {
                        oShift.net_trans_qty = Decimal.Parse(row[9]);
                    }
                    catch { };
                    try
                    {
                        oShift.gross_sold_amt = Decimal.Parse(row[10]);
                    }
                    catch { };
                    try
                    {
                        oShift.adj_system_net_sold_amt = Decimal.Parse(row[11]);
                    }
                    catch { };
                    try
                    {
                        oShift.over_short_amt = Decimal.Parse(row[12]);
                    }
                    catch { };
                    try
                    {
                        oShift.system_net_sold_amt = Decimal.Parse(row[13]);
                    }
                    catch { };
                    try
                    {
                        oShift.net_discount_qty = Decimal.Parse(row[14]);
                    }
                    catch { };
                    try
                    {
                        oShift.net_discount_amt = Decimal.Parse(row[15]);
                    }
                    catch { };
                    try
                    {
                        oShift.net_coupon_qty = Decimal.Parse(row[16]);
                    }
                    catch { };
                    try
                    {
                        oShift.net_coupon_amt = Decimal.Parse(row[17]);
                    }
                    catch { };
                    try
                    {
                        oShift.no_sale_qty = Decimal.Parse(row[18]);
                    }
                    catch { };
                    try
                    {
                        oShift.refund_amt = Decimal.Parse(row[19]);
                    }
                    catch { };
                    try
                    {
                        oShift.payout_amt = Decimal.Parse(row[20]);
                    }
                    catch { };
                    try
                    {
                        oShift.item_cancel_qty = Decimal.Parse(row[21]);
                    }
                    catch { };
                    try
                    {
                        oShift.item_cancel_amt = Decimal.Parse(row[22]);
                    }
                    catch { };
                    try
                    {
                        oShift.trans_cancel_qty = Decimal.Parse(row[23]);
                    }
                    catch { };
                    try
                    {
                        oShift.trans_cancel_amt = Decimal.Parse(row[24]);
                    }
                    catch { };

                    oShift.employee_name = row[25];
                    oShift.bu_name = row[26];
                    try
                    {
                        oShift.eod_time = DateTime.Parse(row[27]);
                    }
                    catch { };

                    oShift.Save();

                    count++;
                }
            }      

            async.Notify(execution_id, "Rows Imported = " + count.ToString());
        }
    }
}
