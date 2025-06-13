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

    public class requirement_list
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("RDM_Repository");
        private string db_pchar = "?";

        public requirement_list()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      e.*, u.first_name, u.last_name
FROM        sys_user u
JOIN        employee e
ON          u.user_id = e.employee_id
WHERE       1=1 
";

            SQL += filter;
            SQL += @"
ORDER BY    last_name, first_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      e.*, u.first_name, u.last_name,
            CASE e.status_code WHEN 'T' THEN 'Terminated' 
                               WHEN 'A' THEN 'Active'
                               WHEN 'L' THEN 'Leave of Absense' END as status_code_name,
            CASE e.rate_type   WHEN 'H' THEN 'Hourly'
                               WHEN 'S' THEN 'Salary' END as rate_type_name
FROM        sys_user u
JOIN        employee e
ON          u.user_id = e.employee_id
WHERE       user_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

    }
}