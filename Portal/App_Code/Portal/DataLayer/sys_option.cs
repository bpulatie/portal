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

    public class sys_option
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_option()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      * 
FROM        sys_option
WHERE       1=1
";
            SQL += filter;

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetAllClient(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      o.*, v.option_value 
FROM        sys_option o
LEFT JOIN   sys_option_value v
ON          o.option_id = v.option_id
AND         v.client_id = " + db_pchar + @"client_id
WHERE       1=1
";
            SQL += filter;

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        sys_option
WHERE       option_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetByIDClient(string client_id, string option_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("option_id", typeof(string), option_id));

            string SQL = @"
SELECT      o.*, v.option_value 
FROM        sys_option o
LEFT JOIN   sys_option_value v
ON          o.option_id = v.option_id
AND         v.client_id = " + db_pchar + @"client_id
WHERE       o.option_id = " + db_pchar + @"option_id
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }
    }
}