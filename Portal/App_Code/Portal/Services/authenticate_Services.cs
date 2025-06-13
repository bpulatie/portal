using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Security.Principal;
using System.DirectoryServices.AccountManagement;
using log4net;
using System.DirectoryServices;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.ServiceModel.Channels;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;

/// <summary>
/// Summary description for member
/// </summary>
[WebService(Namespace = "http://throwdownplanner.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class authenticate_Services : System.Web.Services.WebService
{
    ILog logger = log4net.LogManager.GetLogger("SPALog");
    private string myMoniker = "Authenticate";

    DataLayer.sys_user oUser = new DataLayer.sys_user();
    DataLayer.sys_role oRole = new DataLayer.sys_role();
    SPA.spaResponse myResponse = new SPA.spaResponse();
    DataLayer.new_session oSession = new DataLayer.new_session();

    public authenticate_Services()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void SignIn(string login_name, string password)
    {
        try
        {
            if (login_name.ToUpper() != "SYSADMIN")
            {
                if (ConfigurationManager.AppSettings["Direct_Login"] != "y")
                    throw new Exception("Direct Login not Allowed");
            }

            byte[] hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            String pword = System.Text.Encoding.Default.GetString(hash); 

            DataSet ds = oUser.GetByLoginNameAndPassword(login_name, pword);

            if (ds.Tables[0].Rows.Count > 0)
            {
                oSession.CreateSession(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString());

                myResponse.data = ds.Tables[0].Rows[0]["temp_password"].ToString();
                myResponse.result = true;
                myResponse.message = "OK";
            }
            else
            {
                oSession.CreateSession(string.Empty, string.Empty);

                myResponse.data = string.Empty;
                myResponse.result = false;
                myResponse.message = "Invalid Username or Password";
            }
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
            logger.Error(myMoniker + ": SignIn " + ex.Message);
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void ADFS()
    {
        WSTrustChannelFactory factory = new WSTrustChannelFactory(
                        WSTrust13Bindings.UsernameMixed,
                        new EndpointAddress("https://adfs.murphyusa.com/ls"));

        factory.TrustVersion = TrustVersion.WSTrust13;
        RequestSecurityToken rst = new RequestSecurityToken
        {
            RequestType = RequestTypes.Issue,
            KeyType = KeyTypes.Bearer,
            AppliesTo = new EndpointReference("https://test.globalrdm.com/main.htm")
        };

        //SecurityToken issuedToken = factory.CreateChannel().Issue(rst);

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void SSO()
    {
        try
        {
            if (ConfigurationManager.AppSettings["SSO_Login"] != "y")
                throw new Exception("SSO Login not enabled");
            
            logger.Debug(myMoniker + ": SSO ");
            WindowsIdentity identity = HttpContext.Current.Request.LogonUserIdentity;

            string name = identity.Name;
            logger.Debug(myMoniker + ": named = " + name);

            string domain = name.Substring(0, name.IndexOf(@"\"));
            logger.Debug(myMoniker + ": Domain = " + domain);

            string[] temp = name.Split('\\');
            string userName = temp[1];

            logger.Debug(myMoniker + ": UserName = " + userName);

            // establish domain context
            PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);

            // find your user
            UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);

            // if found - get groups
            if (user != null)
            {
                string empNo = user.EmployeeId;
                logger.Debug(myMoniker + ": EmployeeId = " + empNo);

                if (empNo.Length < 6)
                {
                    empNo = ("000000" + empNo).Substring(empNo.Length);
                    logger.Debug(myMoniker + ": EmployeeId = " + empNo);
                }

                DataSet ds = oUser.GetByEmployeeNo(empNo);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    logger.Debug(myMoniker + ": EmployeeId not Found");

                    string email = user.EmailAddress;
                    logger.Debug(myMoniker + ": Trying Email = " + email);

                    ds = oUser.GetByEmail(email);
                }
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    logger.Debug(myMoniker + ": User Found in AD");

                    // Delete user role relationships
                    oRole.DeleteUserRolesForUser(ds.Tables[0].Rows[0]["user_id"].ToString());
                    
                    List<string> groups = GetAdGroupsForUser(userName, domain);
                    List<string> roles = new List<string>();

                    foreach (string p in groups)
                    {
                        logger.Debug(myMoniker + ": UserName = " + userName + " - Group Name: " + p);

                        DataSet ds1 = oRole.GetRoleByExternalName(p);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            roles.Add(ds1.Tables[0].Rows[0]["role_id"].ToString());
                        }
                    }

                    if (roles.Count() > 0)
                    {
                        oSession.CreateSession(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString());

                        foreach (string role in roles)
                        {
                            Objects.sys_user_role_list oList = new Objects.sys_user_role_list();
                            oList.role_id = Guid.Parse(role);
                            oList.user_id = Guid.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                            oList.creation_date = DateTime.Now;
                            oList.Save();
                        }

                        myResponse.data = oUser.GetByID(ds.Tables[0].Rows[0]["user_id"].ToString());
                        myResponse.result = true;
                        myResponse.message = "OK";
                    }
                    else
                    {
                        oSession.CreateSession(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString(), false);

                        myResponse.data = string.Empty;
                        myResponse.result = false;
                        myResponse.message = "No Valid User Groups";
                        logger.Error(myMoniker + ": No Valid User Groups = " + userName);
                    }
                }
                else
                {
                    oSession.CreateSession(string.Empty, string.Empty);

                    myResponse.data = string.Empty;
                    myResponse.result = false;
                    myResponse.message = "Unknown User";
                    logger.Error(myMoniker + ": User not in database = " + userName);
                }
            }
            else
            {
                oSession.CreateSession(string.Empty, string.Empty);

                myResponse.data = string.Empty;
                myResponse.result = false;
                myResponse.message = "Invalid User Credentials";
                logger.Error(myMoniker + ": Invalid user - nothing found in AD " + userName);
            }
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
            logger.Error(myMoniker + ": Unexpected error " + ex.Message);
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    public static List<string> GetAdGroupsForUser(string userName, string domainName = null)
    {
        var result = new List<string>();

        if (userName.Contains('\\') || userName.Contains('/'))
        {
            domainName = userName.Split(new char[] { '\\', '/' })[0];
            userName = userName.Split(new char[] { '\\', '/' })[1];
        }

        using (PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domainName))
        using (UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, userName))
        using (var searcher = new DirectorySearcher(new DirectoryEntry("LDAP://" + domainContext.Name)))
        {
            searcher.Filter = String.Format("(&(objectCategory=group)(member={0}))", user.DistinguishedName);
            searcher.SearchScope = SearchScope.Subtree;
            searcher.PropertiesToLoad.Add("cn");

            foreach (SearchResult entry in searcher.FindAll())
                if (entry.Properties.Contains("cn"))
                    result.Add(entry.Properties["cn"][0].ToString());
        }

        return result;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void ADLookup(string email)
    {
        try
        {
            logger.Debug(myMoniker + ": ADLookup ");

            string[] temp = email.Split('\\');
            string userName = temp[1];
            string domain = temp[0];

            logger.Debug(myMoniker + ": UserName = " + userName);

            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domain);
            UserPrincipal user = new UserPrincipal(domainContext);

            user.SamAccountName = userName;
            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;
            PrincipalSearchResult<Principal> results = pS.FindAll();

            logger.Debug(myMoniker + ": Result Count = " + results.Count().ToString());

            if (results != null && results.Count() > 0)
            {
                DataSet ds = oUser.GetByEmployeeNo(results.First().Description);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    oSession.CreateSession(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString());

                    myResponse.data = oUser.GetByID(ds.Tables[0].Rows[0]["user_id"].ToString());
                    myResponse.result = true;
                    myResponse.message = "OK";
                }
                else
                {
                    oSession.CreateSession(string.Empty, string.Empty);

                    myResponse.data = string.Empty;
                    myResponse.result = false;
                    myResponse.message = "Unknown User";
                    logger.Debug(myMoniker + ": User not in database = " + userName);
                }
            }
            else
            {
                oSession.CreateSession(string.Empty, string.Empty);

                myResponse.data = string.Empty;
                myResponse.result = false;
                myResponse.message = "Invalid User Credentials";
                logger.Debug(myMoniker + ": Invalid user - nothing found in AD " + userName);
            }
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
            logger.Error(myMoniker + ": Unexpected error " + ex.Message);
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void SignOut()
    {
        try
        {
            oSession.EndSession("e");

            myResponse.data = string.Empty;
            myResponse.result = true;
            myResponse.message = "OK";
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void BrowserClosed()
    {
        try
        {
            oSession.EndSession("b");

            myResponse.data = string.Empty;
            myResponse.result = true;
            myResponse.message = "OK";
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void KeepSessionAlive()
    {
        try
        {
            string session_id = HttpContext.Current.Request.Cookies["spa"]["id"];
            string user_id = HttpContext.Current.Request.Cookies["spa"]["user"];

            DataSet ds = oSession.KeepSessionAlive(session_id);

            string json = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                json = BuildUpdateSession(ds);

                myResponse.data = json;
                myResponse.result = true;
                myResponse.message = "OK";
            }
            else
            {
                myResponse.data = string.Empty;
                myResponse.result = false;
                myResponse.message = "Bad Session Information";
            }
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
            logger.Error(myMoniker + ": Unexpected error " + ex.Message);
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void TemporaryPassword(string login_name)
    {
        try
        {
            DataSet ds = oUser.GetByLoginName(login_name);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["email"].ToString().Length < 10)
                {
                    myResponse.data = string.Empty;
                    myResponse.result = true;
                    myResponse.message = "No email on record - please contact support";
                }
                else
                {
                    string name = ds.Tables[0].Rows[0]["first_name"].ToString() + " " + ds.Tables[0].Rows[0]["last_name"].ToString();
                    string email = ds.Tables[0].Rows[0]["email"].ToString();
                    string pword = ResetPassword(ds.Tables[0].Rows[0]["user_id"].ToString());

                    SendResetPasswordEmail(name, email, pword);

                    myResponse.data = string.Empty;
                    myResponse.result = true;
                    myResponse.message = "A temporary password has been emailed";
                }
            }
            else
            {
                throw new Exception("Bad Username - please contact support");
            }
        }
        catch (Exception ex)
        {
            myResponse.data = string.Empty;
            myResponse.result = false;
            myResponse.message = "Unable to reset password - please contact support";
            logger.Error(myMoniker + ": Unexpected error " + ex.Message);
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void UpdatePassword(string user_id, string password)
    {
        try
        {
            String pword = SPA.spaGlobals.GetHashedPassword(password);
            oUser.UpdatePassword(user_id, pword, false);

            myResponse.data = string.Empty;
            myResponse.result = true;
            myResponse.message = "OK";
        }
        catch (Exception ex)
        {
            myResponse.data = string.Empty;
            myResponse.result = false;
            myResponse.message = "Unable to save password - please contact support";
            logger.Error(myMoniker + ": Unexpected error " + ex.Message);
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }
    
    private string ResetPassword(string user_id)
    {
        String random = Membership.GeneratePassword(6, 3);
        String pword = SPA.spaGlobals.GetHashedPassword(random);

        oUser.UpdatePassword(user_id, pword, true);
        
        return random;
    }

    public void SendResetPasswordEmail(string name, string email, string password)
    {
        string body = "<table>" +
                            "<tr><td>Hello " + name + ",</td></tr>" +
                            "<tr><td>&nbsp;</td></tr>" +
                            "<tr><td>You requested a password reset.</td></tr>" +
                            "<tr><td>Your new temporary password is " + password + "</td></tr>" +
                    "</table>";

        SPA.spaGlobals.SendMail(email, "Password Reset", body);
    }

    private string BuildUpdateSession(DataSet ds)
    {
        string json = "{";

        json += "\"session\": {" +
                    "\"last_activity_time\": \"" + ds.Tables[0].Rows[0]["last_activity_time"].ToString() + "\" ";
        json += "},";

        json += "\"events\": [";
        for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
        {
            if (x == 0)
                json += "{";
            else
                json += ",{";

            json += "\"event_id\": \"" + ds.Tables[1].Rows[x]["event_id"].ToString() + "\", ";
            json += "\"event_date\": \"" + ds.Tables[1].Rows[x]["event_date"].ToString() + "\", ";
            json += "\"event_category\": \"" + ds.Tables[1].Rows[x]["event_category"].ToString() + "\", ";
            json += "\"event_summary\": \"" + ds.Tables[1].Rows[x]["event_summary"].ToString() + "\", ";
            json += "\"event_type\": \"" + ds.Tables[1].Rows[x]["event_type"].ToString() + "\" ";
            json += "}";
        }
        json += "]";
        json += "}";

        return json;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string SupportSignIn(string login_name, string password)
    {
        try
        {
            byte[] hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            String pword = System.Text.Encoding.Default.GetString(hash);

            DataSet ds = oUser.GetByLoginNameAndPassword(login_name, pword);

            if (ds.Tables[0].Rows.Count > 0)
            {
                oSession.CreateSupportSession(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString());

                if (login_name.ToUpper() != "SYSADMIN")
                {
                    myResponse.data = oUser.GetSitesByUser(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                else
                {
                    myResponse.data = oUser.GetAllSitesByClient(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                myResponse.result = true;
                myResponse.message = "OK";
            }
            else
            {
                oSession.CreateSupportSession(string.Empty, string.Empty);

                myResponse.data = string.Empty;
                myResponse.result = false;
                myResponse.message = "Invalid Username or Password";
            }
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
            logger.Error(myMoniker + ": SignIn " + ex.Message);
        }

        return JsonConvert.SerializeObject(myResponse);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public string LinkSite(string login_name, string password, string site_id)
    {
        try
        {
            byte[] hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            String pword = System.Text.Encoding.Default.GetString(hash);

            DataSet ds = oUser.GetByLoginNameAndPassword(login_name, pword);

            if (ds.Tables[0].Rows.Count > 0)
            {
                myResponse.data = oUser.LinkSite(ds.Tables[0].Rows[0]["client_id"].ToString(), ds.Tables[0].Rows[0]["user_id"].ToString(), site_id);
                myResponse.result = true;
                myResponse.message = "OK";
            }
            else
            {
                myResponse.data = string.Empty;
                myResponse.result = false;
                myResponse.message = "Invalid Username or Password";
            }
        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = "Invalid Username or Password";
            logger.Error(myMoniker + ": LinkSite " + ex.Message);
        }

        return JsonConvert.SerializeObject(myResponse);
    }

    public static class WSTrust13Bindings
    {
        /// <summary>
        /// Binding to talk to kerberosmixed endpoint
        /// </summary>
        public static Binding KerberosMixed
        {
            get { return CreateKerberosBinding(SecurityMode.TransportWithMessageCredential); }
        }

        /// <summary>
        /// Binding to talk to username endpoint
        /// </summary>
        public static Binding Username
        {
            get { return CreateUserNameBinding(SecurityMode.Message); }
        }

        /// <summary>
        /// Binding to talk to usernamebasictransport endpoint
        /// </summary>
        public static Binding UsernameBasicTransport
        {
            get { return CreateUserNameBinding(SecurityMode.Transport); }
        }

        /// <summary>
        /// Binding to talk to usernamemixed endpoint
        /// </summary>
        public static Binding UsernameMixed
        {
            get { return CreateUserNameBinding(SecurityMode.TransportWithMessageCredential); }
        }

        /// <summary>
        /// Creates a username binding with the specified security mode.
        /// </summary>
        private static Binding CreateUserNameBinding(SecurityMode securityMode)
        {
            if (securityMode == SecurityMode.None)
                throw new ArgumentException("securityMode None is not allowed");

            BindingElementCollection bindingElements = new BindingElementCollection();

            // Add securtiy binding element
            if (securityMode == SecurityMode.Message)
                bindingElements.Add(
                    SecurityBindingElement.CreateUserNameForCertificateBindingElement());
            else if (securityMode == SecurityMode.TransportWithMessageCredential)
                bindingElements.Add(
                    SecurityBindingElement.CreateUserNameOverTransportBindingElement());

            // Add encoding binding element
            bindingElements.Add(CreateEncodingBindingElement());

            // Add transport binding element
            HttpTransportBindingElement transportBindingElement =
                CreateTransportBindingElement(securityMode);

            if (securityMode == SecurityMode.Transport)
                transportBindingElement.AuthenticationScheme = AuthenticationSchemes.Basic;
            else
                transportBindingElement.AuthenticationScheme = AuthenticationSchemes.Digest;

            bindingElements.Add(transportBindingElement);

            // Create binding
            return new CustomBinding(bindingElements);
        }

        /// <summary>
        /// Creates a kerberos binding with the specified security mode.
        /// On ADFS 2.0 only TransportWithMessageCredential is available.
        /// </summary>
        private static Binding CreateKerberosBinding(SecurityMode securityMode)
        {
            if (securityMode == SecurityMode.None)
                throw new ArgumentException("securityMode None is not allowed");

            BindingElementCollection bindingElements = new BindingElementCollection();

            // Add securtiy binding element
            if (securityMode == SecurityMode.Message)
                bindingElements.Add(
                    SecurityBindingElement.CreateKerberosBindingElement());
            else if (securityMode == SecurityMode.TransportWithMessageCredential)
                bindingElements.Add(
                    SecurityBindingElement.CreateKerberosOverTransportBindingElement());

            // Add encoding binding element
            bindingElements.Add(CreateEncodingBindingElement());

            // Add transport binding element
            HttpTransportBindingElement transportBindingElement =
                CreateTransportBindingElement(securityMode);

            transportBindingElement.AuthenticationScheme = AuthenticationSchemes.Negotiate;
            bindingElements.Add(transportBindingElement);

            // Create binding
            return new CustomBinding(bindingElements);
        }

        private static HttpTransportBindingElement CreateTransportBindingElement(SecurityMode securityMode)
        {
            // Create transport binding element
            if (securityMode == SecurityMode.Message)
                return new HttpTransportBindingElement();
            else
                return new HttpsTransportBindingElement();
        }

        private static TextMessageEncodingBindingElement CreateEncodingBindingElement()
        {
            // Create encoding binding element
            TextMessageEncodingBindingElement encodingBindingElement =
                new TextMessageEncodingBindingElement();

            encodingBindingElement.ReaderQuotas.MaxArrayLength = 2097152;
            encodingBindingElement.ReaderQuotas.MaxStringContentLength = 2097152;

            return encodingBindingElement;
        }
    }

}


