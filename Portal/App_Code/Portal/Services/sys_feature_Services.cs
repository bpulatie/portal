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
public class sys_feature_Services : System.Web.Services.WebService
{
    DataLayer.sys_feature oData = new DataLayer.sys_feature();
    DataLayer.sys_application oApp = new DataLayer.sys_application();
    SPA.spaResponse myResponse = new SPA.spaResponse();
    DataLayer.sys_session oSession = new DataLayer.sys_session();

    public sys_feature_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetAll(int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAll(filter, pageNo, rows);
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
    public void GetByID(string id)
    {
        try
        {
            myResponse.data = oData.GetByID(id);
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
    public void GetAllApplications(int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oApp.GetAll(filter, pageNo, rows);
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
    public void GetApplicationByID(string id)
    {
        try
        {
            myResponse.data = oApp.GetByID(id);
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
    public void GetAllAssignedClientFeatures(string client_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllAssignedClientFeatures(client_id, filter, pageNo, rows);
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
    public void GetAllUnassignedClientFeatures(string client_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllUnassignedClientFeatures(client_id, filter, pageNo, rows);
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
    public void UnassignFeatureFromClient(string client_id, string feature_id)
    {
        try
        {
            Objects.sys_client_feature_list oList = new Objects.sys_client_feature_list();

            oList.client_id = Guid.Parse(client_id);
            oList.feature_id = Guid.Parse(feature_id);
            oList.Delete();
 
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
    public void AssignClientFeature(string client_id, string feature_id)
    {
        try
        {
            Objects.sys_client_feature_list oList = new Objects.sys_client_feature_list();

            oList.client_feature_id = Guid.NewGuid();
            oList.client_id = Guid.Parse(client_id);
            oList.feature_id = Guid.Parse(feature_id);
            oList.Save();

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
    public void GetAllApplicationFeatures(string application_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllApplicationFeatures(application_id, filter, pageNo, rows);
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


