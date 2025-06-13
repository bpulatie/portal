using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncLibrary;

namespace FailureTask
{
    public class Main
    {
        private static Database DB = new Database("spa_portal");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        public static void OnExecute(string [] Args)
        {
            logger.Info("FailureTask: Starting");

            string context = async.GetJobContext(Args[0]);

            logger.Info("FailureTask: Context=" + context);

            throw new Exception("Task Failed For Testing");

            logger.Info("FailureTask: Ending");
        }
    }
}
