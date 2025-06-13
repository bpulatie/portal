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

namespace PetesSalesMixImport
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
            logger.Info("PetesSalesMixImport: Starting");

            execution_id = Args[0];
            logger.Info("v: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("PetesSalesMixImport: Context=" + context);

            string pFTPResult = utils.GetParameter(context, "RetrievedFiles");

            String[] files = pFTPResult.Split('&');

            foreach (string file in files)
            {
                async.Notify(execution_id, "Importing file " + file);
                ImportData(file);
            }

            logger.Info("PetesSalesMixImport: Ending");
        }

        private static void ImportData(string file)
        {
            logger.Info("PetesSalesMixImport: File=" + file);

            int count = 0;
            using (StreamReader sr = File.OpenText(file))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] row = s.Split('|');

                    sales oSales = new sales();
                    oSales.bu_id = int.Parse(row[0]);
                    oSales.bu_name = row[1];
                    oSales.bu_date = DateTime.Parse(row[2]);

                    oSales.department = row[3];
                    oSales.category = row[4];
                    oSales.sub_category = row[5];
                    oSales.item_name = row[6];

                    oSales.gross_sold_qty = Decimal.Parse(row[7]);
                    oSales.gross_sold_amt = Decimal.Parse(row[8]);
                    oSales.net_sold_amt = Decimal.Parse(row[9]);
                    oSales.refund_amt = Decimal.Parse(row[10]);
                    oSales.reduction_amt = Decimal.Parse(row[11]);

                    oSales.Save();

                    count++;
                }
            }            

            async.Notify(execution_id, "Rows Imported = " + count.ToString());
        }
    }
}
