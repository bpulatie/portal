using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using AsyncLibrary;

namespace ExecuteStoredProcedure
{
    public class Main
    {
        private static Database DB = new Database("spa_portal");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        public static void OnExecute(string[] Args)
        {
            logger.Info("ExecuteStoredProcedure: Starting");

            execution_id = Args[0];
            logger.Info("ExecuteStoredProcedure: Execution_id=" + execution_id);

            string context = async.GetJobContext(execution_id);
            logger.Info("ExecuteStoredProcedure: Context=" + context);

            string sp = utils.GetParameter(context, "StoredProcedure");

            ArrayList myParams = new ArrayList();
            string SQL = "exec " + sp;
            DB.ExecuteStoredProcedure(SQL, myParams);

            logger.Info("ExecuteStoredProcedure: Ending");
        }
    }
}
