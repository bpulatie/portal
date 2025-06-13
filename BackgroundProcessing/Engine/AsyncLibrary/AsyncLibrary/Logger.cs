using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;
using System.Runtime.CompilerServices;

namespace AsyncLibrary
{
    public class Logger
    {
        private ILog log = log4net.LogManager.GetLogger("SPATask");

        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
            string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AsyncProcessor.exe");

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
            AppSettingsSection appSettings = configuration.AppSettings;

        }

        public void Info(string message)
        {
            log.Info("[" + GetCurrentNamespace() + "] " + message);
        }

        public void Error(string message)
        {
            log.Error("[" + GetCurrentNamespace() + "] " + message);
        }

        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentNamespace()
        {
            return System.Reflection.Assembly.GetCallingAssembly().EntryPoint.DeclaringType.Namespace;
        }
    }
}
