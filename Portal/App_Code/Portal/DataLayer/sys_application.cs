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

    public class sys_application
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_application()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      * 
FROM        sys_application
WHERE       1=1 ";

            SQL += filter + @"
ORDER BY    application_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }
       
        public string GetByID(string application_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("application_id", typeof(string), application_id));

            string SQL = @"
SELECT      *
FROM        sys_application
WHERE       application_id = " + db_pchar + @"application_id
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        internal bool Exists(string application_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("application_name", typeof(string), application_name));

            string SQL = @"
SELECT      *
FROM        sys_application
WHERE       application_name = " + db_pchar + @"application_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}