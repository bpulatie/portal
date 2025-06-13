using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for member
/// </summary>
/// 
namespace DataLayer
{

    public class async_task
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_async");
        private string db_pchar = "?";

        public async_task()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        async_task
WHERE       client_id = " + db_pchar + @"client_id
";
            SQL += filter;
            SQL += @"
ORDER BY    task_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetAllByJob(string job_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("job_id", typeof(string), job_id));

            string SQL = @"
SELECT      l.*, t.task_name, t.moniker
FROM        async_job_task_list l
JOIN        async_task t
ON          l.task_id = t.task_id
WHERE       l.job_id = " + db_pchar + @"job_id
";
            SQL += filter;
            SQL += @"
ORDER BY    sort_order, task_name
";


            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string task_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("task_id", typeof(string), task_id));

            string SQL = @"
SELECT      *
FROM        async_task
WHERE       task_id = " + db_pchar + @"task_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }


        internal object GetAllParametersByTask(string task_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("task_id", typeof(string), task_id));

            string SQL = @"
SELECT      *, 
            CASE data_type 
                WHEN 'a' THEN 'Alphanumeric' 
                WHEN 'n' THEN 'Numeric' 
                WHEN 'd' THEN 'Date' 
                ELSE 'Unkown' END as data_type_name,
            CASE required 
                WHEN 'y' THEN 'Mandatory' 
                ELSE 'Optional' END as required_name

FROM        async_task_parameter
WHERE       task_id = " + db_pchar + @"task_id
";
            SQL += filter;
            SQL += @"
ORDER BY    parameter_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }
    }
}