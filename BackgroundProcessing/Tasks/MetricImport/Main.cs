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
using System.Xml;
using System.IO;

namespace MetricImport
{
    public class Main
    {
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        private static string pFTPResult = string.Empty;
        private static string pClientID = string.Empty;
        private static string pMetricID = string.Empty;

        public static void OnExecute(string[] Args)
        {
            logger.Info("MetricImport: Starting");

            execution_id = Args[0];
            logger.Info("MetricImport: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("MetricImport: Context=" + context);

            string pFTPResult = utils.GetParameter(context, "RetrievedFiles");
            string pClientID = utils.GetParameter(context, "ClientID");
            string pMetricID = utils.GetParameter(context, "MetricID");

            if (pFTPResult == String.Empty)
            {
                logger.Info("MetricImport: No files to Import");
                async.Notify(execution_id, "No files to Import");
            }
            else
            {
                String[] files = pFTPResult.Split('&');

                foreach (string file in files)
                {
                    async.Notify(execution_id, "Importing file " + file);
                    ImportData(pMetricID, pClientID, file);
                }
            }

            logger.Info("MetricImport: Ending");
        }

        private static void ImportData(string metric_id, string client_id, string file)
        {
            logger.Info("MetricImport: File=" + file);

            int count = 0;
            using (StreamReader sr = File.OpenText(file))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] row = s.Split('|');

                    metric oMetric = new metric();

                    oMetric.metric_id = Guid.Parse(metric_id);
                    oMetric.client_id = Guid.Parse(client_id);

                    oMetric.business_date = DateTime.Parse(row[0]);
                    oMetric.dimension_1_id = int.Parse(row[1]);
                    oMetric.dimension_1_name = row[2];
                    oMetric.dimension_2_id = int.Parse(row[3]);
                    oMetric.dimension_2_name = row[4];
                    oMetric.dimension_3_id = 0;
                    oMetric.dimension_3_name = string.Empty;

                    try
                    {
                        oMetric.value_1 = decimal.Parse(row[5]);
                    }
                    catch { };

                    oMetric.Save();
                    count++;
                }
            }      

            async.Notify(execution_id, "Rows Imported = " + count.ToString());
        }
    }
}
