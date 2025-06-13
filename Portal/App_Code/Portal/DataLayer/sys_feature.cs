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

    public class sys_feature
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_feature()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      f.*, g.application_name 
FROM        sys_feature f
LEFT JOIN   sys_application g
ON          f.application_id = g.application_id
WHERE       1=1 ";

            SQL += filter + @"
ORDER BY    application_name, feature_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      * 
FROM        sys_feature 
WHERE       feature_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllAssignedClientFeatures(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      f.*, g.application_name 
FROM        sys_feature f
LEFT JOIN   sys_application g
ON          f.application_id = g.application_id
WHERE       feature_id IN (SELECT feature_id FROM sys_client_feature_list WHERE client_id = " + db_pchar + @"client_id) 
";

            SQL += filter + @"
ORDER BY    application_name, feature_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllUnassignedClientFeatures(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      f.*, g.application_name 
FROM        sys_feature f
LEFT JOIN   sys_application g
ON          f.application_id = g.application_id
WHERE       feature_id NOT IN (SELECT feature_id FROM sys_client_feature_list WHERE client_id = " + db_pchar + @"client_id) 
";

            SQL += filter + @"
ORDER BY    application_name, feature_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllApplicationFeatures(string application_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("application_id", typeof(string), application_id));

            string SQL = @"
SELECT      *
FROM        sys_feature f
WHERE       application_id = " + db_pchar + @"application_id 
";

            SQL += filter + @"
ORDER BY    feature_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }


        internal bool Exists(string feature_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("feature_name", typeof(string), feature_name));

            string SQL = @"
SELECT      *
FROM        sys_feature
WHERE       feature_name = " + db_pchar + @"feature_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}