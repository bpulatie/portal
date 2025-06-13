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

    public class supplier_catalog_current
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_custom");
        private string db_pchar = "?";

        public supplier_catalog_current()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      c.*, s.name as supplier_name, d.department_name
FROM        supplier_catalog_current c
LEFT JOIN   supplier_item_department d
ON          c.supplier_item_department_id = d.supplier_item_department_id
LEFT JOIN   supplier s
ON          c.supplier_id = s.supplier_id
WHERE       c.client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    supplier_category, product_description
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllUnassigned(string client_id, string supplier_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("supplier_id", typeof(string), supplier_id));

            string SQL = @"
SELECT      c.*, s.name as supplier_name, d.department_name
FROM        supplier_catalog_current c
LEFT JOIN   supplier_item_department d
ON          c.supplier_item_department_id = d.supplier_item_department_id
LEFT JOIN   supplier s
ON          c.supplier_id = s.supplier_id
WHERE       c.client_id = " + db_pchar + @"client_id
AND         c.supplier_id = " + db_pchar + @"supplier_id
AND         c.supplier_item_department_id is null
";

            SQL += filter;
            SQL += @"
ORDER BY    supplier_category, product_description
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetCountBySupplier(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      s.supplier_id, s.name as supplier_name, COALESCE(a.unassigned_items, 0) as unassigned_items, COALESCE(b.assigned_items, 0) as assigned_items
FROM        supplier s
LEFT JOIN   (
                SELECT      supplier_id, Count(*) as unassigned_items
                FROM        supplier_catalog_current
                WHERE       client_id = " + db_pchar + @"client_id
                AND         supplier_item_department_id is null
                GROUP BY    supplier_id
            ) a
ON          s.supplier_id = a.supplier_id
LEFT JOIN   (
                SELECT      supplier_id, Count(*) as assigned_items
                FROM        supplier_catalog_current
                WHERE       client_id = " + db_pchar + @"client_id
                AND         supplier_item_department_id is not null
                GROUP BY    supplier_id
            ) b
ON          s.supplier_id = b.supplier_id
";

            SQL += filter;
            SQL += @"
ORDER BY    supplier_name, unassigned_items
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string supplier_catalog_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("supplier_catalog_id", typeof(string), supplier_catalog_id));

            string SQL = @"
SELECT      c.*, s.name as supplier_name, d.department_name
FROM        supplier_catalog_current c
LEFT JOIN   supplier_item_department d
ON          c.supplier_item_department_id = d.supplier_item_department_id
LEFT JOIN   supplier s
ON          c.supplier_id = s.supplier_id
WHERE       c.supplier_catalog_id = " + db_pchar + @"supplier_catalog_id
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }


        public void AssignDepartment(string supplier_catalog_id, string supplier_item_department_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("supplier_catalog_id", typeof(string), supplier_catalog_id));
            myParams.Add(DB.CreateParameter("supplier_item_department_id", typeof(string), supplier_item_department_id));

            string SQL = @"
UPDATE      supplier_catalog_current 
SET         supplier_item_department_id = " + db_pchar + @"supplier_item_department_id
WHERE       supplier_catalog_id = " + db_pchar + @"supplier_catalog_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        internal void GetCatalogData(string client_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
exec        GetCatalogData @client_id, @user_id
";

            DB.ExecuteStoredProcedure(SQL, myParams);
        }

        internal void SendCatalogData(string client_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
exec        SendCatalogData @client_id, @user_id
";

            DB.ExecuteStoredProcedure(SQL, myParams);
        }
    }
}