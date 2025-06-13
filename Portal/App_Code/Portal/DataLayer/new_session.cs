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

    public class new_session
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_portal");
        private string db_pchar = "?";

        public new_session()
        {
            db_pchar = DB.GetParameterCharacter();
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

        public void CreateSupportSession(string client_id, string user_id)
        {
            ExpireSessions();

            Objects.sys_session ss = new Objects.sys_session();
            ss.session_id = Guid.NewGuid();
            ss.start_time = DateTime.Now;
            ss.ip_address = GetClientIpaddress();

            if (user_id == string.Empty)
            {
                ss.session_status = "q";
                ss.end_time = DateTime.Now;
            }
            else
            {
                ss.client_id = Guid.Parse(client_id);
                ss.user_id = Guid.Parse(user_id);
                ss.session_status = "l";
                ss.end_time = DateTime.Now;
            }

            ss.last_activity_time = DateTime.Now;
            ss.Save();
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

        public DataSet KeepSessionAlive(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT      *
FROM        sys_session s
WHERE       s.session_id = " + db_pchar + @"id;

SELECT      top 5 e.* 
FROM        sys_session s
JOIN        sys_event e
ON          s.client_id = e.client_id
WHERE       s.session_id = " + db_pchar + @"id
ORDER BY    e.event_date desc

";
            return DB.GetDataSet(SQL, myParams);
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