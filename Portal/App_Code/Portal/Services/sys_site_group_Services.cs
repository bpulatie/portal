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
public class sys_site_group_Services : System.Web.Services.WebService
{
    SPA.spaResponse myResponse = new SPA.spaResponse();
    DataLayer.sys_session oSession = new DataLayer.sys_session();

    DataLayer.sys_site_group oData = new DataLayer.sys_site_group();

    public sys_site_group_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetAll(int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAll(oSession.client_id, filter, pageNo, rows);
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
    public void GetByID(string site_group_id)
    {
        try
        {
            myResponse.data = oData.GetByID(site_group_id);
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
    public void GetAssignedGroupSites(string site_group_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAssignedGroupSites(oSession.client_id, site_group_id, filter, pageNo, rows); 
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
    public void GetUnassignedGroupSites(string site_group_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetUnassignedGroupSites(oSession.client_id, site_group_id, filter, pageNo, rows); 
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
    public void AssignSiteToGroup(string site_group_id, string site_id)
    {
        try
        {
            Objects.sys_site_group_list oGroup = new Objects.sys_site_group_list();
            oGroup.site_group_id = Guid.Parse(site_group_id);
            oGroup.site_id = Guid.Parse(site_id);
            oGroup.Save();

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
    public void UnassignGroupSite(string site_group_id, string site_id)
    {
        try
        {
            Objects.sys_site_group_list oGroup = new Objects.sys_site_group_list();
            oGroup.site_group_id = Guid.Parse(site_group_id);
            oGroup.site_id = Guid.Parse(site_id);
            oGroup.Delete();

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


