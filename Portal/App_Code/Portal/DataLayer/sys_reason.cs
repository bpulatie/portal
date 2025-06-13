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

    public class sys_reason
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_reason()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAllCategories(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        sys_reason_category
WHERE       1=1
";

            SQL += filter;
            SQL += @"
ORDER BY    reason_category
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByCategoryID(string reason_category_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("reason_category_id", typeof(string), reason_category_id));

            string SQL = @"
SELECT      *
FROM        sys_reason_category
WHERE       reason_category_id = " + db_pchar + @"reason_category_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      r.*, c.reason_category
FROM        sys_reason r
JOIN        sys_reason_category c
ON          r.reason_category_id = c.reason_category_id
WHERE       r.client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    reason_category, reason_code
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string reason_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("reason_id", typeof(string), reason_id));

            string SQL = @"
SELECT      *
FROM        sys_reason
WHERE       reason_id = " + db_pchar + @"reason_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

    }
}