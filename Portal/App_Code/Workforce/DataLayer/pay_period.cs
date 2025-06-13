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

    public class pay_period
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_workforce");
        private string db_pchar = "?";

        public pay_period()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string orderBy = "ORDER BY    start_date DESC";
            if (filter.IndexOf("= 'o'") > 0)
                orderBy = "ORDER BY    start_date";

            string SQL = @"
SELECT      p.*, COALESCE(x.open_exceptions, 0) as open_exceptions, COALESCE(z.closed_exceptions, 0) as closed_exceptions,
            CASE p.status_code WHEN 'o' THEN 'Open'
                               WHEN 'c' THEN 'Closed'
                               ELSE 'Unknown' END as status_name 
FROM        pay_period p

LEFT JOIN	(
			SELECT		pay_period_id, count(*) as open_exceptions 
			FROM		pay_exception
			WHERE		status_code = 'o'
			GROUP BY	pay_period_id
			) x
ON			x.pay_period_id = p.pay_period_id

LEFT JOIN	(
			SELECT		pay_period_id, count(*) as closed_exceptions 
			FROM		pay_exception
			WHERE		status_code <> 'o'
			GROUP BY	pay_period_id
			) z
ON			z.pay_period_id = p.pay_period_id

WHERE       p.client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @" " + orderBy;

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        pay_period p
WHERE       pay_period_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public DataSet GetPayPeriodByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        pay_period p
WHERE       pay_period_id = " + db_pchar + @"id";

            return DB.GetDataSet(SQL, myParams);
        }

        public DataSet GetPayPeriodStatus(string pay_period_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT      COALESCE(summary_records,0) as summary_records,
			COALESCE(open_exceptions,0) as open_exceptions,
			COALESCE(closed_exceptions,0) as closed_exceptions

FROM		pay_period p

LEFT JOIN	(
                SELECT      pay_period_id, count(*) as summary_records
                FROM        pay_detail 
                WHERE       pay_period_id = " + db_pchar + @"pay_period_id
				GROUP BY	pay_period_id
			) a
ON			p.pay_period_id = a.pay_period_id
LEFT JOIN	(
                SELECT      pay_period_id, count(*) as open_exceptions
                FROM        pay_exception 
                WHERE       pay_period_id = " + db_pchar + @"pay_period_id
                AND         status_code = 'o'
				GROUP BY	pay_period_id
			) b
ON			p.pay_period_id = b.pay_period_id
LEFT JOIN	(
                SELECT      pay_period_id, count(*) as closed_exceptions
                FROM        pay_exception 
                WHERE       pay_period_id = " + db_pchar + @"pay_period_id
                AND         status_code = 'c'
				GROUP BY	pay_period_id
            ) c
ON			p.pay_period_id = c.pay_period_id
WHERE		p.pay_period_id = " + db_pchar + @"pay_period_id
";

            return DB.GetDataSet(SQL, myParams);
        }

        public void GeneratePayFile(string pay_period_id, string pay_group_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("pay_group_id", typeof(string), pay_group_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
exec GeneratePayFile " + db_pchar + @"pay_period_id, " + db_pchar + @"pay_group_id, " + db_pchar + @"user_id 
";

            DB.ExecuteStoredProcedure(SQL, myParams);
        }

        public void ReprocessPayFile(string pay_period_id, string pay_group_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("pay_group_id", typeof(string), pay_group_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
exec ReprocessPayFile " + db_pchar + @"pay_period_id, " + db_pchar + @"pay_group_id, " + db_pchar + @"user_id 
";

            DB.ExecuteStoredProcedure(SQL, myParams);
        }
        
        public void ImportPayData(string pay_period_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
exec ImportPayData " + db_pchar + @"pay_period_id, " + db_pchar + @"user_id 
";

            DB.ExecuteStoredProcedure(SQL, myParams);
        }
    }
}


