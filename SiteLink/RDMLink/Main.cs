using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;
using System.Timers;
using System.IO;
using System.Configuration;
using System.Net;
using System.ServiceModel;

namespace RDMLink
{
    public partial class Main : ServiceBase
    {
        ILog logger = log4net.LogManager.GetLogger("SPALog");

        private RDM_Services.import_export_ServicesSoapClient oWebService = new RDM_Services.import_export_ServicesSoapClient();
        private System.Timers.Timer aTimer;
        private int interval = 2000;

        public Main()
        {
            InitializeComponent();

            if (!System.Diagnostics.EventLog.SourceExists("RDMSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("RDMSource", "RDMLinkLog");
            }
            RDMLinkLog.Source = "RDMSource";
            RDMLinkLog.Log = "RDMLinkLog";

            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnStart(string[] args)
        {
            logger.Info("RDMLink Started.");
            RDMLinkLog.WriteEntry("RDMLink Starting");

            aTimer = new System.Timers.Timer(10000);

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = interval;
            aTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            logger.Info("RDMLink Stopping.");
            RDMLinkLog.WriteEntry("RDMLink Stopping");
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            aTimer.Stop();

            try
            {
                logger.Info("Start Send");

                string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RDMConfig.exe");
                logger.Info("exePath=" + exePath);

                Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
                AppSettingsSection appSettings = configuration.AppSettings;

                string folderToMonitor = appSettings.Settings["folderToMonitor"].Value;
                string folderForBackup = appSettings.Settings["folderForBackup"].Value;
                string hostURL = appSettings.Settings["hostURL"].Value;
                string siteGUID = appSettings.Settings["siteGUID"].Value;

                string[] files = Directory.GetFiles(folderToMonitor);
                logger.Info(files.Length.ToString() + " Files Found");

                foreach (string file in files)
                {
                    SendFile(hostURL, siteGUID, file);

                    logger.Info("Backup File to - " + folderForBackup + @"\" + Path.GetFileName(file));
                    File.Copy(file, folderForBackup + @"\" + Path.GetFileName(file), true);
                    File.Delete(file);
                }

                logger.Info("End Send");

            }
            catch (Exception ex)
            {
                logger.Error("RDMLink: Error - " + ex.Message);
            }

            aTimer.Start();
        }

        private void SendFile(string hostURL, string siteGUID, string file)
        {
            //Set the URL
            oWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress(hostURL + @"/Services/import_export_Services.asmx");

            FileInfo fInfo = new FileInfo(file);
            long numBytes = fInfo.Length;
            double dLen = Convert.ToDouble(fInfo.Length / 1000000);

            if (dLen < 4)
            {
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] data = br.ReadBytes((int)numBytes);
                br.Close();

                logger.Info("Sending File - " + Path.GetFileName(file) + @"   Size = " + numBytes.ToString());

                string sResponse = oWebService.FileUpload(siteGUID, Path.GetFileName(file), data);
                logger.Info(sResponse);

                fs.Close();
                fs.Dispose();

                logger.Info("Posted " + Path.GetFileName(file));
            }
            else
            {
                logger.Info("The file selected exceeds the size limit for uploads. " + Path.GetFileName(file));
            }
        }

    }
}
