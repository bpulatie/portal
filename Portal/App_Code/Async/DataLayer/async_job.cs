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

    public class async_job
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_async");
        private string db_pchar = "?";

        public async_job()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *, 
            CASE WHEN schedule_code = 'o' THEN 'On Demand' ELSE 'Scheduled' END as schedule_code_name
FROM        async_job
WHERE       client_id = " + db_pchar + @"client_id
";
            SQL += filter;
            SQL += @"
ORDER BY    job_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string job_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("job_id", typeof(string), job_id));

            string SQL = @"
SELECT      *
FROM        async_job
WHERE       job_id = " + db_pchar + @"job_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }


        internal object GetAllParametersByJob(string job_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("job_id", typeof(string), job_id));

            string SQL = @"
SELECT      DISTINCT p.parameter_name, 
            CASE data_type 
                WHEN 'a' THEN 'Alphanumeric' 
                WHEN 'n' THEN 'Numeric' 
                WHEN 'd' THEN 'Date' 
                ELSE 'Unkown' END as data_type_name,
            CASE required 
                WHEN 'y' THEN 'Mandatory' 
                ELSE 'Optional' END as required_name

FROM        async_job_task_list l
JOIN        async_task_parameter p
ON          l.task_id = p.task_id
WHERE       l.job_id = " + db_pchar + @"job_id
";
            SQL += filter;
            SQL += @"
ORDER BY    parameter_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }
    }
}