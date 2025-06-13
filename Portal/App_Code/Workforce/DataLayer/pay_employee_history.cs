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

    public class pay_employee_history
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_workforce");
        private string db_pchar = "?";

        public pay_employee_history()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAllHistoryForEmployee(string employee_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("employee_id", typeof(string), employee_id));

            string SQL = @"
SELECT      *
FROM        pay_employee_history
WHERE       employee_id = " + db_pchar + @"employee_id 
";

            SQL += filter;
            SQL += @"
ORDER BY    modified_date desc";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByHistoryID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        pay_employee_history
WHERE       employee_history_id = " + db_pchar + @"id 
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }
    }
}