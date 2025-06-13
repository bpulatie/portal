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

    public class sys_session
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public string session_id;
        public string client_id;
        public string user_id;

        public sys_session()
        {
            db_pchar = DB.GetParameterCharacter();

            if (HttpContext.Current.Request.Cookies["spa"] == null)
                throw new Exception("No Session Information - Restart your Browser");

            session_id = HttpContext.Current.Request.Cookies["spa"]["id"];
            client_id = HttpContext.Current.Request.Cookies["spa"]["client"];
            user_id = HttpContext.Current.Request.Cookies["spa"]["user"];

            DataSet ds = GetBySessionID(session_id);

            if (ds.Tables[0].Rows.Count < 1)
                throw new Exception("Session Does not Exist");

            if (ds.Tables[0].Rows[0]["user_id"].ToString() != user_id)
                throw new Exception("Invalid Session, bad user");

            if (ds.Tables[0].Rows[0]["session_status"].ToString() != "a")
                throw new Exception("Session Expired - Restart your Browser");

            UpdateSession(session_id);
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      s.*, u.last_name, u.first_name, 
            CASE session_status WHEN 'a' THEN 'Active Session'
                                WHEN 'b' THEN 'Session Ended - Browser Closed' 
                                WHEN 'e' THEN 'Session Ended - User Signed Out' 
                                WHEN 'd' THEN 'Session Denied - User Already Signed In' 
                                WHEN 'x' THEN 'Session Expired' 
                                WHEN 'g' THEN 'Session Denied - User has no AD Groups' 
                                WHEN 'u' THEN 'Session Denied - Unknown User' 
                                WHEN 'm' THEN 'Session Denied - Session Already Exists' 
                                ELSE 'Unknown Status' 
                                END as status_name
FROM        sys_session s
LEFT JOIN   sys_user u
ON          s.user_id = u.user_id
WHERE       s.client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    start_time desc";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      s.*, u.last_name, u.first_name, 
            CASE session_status WHEN 'a' THEN 'Active Session'
                                WHEN 'b' THEN 'Session Ended - Browser Closed' 
                                WHEN 'e' THEN 'Session Ended - User Signed Out' 
                                WHEN 'd' THEN 'Session Denied - User Already Signed In' 
                                WHEN 'x' THEN 'Session Expired' 
                                WHEN 'g' THEN 'Session Denied - User has no AD Groups' 
                                WHEN 'u' THEN 'Session Denied - Unknown User' 
                                WHEN 'm' THEN 'Session Denied - Session Already Exists' 
                                ELSE 'Unknown Status' 
                                END as status_name 
FROM        sys_session s
LEFT JOIN   sys_user u
ON          s.user_id = u.user_id
WHERE       s.session_id = " + db_pchar + @"id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public DataSet GetBySessionID(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      s.*, u.last_name, u.first_name
FROM        sys_session s
LEFT JOIN   sys_user u
ON          s.user_id = u.user_id
WHERE       s.session_id = " + db_pchar + @"id";

            return DB.GetDataSet(SQL, myParams);
        }

        public void UpdateSession(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));
            myParams.Add(DB.CreateParameter("time", typeof(DateTime), DateTime.Now));

            string SQL = @"
UPDATE      sys_session 
SET         last_activity_time = " + db_pchar + @"time
WHERE       session_id = " + db_pchar + @"id";

            DB.ExecuteSQL(SQL, myParams);
        }
        
        public DataSet GetSession(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      u.user_id, u.last_name, u.first_name, u.email, u.user_type, u.style_preference, u.menu_location, u.login_name, s.last_activity_time, c.client_id, c.name as client_name  
FROM        sys_session s
JOIN		sys_user u
ON          s.user_id = u.user_id
JOIN		sys_client c
ON          u.client_id = c.client_id
WHERE       s.session_id = " + db_pchar + @"id;

SELECT      a.access_name
FROM        sys_session s
JOIN		sys_user_role_list l
ON          s.user_id = l.user_id
JOIN		sys_role r
ON          l.role_id = r.role_id
JOIN		sys_role_access_list m
ON          r.role_id = m.role_id
JOIN		sys_access a
ON          m.access_id = a.access_id
WHERE       s.session_id = " + db_pchar + @"id;

SELECT      r.role_id, r.role_name  
FROM        sys_session s
JOIN		sys_user u
ON          s.user_id = u.user_id
JOIN		sys_user_role_list l
ON          s.user_id = l.user_id
JOIN		sys_role r
ON          l.role_id = r.role_id
WHERE       s.session_id = " + db_pchar + @"id;

SELECT      b.site_id, b.site_code, b.name as bu_name  
FROM        sys_session s
JOIN		sys_user u
ON          s.user_id = u.user_id
JOIN		sys_user_site_list l
ON          s.user_id = l.user_id
JOIN		sys_site b
ON          l.site_id = b.site_id
WHERE       s.session_id = " + db_pchar + @"id;

SELECT      m.menu_name, i.menu_item_name, f.moniker, MIN(i.menu_mode) as menu_mode, 
			MIN(m.sort_order) as header_order, MIN(i.sort_order) as item_order
FROM        sys_role_menu_list l
JOIN		sys_menu m
ON			l.menu_id = m.menu_id
JOIN		sys_menu_item i
ON			l.menu_id = i.menu_id
JOIN        sys_feature f
ON          i.feature_id = f.feature_id
WHERE       l.role_id in (
							SELECT      r.role_id  
							FROM        sys_session s
							JOIN		sys_user u
							ON          s.user_id = u.user_id
							JOIN		sys_user_role_list l
							ON          s.user_id = l.user_id
							JOIN		sys_role r
							ON          l.role_id = r.role_id
							WHERE       s.session_id = " + db_pchar + @"id
                         )
GROUP BY	m.menu_name, i.menu_item_name, f.moniker
ORDER BY    header_order, m.menu_name, item_order, i.menu_item_name;
";

            return DB.GetDataSet(SQL, myParams);
        }

        public void CreateSession(string client_id, string user_id, bool groups = true)
        {
            ExpireSessions();

            Objects.sys_session ss = new Objects.sys_session();
            ss.session_id = Guid.NewGuid();
            ss.start_time = DateTime.Now;
            ss.ip_address = GetClientIpaddress();

            if (user_id == string.Empty)
            {
                ss.session_status = "u";
                ss.end_time = DateTime.Now;
            }
            else
            {
                if (HttpContext.Current.Request.Cookies["spa"] != null)
                {
                    ss.session_status = "m";
                    ss.client_id = Guid.Parse(client_id);
                    ss.user_id = Guid.Parse(user_id);
                    ss.last_activity_time = DateTime.Now;
                    ss.Save();

                    throw new Exception("Active Session exists on this machine");
                }

                ArrayList myParams = new ArrayList();
                myParams.Add(DB.CreateParameter("id", typeof(string), user_id));

                string SQL = @"
SELECT      * 
FROM        sys_session 
WHERE       user_id = " + db_pchar + @"id
AND			session_status = 'a'
";

                DataSet ds = DB.GetDataSet(SQL, myParams);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ip_address"].ToString() != ss.ip_address.ToString())
                    {
                        ss.session_status = "d";
                        ss.user_id = Guid.Parse(user_id);
                        ss.last_activity_time = DateTime.Now;
                        ss.Save();

                        throw new Exception("User Already has an Active Session");
                    }
                    else
                    {
                        // Reset User Session (Means end it with 'r')
                        ResetUserSession(ds.Tables[0].Rows[0]["session_id"].ToString(), user_id);
                    }
                }

                if (groups == true)
                {
                    ss.session_status = "a";
                    ss.client_id = Guid.Parse(client_id);
                    ss.user_id = Guid.Parse(user_id);
                }
                else
                {
                    ss.session_status = "g";
                    ss.end_time = DateTime.Now;
                }
            }

            ss.last_activity_time = DateTime.Now;

            ss.Save();


            if (user_id != string.Empty)
            {
                //HttpContext.Current.Response.Cookies["spa"].Expires = DateTime.Now.AddMinutes(10);
                HttpContext.Current.Response.Cookies["spa"]["id"] = ss.session_id.ToString();
                HttpContext.Current.Response.Cookies["spa"]["client"] = ss.client_id.ToString();
                HttpContext.Current.Response.Cookies["spa"]["user"] = ss.user_id.ToString();
                HttpContext.Current.Response.Cookies["spa"].HttpOnly = true;
            }

        }

        private void ExpireSessions()
        {
            string mySQL = @"
UPDATE      sys_session
SET         session_status = 'x', end_time = getdate()
WHERE       session_status = 'a'
AND			last_activity_time < dateadd(minute, -15, getdate())
";

            ArrayList myParams1 = new ArrayList();
            DB.ExecuteSQL(mySQL, myParams1);
        }


        public void EndSession(string status)
        {
            if (HttpContext.Current.Request.Cookies["spa"] == null)
                return;
            
            string session_id = HttpContext.Current.Request.Cookies["spa"]["id"];
            string user_id = HttpContext.Current.Request.Cookies["spa"]["user"];

            HttpContext.Current.Response.Cookies["spa"].Expires = DateTime.Now.AddDays(-1);

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), session_id));
            myParams.Add(DB.CreateParameter("time", typeof(DateTime), DateTime.Now));
            myParams.Add(DB.CreateParameter("status", typeof(string), status));

            string SQL = @"
UPDATE      sys_session 
SET         end_time = " + db_pchar + @"time, 
            session_status = " + db_pchar + @"status
WHERE       session_id = " + db_pchar + @"id";

            DB.ExecuteSQL(SQL, myParams);
        }

        public void ResetUserSession(string session_id, string user_id)
        {

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), session_id));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("time", typeof(DateTime), DateTime.Now));
            myParams.Add(DB.CreateParameter("status", typeof(string), 'r'));

            string SQL = @"
UPDATE      sys_session 
SET         end_time = " + db_pchar + @"time, 
            session_status = " + db_pchar + @"status
WHERE       session_id = " + db_pchar + @"id
AND         user_id = " + db_pchar + @"user_id";

            DB.ExecuteSQL(SQL, myParams);
        }

        private string GetClientIpaddress()
        {
            string ipAddress = string.Empty;
            ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ipAddress == "" || ipAddress == null)
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                return ipAddress;
            }
            else
            {
                return ipAddress;
            }
        }

    }
}