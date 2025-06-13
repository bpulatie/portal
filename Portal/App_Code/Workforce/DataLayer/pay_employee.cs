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

    public class pay_employee
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_workforce");
        private string db_pchar = "?";

        public pay_employee()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        pay_employee 
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    last_name, first_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *,
            CASE status_code WHEN 'T' THEN 'Terminated' 
                             WHEN 'A' THEN 'Active'
                             WHEN 'L' THEN 'Leave of Absense' END as status_code_name,
            CASE rate_type   WHEN 'H' THEN 'Hourly'
                             WHEN 'S' THEN 'Salary' END as rate_type_name
FROM        pay_employee 
WHERE       employee_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public void queueEmployeeUpdate(string employee_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("employee_id", typeof(string), employee_id));

            string SQL = @"
exec queueEmployeeUpdate " + db_pchar + @"employee_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public void triggerEmployeeUpdate(string employee_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("employee_id", typeof(string), employee_id));

            string SQL = @"
exec triggerEmployeeUpdate " + db_pchar + @"employee_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

    }
}