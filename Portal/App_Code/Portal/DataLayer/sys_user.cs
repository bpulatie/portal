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

    public class sys_user
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public sys_user()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      * 
FROM        sys_user
WHERE       1=1 
";

            SQL += filter;
            SQL += @"
ORDER BY    last_name, first_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetClientAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *, 
            CASE WHEN user_type = 's' THEN 'System Admin' 
                 WHEN user_type = 'c' THEN 'Client Admin'
                 ELSE 'Standard User' END as user_type_name
FROM        sys_user
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    last_name, first_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllPaySpecialists()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      u.*
FROM        sys_option o
JOIN		sys_role r
ON			r.external_name = o.value 
JOIN        sys_user_role_list l
ON          r.role_id = l.role_id
JOIN        sys_user u
ON          l.user_id = u.user_id
WHERE       o.option_name = 'Pay Specialist External Name'
ORDER BY    last_name, first_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       user_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public DataSet GetByEmployeeNo(string num)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("num", typeof(string), num));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       login_name = " + db_pchar + @"num";

            return DB.GetDataSet(SQL, myParams);
        }

        public DataSet GetByEmail(string email)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("email", typeof(string), email));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       email = " + db_pchar + @"email 
";

            return DB.GetDataSet(SQL, myParams);
        }

        public DataSet GetByLoginNameAndPassword(string login_name, string password)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("login_name", typeof(string), login_name));
            myParams.Add(DB.CreateParameter("password", typeof(string), password));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       login_name = " + db_pchar + @"login_name 
AND         password = " + db_pchar + @"password";

            return DB.GetDataSet(SQL, myParams);
        }

        public DataSet GetByLoginName(string login_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("login_name", typeof(string), login_name));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       login_name = " + db_pchar + @"login_name 
";

            return DB.GetDataSet(SQL, myParams);
        }

        public DataSet GetByName(string name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("last_name", typeof(string), name));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       last_name = " + db_pchar + @"last_name ";

            return DB.GetDataSet(SQL, myParams);
        }

        public string Register(string email, string password, string firstname, string lastname)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("email", typeof(string), email));
            myParams.Add(DB.CreateParameter("password", typeof(string), password));

            string SQL = @"
SELECT      *
FROM        sys_user 
WHERE       email = " + db_pchar + @"email 
AND         password = " + db_pchar + @"password";

            DataSet rs = DB.GetDataSet(SQL, myParams);
            try
            {
                if (rs.Tables[0].Rows.Count > 0)
                    throw new Exception("Email already in use");
            }
            catch(Exception ex)
            {
                return "Unexpected Error: " + ex.Message;
            }

            Objects.sys_user myUser = new Objects.sys_user();
            myUser.user_id = Guid.NewGuid();
            myUser.email = email;
            myUser.password = password;
            myUser.user_type = "m";
            myUser.last_name = lastname;
            myUser.first_name = firstname;

            try
            {
                myUser.Save();
            }
            catch(Exception ex)
            {
                return "Unexpected Error: " + ex.Message;
            }

            return myUser.user_id.ToString();
        }

        public int UnassignSiteFromUser(string user_id, string site_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id));

            string SQL = @"
DELETE    
FROM        sys_user_site_list
WHERE       user_id = " + db_pchar + @"user_id
AND         site_id = " + db_pchar + @"site_id";

            return DB.ExecuteSQL(SQL, myParams);
        }

        public int UnassignRoleFromUser(string user_id, string role_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("role_id", typeof(string), role_id));

            string SQL = @"
DELETE    
FROM        sys_user_role_list
WHERE       user_id = " + db_pchar + @"user_id
AND         role_id = " + db_pchar + @"role_id";

            return DB.ExecuteSQL(SQL, myParams);
        }


        internal void UpdatePassword(string user_id, string random, bool temp = true)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("random", typeof(string), random));

            if (temp == true)
                myParams.Add(DB.CreateParameter("temp", typeof(string), "y"));
            else
                myParams.Add(DB.CreateParameter("temp", typeof(string), "n"));

            string SQL = @"
UPDATE      sys_user
SET         password = " + db_pchar + @"random,
            temp_password = " + db_pchar + @"temp
WHERE       user_id = " + db_pchar + @"user_id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        internal bool Exists(Guid user_id, string login_name)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id.ToString()));
            myParams.Add(DB.CreateParameter("login_name", typeof(string), login_name));

            string SQL = @"
SELECT      *
FROM        sys_user
WHERE       user_id <> " + db_pchar + @"user_id
AND         login_name = " + db_pchar + @"login_name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            if (ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal string GetSitesByUser(string client_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      s.site_id, s.site_code, s.name, c.name as client_name
FROM        sys_user_site_list l
JOIN        sys_site s
ON          l.site_id = s.site_id
JOIN        sys_client c
ON          s.client_id = c.client_id
WHERE       l.user_id = " + db_pchar + @"user_id
ORDER BY    c.name, s.name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);
            
            string json = "{sites: [";
            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                if (x == 0)
                    json += "{";
                else
                    json += ",{";

                json += "site_id: '" + ds.Tables[0].Rows[x]["site_id"].ToString() + "', ";
                json += "site_code: '" + ds.Tables[0].Rows[x]["site_code"].ToString() + "', ";
                json += "name: '" + ds.Tables[0].Rows[x]["name"].ToString() + "', ";
                json += "client: '" + ds.Tables[0].Rows[x]["client_name"].ToString() + "' ";
                json += "}";
            }

            json += "]}";
            return json;
        }

        internal object GetAllSitesByClient(string client_id, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT      s.site_id, s.site_code, s.name, c.name as client_name
FROM        sys_site s
JOIN        sys_client c
ON          s.client_id = c.client_id
ORDER BY    c.name, s.name
";

            DataSet ds = DB.GetDataSet(SQL, myParams);

            string json = "{sites: [";
            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                if (x == 0)
                    json += "{";
                else
                    json += ",{";

                json += "site_id: '" + ds.Tables[0].Rows[x]["site_id"].ToString() + "', ";
                json += "site_code: '" + ds.Tables[0].Rows[x]["site_code"].ToString() + "', ";
                json += "name: '" + ds.Tables[0].Rows[x]["name"].ToString() + "', ";
                json += "client: '" + ds.Tables[0].Rows[x]["client_name"].ToString() + "' ";
                json += "}";
            }

            json += "]}";
            return json;
        }

        internal string LinkSite(string client_id, string user_id, string site_id)
        {
            string site_guid = Guid.NewGuid().ToString();

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("site_guid", typeof(string), site_guid));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id));

            string SQL = @"
UPDATE      sys_site 
SET         site_guid = " + db_pchar + @"site_guid
WHERE       site_id = " + db_pchar + @"site_id
";
            DB.ExecuteSQL(SQL, myParams);

            return site_guid;
        }

        internal void UpdateImage(string user_id, string image_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("image_id", typeof(string), image_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
UPDATE      sys_user 
SET         image_id = " + db_pchar + @"image_id
WHERE       user_id = " + db_pchar + @"user_id
";
            DB.ExecuteSQL(SQL, myParams);
        }
    }
}