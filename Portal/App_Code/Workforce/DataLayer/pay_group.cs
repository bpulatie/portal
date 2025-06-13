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

    public class pay_group
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_workforce");
        private string db_pchar = "?";

        public pay_group()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      * 
FROM        pay_group
WHERE       1=1
";
            SQL += filter + @"
ORDER BY    pay_group_code 
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string pay_group_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_group_id", typeof(string), pay_group_id));

            string SQL = @"
SELECT      *
FROM        pay_group
WHERE       pay_group_id = " + db_pchar + @"pay_group_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public DataSet GetPayGroupByID(string pay_group_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_group_id", typeof(string), pay_group_id));

            string SQL = @"
SELECT      *
FROM        pay_group
WHERE       pay_group_id = " + db_pchar + @"pay_group_id";

            return DB.GetDataSet(SQL, myParams);
        }

        public string GetAllInPayPeriod(string pay_period_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT      g.pay_group_id, g.pay_group_code 
FROM        pay_detail d
JOIN        pay_group g
ON          d.pay_group_id = g.pay_group_id
WHERE       d.pay_period_id = " + db_pchar + @"pay_period_id
GROUP BY    g.pay_group_id, g.pay_group_code
ORDER BY    pay_group_code";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }
    }
}