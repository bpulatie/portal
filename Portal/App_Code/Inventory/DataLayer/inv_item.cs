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

    public class inv_item
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_inventory");
        private string db_pchar = "?";

        public inv_item()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      i.*, c.name as category_name
FROM        inv_item i
JOIN        inv_category c
ON          i.category_id = c.category_id
WHERE       c.client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    i.item_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string item_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("item_id", typeof(string), item_id));

            string SQL = @"
SELECT      i.*, c.name as category_name
FROM        inv_item i
JOIN        inv_category c
ON          i.category_id = c.category_id
WHERE       i.item_id = " + db_pchar + @"item_id";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

    }
}