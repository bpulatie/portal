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

    public class metric
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public metric()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string metric_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("metric_id", typeof(string), metric_id));
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        metric
WHERE       client_id = " + db_pchar + @"client_id
AND         metric_id = " + db_pchar + @"metric_id
";

            SQL += filter;
            SQL += @"
ORDER BY    business_date desc, dimension_1_name, dimension_2_name, dimension_3_name ";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }


        internal object GetAllMetrics(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        metric_config
WHERE       1=1
";

            SQL += filter;
            SQL += @"
ORDER BY    metric_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal object GetByMetricID(string metric_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("metric_id", typeof(string), metric_id));

            string SQL = @"
SELECT      *
FROM        metric_config
WHERE       metric_id = " + db_pchar + @"metric_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }
    }
}