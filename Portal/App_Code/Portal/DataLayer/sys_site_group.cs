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

    public class sys_site_group
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_site_group()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      * 
FROM        sys_site_group
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter + @"
ORDER BY    name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string site_group_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("site_group_id", typeof(string), site_group_id));

            string SQL = @"
SELECT      *
FROM        sys_site_group
WHERE       site_group_id = " + db_pchar + @"site_group_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAssignedGroupSites(string client_id, string site_group_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("site_group_id", typeof(string), site_group_id));

            string SQL = @"
SELECT      *
FROM        sys_site 
WHERE       site_id IN (   
                            SELECT  site_id 
                            FROM    sys_site_group_list 
                            WHERE   site_group_id = " + db_pchar + @"site_group_id
                        )
AND         client_id = " + db_pchar + @"client_id    
";

            SQL += filter + @"
ORDER BY    site_code, name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetUnassignedGroupSites(string client_id, string site_group_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("site_group_id", typeof(string), site_group_id));

            string SQL = @"
SELECT      *
FROM        sys_site 
WHERE       site_id NOT IN (   
                            SELECT  site_id 
                            FROM    sys_site_group_list 
                        )
AND         client_id = " + db_pchar + @"client_id    
";

            SQL += filter + @"
ORDER BY    site_code, name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }


        internal bool Exists(Guid client_id, Guid site_group_id, string name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));
            myParams.Add(DB.CreateParameter("site_group_id", typeof(string), site_group_id.ToString()));
            myParams.Add(DB.CreateParameter("name", typeof(string), name));

            string SQL = @"
SELECT      *
FROM        sys_site_group
WHERE       site_group_id <> " + db_pchar + @"site_group_id
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