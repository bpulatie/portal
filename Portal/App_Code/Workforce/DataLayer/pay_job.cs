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

    public class pay_job
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_workforce");
        private string db_pchar = "?";

        public pay_job()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      * 
FROM        pay_job
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    job_code, company, name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        pay_job
WHERE       job_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }


        internal bool NameExists(Guid client_id, Guid job_id, string name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));
            myParams.Add(DB.CreateParameter("job_id", typeof(string), job_id.ToString()));
            myParams.Add(DB.CreateParameter("name", typeof(string), name));

            string SQL = @"
SELECT      *
FROM        pay_job
WHERE       job_id <> " + db_pchar + @"job_id
AND         name = " + db_pchar + @"name
AND         client_id = " + db_pchar + @"client_id
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}