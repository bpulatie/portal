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

    public class sys_role
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_role()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        sys_role
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    role_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        sys_role 
WHERE       role_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllByUserAssigned(string user_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      * 
FROM        sys_role
WHERE       role_id IN (SELECT role_id FROM sys_user_role_list WHERE user_id = " + db_pchar + @"user_id ) ";

            SQL += filter + @"
ORDER BY    role_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllByUserUnassigned(string client_id, string user_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      * 
FROM        sys_role
WHERE       role_id NOT IN (SELECT role_id FROM sys_user_role_list WHERE user_id = " + db_pchar + @"user_id ) 
AND         client_id = " + db_pchar + @"client_id
";

            SQL += filter + @"

ORDER BY    role_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public DataSet GetRoleByExternalName(string name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("name", typeof(string), name));

            string SQL = @"
SELECT      *
FROM        sys_role 
WHERE       external_name = " + db_pchar + @"name";

            return DB.GetDataSet(SQL, myParams);
        }

        public void DeleteUserRolesForUser(string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
DELETE
FROM        sys_user_role_list 
WHERE       user_id = " + db_pchar + @"user_id";

            DB.ExecuteSQL(SQL, myParams);
        }

    }
}