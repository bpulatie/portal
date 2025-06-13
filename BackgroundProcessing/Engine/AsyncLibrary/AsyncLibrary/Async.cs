using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Data;

namespace AsyncLibrary
{
    public class Async
    {
        private static Database DB = new Database("spa_async");

        public Async()
        {

        }

        public string GetJobContext(string execution_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("execution_id", typeof(string), execution_id));

            string SQL = @"
SELECT      context
FROM        async_execution 
WHERE       execution_id = @execution_id
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["context"].ToString();
            else
                return string.Empty;
        }

        public void AddJobContext(string execution_id, string context)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(DB.CreateParameter("context", typeof(string), context));

            string SQL = @"
UPDATE      async_execution 
SET         context = CONCAT(context, @context)
WHERE       execution_id = @execution_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public void SetJobContext(string execution_id, string context)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(DB.CreateParameter("context", typeof(string), context));

            string SQL = @"
UPDATE      async_execution 
SET         context = @context
WHERE       execution_id = @execution_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public void Notify(string execution_id, string message)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(DB.CreateParameter("status_message", typeof(string), message));

            string SQL = @"
INSERT INTO async_execution_detail
(execution_detail_id, execution_id, execution_time, status_code, status_message)
VALUES (newid(), @execution_id, GetDate(), 'i', @status_message)
";

            DB.ExecuteSQL(SQL, myParams);
        }

    }
}
