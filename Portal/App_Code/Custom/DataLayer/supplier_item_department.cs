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

    public class supplier_item_department
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_custom");
        private string db_pchar = "?";

        public supplier_item_department()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string supplier_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("supplier_id", typeof(string), supplier_id));

            string SQL = @"
SELECT      *
FROM        supplier_item_department
WHERE       client_id = " + db_pchar + @"client_id
AND         supplier_id = " + db_pchar + @"supplier_id
";

            SQL += filter;
            SQL += @"
ORDER BY    department_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string supplier_item_department_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("supplier_item_department_id", typeof(string), supplier_item_department_id));

            string SQL = @"
SELECT      *
FROM        supplier_item_department
WHERE       supplier_item_department_id = " + db_pchar + @"supplier_item_department_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

    }
}