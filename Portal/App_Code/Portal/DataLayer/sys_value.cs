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

    public class sys_value
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_value()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string group_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("group_id", typeof(string), group_id));

            string SQL = @"
SELECT      * 
FROM        sys_value
WHERE       group_id = " + db_pchar + @"group_id
ORDER BY    sort_order, value_text
";
            SQL += filter;

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string value_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("value_id", typeof(string), value_id));

            string SQL = @"
SELECT      *
FROM        sys_value
WHERE       value_id = " + db_pchar + @"value_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public int DeleteByID(string value_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("value_id", typeof(string), value_id));

            string SQL = @"
DELETE      
FROM        sys_value
WHERE       value_id = " + db_pchar + @"value_id";

            return DB.ExecuteSQL(SQL, myParams);
        }

    }
}