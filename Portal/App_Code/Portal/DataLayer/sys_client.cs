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

    public class sys_client
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_client()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      c.*, COALESCE(s.site_count,0) as site_count, COALESCE(u.user_count,0) as user_count,
            CASE WHEN status_code = 'o' THEN 'Open'
                 WHEN status_code = 'p' THEN 'Prospect'
                 WHEN status_code = 's' THEN 'Suspended'
                 WHEN status_code = 'c' THEN 'Closed'
                 ELSE 'Unknown' END as status_name
FROM        sys_client c
LEFT JOIN   (
                SELECT      client_id, COUNT(*) as site_count
                FROM        sys_site
                GROUP BY    client_id
            ) s
ON          c.client_id = s.client_id       
LEFT JOIN   (
                SELECT      client_id, COUNT(*) as user_count
                FROM        sys_user
                GROUP BY    client_id
            ) u
ON          c.client_id = u.client_id       
WHERE       1=1
";
            SQL += filter;
            SQL += @"
ORDER BY    name
";


            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string client_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        sys_client
WHERE       client_id = " + db_pchar + @"client_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }


        internal bool Exists(Guid client_id, string name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id.ToString()));
            myParams.Add(DB.CreateParameter("name", typeof(string), name));

            string SQL = @"
SELECT      *
FROM        sys_client
WHERE       client_id <> " + db_pchar + @"client_id
AND         name = " + db_pchar + @"name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public string GetAllNotesByClient(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        sys_client_note
WHERE       client_id = " + db_pchar + @"client_id
";
            SQL += filter;
            SQL += @"
ORDER BY    modified_date desc
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllNotes(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      n.*, c.name, 
            CASE WHEN c.status_code = 'o' THEN 'Open'
                 WHEN c.status_code = 'p' THEN 'Prospect'
                 WHEN c.status_code = 's' THEN 'Suspended'
                 WHEN c.status_code = 'c' THEN 'Closed'
                 ELSE 'Unknown' END as status_name
FROM        sys_client_note n
JOIN        sys_client c
ON          n.client_id = c.client_id
WHERE       1=1
";
            SQL += filter;
            SQL += @"
ORDER BY    n.modified_date desc
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetNoteByID(string client_note_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_note_id", typeof(string), client_note_id));

            string SQL = @"
SELECT      *
FROM        sys_client_note
WHERE       client_note_id = " + db_pchar + @"client_note_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }



    }
}