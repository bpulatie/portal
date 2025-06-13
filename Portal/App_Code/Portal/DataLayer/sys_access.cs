using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for member
/// </summary>
/// 
namespace DataLayer
{

    public class sys_access
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_access()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        sys_access
WHERE       1=1 
";

            SQL += filter;
            SQL += @"
ORDER BY    access_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string access_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), access_id));

            string SQL = @"
SELECT      *
FROM        sys_access
WHERE       access_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllByRoleAssigned(string role_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));

            string SQL = @"
SELECT      * 
FROM        sys_access
WHERE       access_id IN (SELECT access_id FROM sys_role_access_list WHERE role_id = " + db_pchar + @"role_id ) ";

            SQL += filter + @"
ORDER BY    access_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllByRoleUnassigned(string role_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));

            string SQL = @"
SELECT      * 
FROM        sys_access
WHERE       access_id NOT IN (SELECT access_id FROM sys_role_access_list WHERE role_id = " + db_pchar + @"role_id ) ";

            SQL += filter + @"

ORDER BY    access_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public void DeleteAccessForRole(string role_id, string access_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));
            myParams.Add(DB.CreateParameter("access_id", typeof(string), access_id));

            string SQL = @"
DELETE
FROM        sys_role_access_list 
WHERE       role_id = " + db_pchar + @"role_id
AND         access_id = " + db_pchar + @"access_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public void AddAccessForRole(string user_id, string role_id, string access_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));
            myParams.Add(DB.CreateParameter("access_id", typeof(string), access_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
INSERT
INTO        sys_role_access_list 
            (role_id, access_id, creation_user_id, creation_date, modified_user_id, modified_date)
VALUES      (" + db_pchar + @"role_id, " + db_pchar + @"access_id, " + db_pchar + @"user_id, GETDATE(), " + db_pchar + @"user_id, GETDATE() ) 
";

            DB.ExecuteSQL(SQL, myParams);
        }


        internal bool Exists(string access_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("access_name", typeof(string), access_name));

            string SQL = @"
SELECT      *
FROM        sys_access
WHERE       access_name = " + db_pchar + @"access_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}