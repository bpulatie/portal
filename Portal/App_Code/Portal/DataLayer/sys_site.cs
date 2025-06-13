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

    public class sys_site
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_site()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      * 
FROM        sys_site
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter + @"
ORDER BY    name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllByUserAssigned(string client_id, string user_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      s.* 
FROM        sys_user_site_list l
JOIN        sys_site s
ON          s.site_id = l.site_id
WHERE       l.user_id = " + db_pchar + @"user_id 
AND         s.client_id = " + db_pchar + @"client_id
";

            SQL += filter + @"
ORDER BY    name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllByUserUnassigned(string client_id, string user_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      * 
FROM        sys_site
WHERE       site_id NOT IN (SELECT site_id FROM sys_user_site_list WHERE user_id = " + db_pchar + @"user_id) 
AND         client_id = " + db_pchar + @"client_id 
";
            
            SQL += filter + @" 
ORDER BY    name
";


            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        sys_site
WHERE       site_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        internal bool NameExists(Guid client_id, Guid site_id, string name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id.ToString()));
            myParams.Add(DB.CreateParameter("name", typeof(string), name));

            string SQL = @"
SELECT      *
FROM        sys_site
WHERE       site_id <> " + db_pchar + @"site_id
AND         name = " + db_pchar + @"name
AND         client_id = " + db_pchar + @"client_id
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal bool CodeExists(Guid client_id, Guid site_id, string site_code)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id.ToString()));
            myParams.Add(DB.CreateParameter("site_code", typeof(string), site_code));

            string SQL = @"
SELECT      *
FROM        sys_site
WHERE       site_id <> " + db_pchar + @"site_id
AND         site_code = " + db_pchar + @"site_code
AND         client_id = " + db_pchar + @"client_id
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal DataSet GetBySiteGuid(string site_guid)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("site_guid", typeof(string), site_guid));

            string SQL = @"
SELECT      *
FROM        sys_site
WHERE       site_guid = " + db_pchar + @"site_guid";

            return DB.GetDataSet(SQL, myParams);
        }
    }
}