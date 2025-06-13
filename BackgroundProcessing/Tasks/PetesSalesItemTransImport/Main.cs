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

namespace PetesSalesItemTransImport
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
            logger.Info("PetesSalesItemTransImport: Starting");

            execution_id = Args[0];
            logger.Info("v: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("PetesSalesItemTransImport: Context=" + context);

            string pFTPResult = utils.GetParameter(context, "RetrievedFiles");

            String[] files = pFTPResult.Split('&');

            foreach (string file in files)
            {
                async.Notify(execution_id, "Importing file " + file);
                ImportData(file);
            }

            logger.Info("PetesSalesItemTransImport: Ending");
        }

        private static void ImportData(string file)
        {
            logger.Info("PetesSalesItemTransImport: File=" + file);

            int count = 0;
            using (StreamReader sr = File.OpenText(file))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] row = s.Split('|');

                    sales_line oLine = new sales_line();

                    oLine.bu_id = int.Parse(row[0]);
                    oLine.bu_date = DateTime.Parse(row[1]);
                    oLine.shift_id = int.Parse(row[2]);
                    oLine.employee_id = int.Parse(row[3]);
                    oLine.transaction_id = int.Parse(row[4]);
                    oLine.transaction_line_id = int.Parse(row[5]);

                    try
                    {
                        oLine.transaction_timestamp = DateTime.Parse(row[6]);
                    }
                    catch { };
                    try
                    {
                        oLine.sales_type_id = int.Parse(row[7]);
                    }
                    catch { };
                    try
                    {
                        oLine.sales_item_id = int.Parse(row[8]);
                    }
                    catch { };
                    try
                    {
                        oLine.barcode = row[9];
                    }
                    catch { };

                    try
                    {
                        oLine.sale_qty = Decimal.Parse(row[10]);
                    }
                    catch { };
                    try
                    {
                        oLine.gross_amt = Decimal.Parse(row[11]);
                    }
                    catch { };
                    try
                    {
                        oLine.unit_price = Decimal.Parse(row[12]);
                    }
                    catch { };
                    try
                    {
                        oLine.promo_price = Decimal.Parse(row[13]);
                    }
                    catch { };
                    try
                    {
                        oLine.retail_price = Decimal.Parse(row[14]);
                    }
                    catch { };
                    try
                    {
                        oLine.refund_qty = Decimal.Parse(row[15]);
                    }
                    catch { };
                    try
                    {
                        oLine.refund_amt = Decimal.Parse(row[16]);
                    }
                    catch { };
                    try
                    {
                        oLine.reduction_amt = Decimal.Parse(row[17]);
                    }
                    catch { };
                    try
                    {
                        oLine.refund_reduction_amt = Decimal.Parse(row[18]);
                    }
                    catch { };
                    try
                    {
                        oLine.sales_dest_id = int.Parse(row[19]);
                    }
                    catch { };

                    oLine.component_flag = row[20];
                    oLine.item_name = row[21];
                    oLine.bu_name = row[22];
                    try
                    {
                        oLine.pump_number = int.Parse(row[23]);
                    }
                    catch { };
                    try
                    {
                        oLine.hose = int.Parse(row[24]);
                    }
                    catch { };

                    oLine.Save();

                    count++;
                }
            }      

            async.Notify(execution_id, "Rows Imported = " + count.ToString());
        }
    }
}
