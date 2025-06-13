using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsyncLibrary;

namespace SuccessTask
{
    public class Main
    {
        private static Database DB = new Database("spa_portal");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        public static void OnExecute(string [] Args)
        {
            logger.Info("SuccessTask: Starting");

            string context = async.GetJobContext(Args[0]);

            logger.Info("SuccessTask: Context=" + context);



            logger.Info("SuccessTask: Ending");
        }

    }
}
