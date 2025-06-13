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

    public class inv_category_level
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_inventory");
        private string db_pchar = "?";

        public inv_category_level()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        inv_category_level
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    depth
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public List<T> ListAll<T>(string client_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        inv_category_level
WHERE       client_id = " + db_pchar + @"client_id
ORDER BY    depth desc
";

            return DB.GetList<T>(SQL, myParams);
        }

        public string GetByID(string category_level_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("category_level_id", typeof(string), category_level_id));

            string SQL = @"
SELECT      *
FROM        inv_category_level
WHERE       category_level_id = " + db_pchar + @"category_level_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        internal object GetByLevel(string client_id, string level, string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("level", typeof(string), level));
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      @level as level, @id as p1, *
FROM        inv_category_level
WHERE       client_id = " + db_pchar + @"client_id
AND         depth = " + db_pchar + @"level
";

            SQL += @"
ORDER BY    level_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }
    }
}