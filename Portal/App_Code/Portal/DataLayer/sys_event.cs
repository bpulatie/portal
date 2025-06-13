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

    public class sys_event
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_event()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *, CASE event_type WHEN 'i' THEN 'Information'
                               WHEN 'e' THEN 'Error'
                               WHEN 'w' THEN 'Warning'
                               ELSE 'Unknown Type'
                               END as event_type_name 
FROM        sys_event
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    event_date desc
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *, CASE event_type WHEN 'i' THEN 'Information'
                               WHEN 'e' THEN 'Error'
                               WHEN 'w' THEN 'Warning'
                               ELSE 'Unknown Type'
                               END as event_type_name 
FROM        sys_event
WHERE       event_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllCategories(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        sys_event_category
WHERE       1=1
";

            SQL += filter;
            SQL += @"
ORDER BY    event_category_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByCategoryID(string category_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("category_id", typeof(string), category_id));

            string SQL = @"
SELECT      *
FROM        sys_event_category
WHERE       event_category_id = " + db_pchar + @"category_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllEventsByCategory(string event_category_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_category_id", typeof(string), event_category_id));

            string SQL = @"
SELECT      e.*, c.event_category_name
FROM        sys_event_type e
JOIN        sys_event_category c
ON          e.event_category_id = c.event_category_id
WHERE       e.event_category_id = " + db_pchar + @"event_category_id
";

            SQL += filter;
            SQL += @"
ORDER BY    event_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByEventID(string event_type_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_type_id", typeof(string), event_type_id));

            string SQL = @"
SELECT      e.*, c.event_category_name
FROM        sys_event_type e
JOIN        sys_event_category c
ON          e.event_category_id = c.event_category_id
WHERE       e.event_type_id = " + db_pchar + @"event_type_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllSubscribedEvents(string user_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      s.*, e.event_name, c.event_category_name
FROM        sys_event_subscription s
JOIN        sys_event_type e
ON          e.event_type_id = s.event_type_id
JOIN        sys_event_category c
ON          e.event_category_id = c.event_category_id
WHERE       s.user_id = " + db_pchar + @"user_id
";

            SQL += filter;
            SQL += @"
ORDER BY    event_category_name, event_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllUnSubscribedEvents(string user_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      e.*, c.event_category_name
FROM        sys_event_type e
JOIN        sys_event_category c
ON          e.event_category_id = c.event_category_id
WHERE       e.event_type_id NOT IN (SELECT event_type_id FROM sys_event_subscription WHERE user_id = " + db_pchar + @"user_id)
";

            SQL += filter;
            SQL += @"
ORDER BY    event_category_name, event_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal bool Exists(string event_category_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_category_name", typeof(string), event_category_name));

            string SQL = @"
SELECT      *
FROM        sys_event_category
WHERE       event_category_name = " + db_pchar + @"event_category_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal bool TypeExists(string event_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_name", typeof(string), event_name));

            string SQL = @"
SELECT      *
FROM        sys_event_type
WHERE       event_name = " + db_pchar + @"event_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal string GetDashboardEvents(string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      e.*
FROM        sys_event_subscription s
JOIN        sys_event e
ON          s.event_type_id = e.event_type_id
WHERE       s.user_id = " + db_pchar + @"user_id
AND         e.creation_date > DATEADD(day, -7, GetDate())
ORDER BY    e.event_date desc
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal string GetBySubscriptionID(string event_subscription_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("event_subscription_id", typeof(string), event_subscription_id));

            string SQL = @"
SELECT      s.*, e.event_name
FROM        sys_event_subscription s
JOIN        sys_event_type e
ON          s.event_type_id = e.event_type_id
WHERE       s.event_subscription_id = " + db_pchar + @"event_subscription_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }
    }
}
