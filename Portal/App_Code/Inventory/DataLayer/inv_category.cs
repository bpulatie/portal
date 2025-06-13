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

    public class inv_category
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_inventory");
        private string db_pchar = "?";

        public inv_category()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      c.*, l.level_name
FROM        inv_category c
JOIN        inv_category_level l
ON          c.category_level_id = l.category_level_id
WHERE       c.client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    level_name, name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllLeafCategories(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      c.*, l.level_name
FROM        inv_category c
JOIN        inv_category_level l
ON          c.category_level_id = l.category_level_id
WHERE       c.client_id = " + db_pchar + @"client_id
AND         l.item_level = 'y'
";

            SQL += filter;
            SQL += @"
ORDER BY    level_name, name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllHierarchy(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
;WITH CTE as
(
            SELECT        c.parent_category_id, c.category_id, c.name, l.depth, l.level_name, cast(c.name as varchar(500)) as myOrder
            FROM          inv_category c
            JOIN          inv_category_level l
            ON            c.category_level_id = l.category_level_id
            WHERE         c.parent_category_id = '00000000-0000-0000-0000-000000000000'
            AND           c.client_id = " + db_pchar + @"client_id
  
            UNION ALL

            SELECT        a.parent_category_id, a.category_id, a.name, l.depth, l.level_name, cast(concat(rtrim(c.myOrder), a.name) as varchar(500))  as myOrder
            FROM          CTE c
            INNER JOIN    inv_category a
            ON            c.category_id = a.parent_category_id
            AND           a.client_id = " + db_pchar + @"client_id
            JOIN          inv_category_level l
            ON            a.category_level_id = l.category_level_id
)

SELECT      * 
FROM        CTE 
ORDER BY    myOrder
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string category_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("category_id", typeof(string), category_id));

            string SQL = @"
SELECT      c.*, l.level_name
FROM        inv_category c
JOIN        inv_category_level l
ON          c.category_level_id = l.category_level_id
WHERE       category_id = " + db_pchar + @"category_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        internal object GetAllByLevel(string client_id, string category_level_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("category_level_id", typeof(string), GetNextLevelHigher(client_id, category_level_id)));

            string SQL = @"
SELECT      c.*, l.level_name
FROM        inv_category c
JOIN        inv_category_level l
ON          c.category_level_id = l.category_level_id
WHERE       c.client_id = " + db_pchar + @"client_id
AND         c.category_level_id = " + db_pchar + @"category_level_id
";

            SQL += filter;
            SQL += @"
ORDER BY    level_name, name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        private string GetNextLevelHigher(string client_id, string category_level_id)
        {
            if (category_level_id == "")
                return Guid.Empty.ToString();

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("category_level_id", typeof(string), category_level_id));

            string SQL = @"
SELECT      top 1 l.*
FROM        inv_category_level c
JOIN        inv_category_level l 
ON          l.depth < c.depth
WHERE       c.client_id = " + db_pchar + @"client_id
AND         l.client_id = " + db_pchar + @"client_id
AND         c.category_level_id = " + db_pchar + @"category_level_id
ORDER BY	l.depth desc
";

            DataSet ds = DB.GetDataSet(SQL, myParams);

            if (ds.Tables[0].Rows.Count == 0)
                return Guid.Empty.ToString();

            return ds.Tables[0].Rows[0]["category_level_id"].ToString();
        }
    }
}