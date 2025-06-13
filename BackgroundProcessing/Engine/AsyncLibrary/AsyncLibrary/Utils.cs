using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AsyncLibrary
{
    public class Utils
    {
        private AppSettingsSection appSettings;

        public Utils()
        {
            string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AsyncProcessor.exe");

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
            appSettings = configuration.AppSettings;
        }

        public string GetAppSetting(string setting)
        {
            return appSettings.Settings[setting].Value;
        }

        public string GetParameter(string context, string name)
        {

            string[] parameters = context.Split(';');
            foreach (string parameter in parameters)
            {
                string[] value = parameter.Split('=');

                if (value[0].Trim() == name)
                    return value[1].Trim();
            }

            throw new Exception("Unknown Parameter Requested: " + name);
        }
    }
}
