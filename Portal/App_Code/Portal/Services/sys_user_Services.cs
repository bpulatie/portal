using System;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;


/// <summary>
/// Summary description for member
/// </summary>
[WebService(Namespace = "http://throwdownplanner.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class sys_user_Services : System.Web.Services.WebService
{
    DataLayer.sys_session oSession = new DataLayer.sys_session();
    DataLayer.sys_user oUser = new DataLayer.sys_user();
    SPA.spaResponse myResponse = new SPA.spaResponse();

    public sys_user_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetUsers(int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oUser.GetClientAll(oSession.client_id, filter, pageNo, rows);
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
    public void GetClientUsers(string client_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oUser.GetClientAll(client_id, filter, pageNo, rows);
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
    public void GetAllPaySpecialists()
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oUser.GetAllPaySpecialists();
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
    public void GetProfileUser()
    {
        try
        {
            myResponse.data = oUser.GetByID(oSession.user_id);
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
    public void GetByID(string user_id)
    {
        try
        {
            myResponse.data = oUser.GetByID(user_id);
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
    public void AssignSiteToUser(string user_id, string site_id)
    {
        try
        {
            Objects.sys_user_site_list list = new Objects.sys_user_site_list();
            list.user_id = Guid.Parse(user_id);
            list.site_id = Guid.Parse(site_id);
            list.Save();

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
    public void AssignRoleToUser(string user_id, string role_id)
    {
        try
        {
            Objects.sys_user_role_list list = new Objects.sys_user_role_list();
            list.user_id = Guid.Parse(user_id);
            list.role_id = Guid.Parse(role_id);
            list.Save();

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
    public void UnassignSiteFromUser(string user_id, string site_id)
    {
        try
        {
            oUser.UnassignSiteFromUser(user_id, site_id);

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
    public void UnassignRoleFromUser(string user_id, string role_id)
    {
        try
        {
            oUser.UnassignRoleFromUser(user_id, role_id);

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

}


