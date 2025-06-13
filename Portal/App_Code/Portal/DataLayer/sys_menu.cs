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

    public class sys_menu
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_menu()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        sys_menu
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    sort_order
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string menu_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("menu_id", typeof(string), menu_id));

            string SQL = @"
SELECT      *
FROM        sys_menu
WHERE       menu_id = " + db_pchar + @"menu_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public void RemoveMenuItem(string menu_id, string feature_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("menu_id", typeof(string), menu_id));
            myParams.Add(DB.CreateParameter("feature_id", typeof(string), feature_id));

            string SQL = @"
DELETE
FROM        sys_menu_item
WHERE       menu_id = " + db_pchar + @"menu_id
AND         feature_id = " + db_pchar + @"feature_id
";
            DB.ExecuteSQL(SQL, myParams);
        }

        public string GetAllByMenuAssigned(string menu_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("menu_id", typeof(string), menu_id));

            string SQL = @"
SELECT		i.*, f.feature_name,
            CASE WHEN i.menu_mode = 0 THEN 'Update'
                 ELSE 'View' END as menu_mode_name 
FROM		sys_menu_item i
JOIN        sys_feature f
ON          i.feature_id = f.feature_id
WHERE       menu_id = " + db_pchar + @"menu_id
";

            SQL += filter;
            SQL += @"
ORDER BY    sort_order, menu_item_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }


        internal object GetAllByMenuUnAssigned(string client_id, string menu_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("menu_id", typeof(string), menu_id));
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT		f.* 
FROM		sys_feature f
JOIN		sys_client_feature_list i
ON          i.feature_id = f.feature_id
WHERE		client_id = " + db_pchar + @"client_id
AND			f.feature_id not in (
									SELECT	feature_id FROM	sys_menu_item WHERE menu_id = " + db_pchar + @"menu_id
								)
";

            SQL += filter;
            SQL += @"
ORDER BY    feature_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetAllUnassigned(string client_id, string role_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));

            string SQL = @"
SELECT		*
FROM		sys_menu 
WHERE       menu_id not in (
                                SELECT  menu_id 
                                FROM    sys_role_menu_list 
                                WHERE   role_id = " + db_pchar + @"role_id
                            )
AND         client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    menu_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetAllAssigned(string role_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));

            string SQL = @"
SELECT		m.*
FROM		sys_menu m
JOIN        sys_role_menu_list l
ON          m.menu_id = l.menu_id
WHERE       role_id = " + db_pchar + @"role_id
";

            SQL += filter;
            SQL += @"
ORDER BY    sort_order, menu_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal bool Exists(Guid client_id, string menu_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));
            myParams.Add(DB.CreateParameter("menu_name", typeof(string), menu_name));

            string SQL = @"
SELECT      *
FROM        sys_menu
WHERE       client_id = " + db_pchar + @"client_id
AND         menu_name = " + db_pchar + @"menu_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}