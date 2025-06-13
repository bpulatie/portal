using System;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using log4net;
using System.Configuration;
using Newtonsoft.Json;


/// <summary>
/// Summary description for member
/// </summary>
[WebService(Namespace = "http://throwdownplanner.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class sys_session_Services : System.Web.Services.WebService
{
    ILog logger = log4net.LogManager.GetLogger("SPALog");
    private string myMoniker = "Session_Services";

    SPA.spaResponse myResponse = new SPA.spaResponse();
    DataLayer.sys_session oSession = new DataLayer.sys_session();

    public sys_session_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetAll(int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oSession.GetAll(oSession.client_id, filter, pageNo, rows);
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
            myResponse.data = oSession.GetByID(id);
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
    public void GetSessionContext()
    {
        try
        {
            string session_id = HttpContext.Current.Request.Cookies["spa"]["id"];
            string user_id = HttpContext.Current.Request.Cookies["spa"]["user"];

            DataSet ds = oSession.GetSession(session_id);

            string json = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                json = BuildUserContext(ds);

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

    private string BuildUserContext(DataSet ds)
    {
        string disable_ie_backspace = "n";
        if (ConfigurationManager.AppSettings["disable_ie_backspace"] == "y")
            disable_ie_backspace = "y";

        

        string json = "{";

        json += "\"system\": {" +
                    "\"site_name\": \"" + System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName() + "\" ";
        json += "},";

        json += "\"user\": {" +
                    "\"user_id\": \"" + ds.Tables[0].Rows[0]["user_id"].ToString() + "\", " +
                    "\"last_name\": \"" + ds.Tables[0].Rows[0]["last_name"].ToString() + "\", " +
                    "\"first_name\": \"" + ds.Tables[0].Rows[0]["first_name"].ToString() + "\", " +
                    "\"email\": \"" + ds.Tables[0].Rows[0]["email"].ToString() + "\", " +
                    "\"user_type\": \"" + ds.Tables[0].Rows[0]["user_type"].ToString() + "\", " +
                    "\"style_preference\": \"" + ds.Tables[0].Rows[0]["style_preference"].ToString() + "\", " +
                    "\"menu_location\": \"" + ds.Tables[0].Rows[0]["menu_location"].ToString() + "\", " +
                    "\"disable_ie_backspace\": \"" + disable_ie_backspace + "\", " +
                    "\"last_activity_time\": \"" + ds.Tables[0].Rows[0]["last_activity_time"].ToString() + "\" ";
        json += "},";

        json += "\"client\": {" +
                    "\"client_id\": \"" + ds.Tables[0].Rows[0]["client_id"].ToString() + "\", " +
                    "\"client_name\": \"" + ds.Tables[0].Rows[0]["client_name"].ToString() + "\" ";
        json += "},";

        json += "\"accessFeatures\": [";
        for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
        {
            if (x == 0)
                json += "{";
            else
                json += ",{";

            json += "\"access_name\": \"" + ds.Tables[1].Rows[x]["access_name"].ToString() + "\" ";
            json += "}";
        }
        json += "],";

        json += "\"roles\": [";
        for (int x = 0; x < ds.Tables[2].Rows.Count; x++)
        {
            if (x == 0)
                json += "{";
            else
                json += ",{";

            json += "\"role_id\": \"" + ds.Tables[2].Rows[x]["role_id"].ToString() + "\", ";
            json += "\"role_name\": \"" + ds.Tables[2].Rows[x]["role_name"].ToString() + "\" ";
            json += "}";
        }
        json += "],";

        json += "\"sites\": [";
        for (int x = 0; x < ds.Tables[3].Rows.Count; x++)
        {
            if (x == 0)
                json += "{";
            else
                json += ",{";

            json += "\"site_id\": \"" + ds.Tables[3].Rows[x]["site_id"].ToString() + "\", ";
            json += "\"site_code\": \"" + ds.Tables[3].Rows[x]["site_code"].ToString() + "\" ";
            json += "}";
        }
        json += "],";


        string header = string.Empty;
        bool firstItem = true;

        json += "\"menu\": " + 
                "[";

        if (ds.Tables[0].Rows[0]["user_type"].ToString() == "s")
        {
            json += AddSystemAdminMenu();
            json += AddQueueAdminMenu();
            json += AddClientAdminMenu(ds);
        }

        if (ds.Tables[0].Rows[0]["user_type"].ToString() == "c")
        {
            json += AddClientAdminMenu(ds);
        }


        for (int x = 0; x < ds.Tables[4].Rows.Count; x++)
        {
            if (header != ds.Tables[4].Rows[x]["menu_name"].ToString())
            {
                firstItem = true;

                header = ds.Tables[4].Rows[x]["menu_name"].ToString();
                if (x == 0)
                {
                    json += "{\"header\": \"" + header + "\", \"items\": [";
                }
                else
                {
                    json += "]},{\"header\": \"" + header + "\", \"items\": [";
                }
            }

            if (firstItem)
            {
                json += "{";
                firstItem = false;
            }
            else
            {
                json += ",{";
            }

            json += "\"name\": \"" + ds.Tables[4].Rows[x]["menu_item_name"].ToString() + "\", ";
            json += "\"moniker\": \"" + ds.Tables[4].Rows[x]["moniker"].ToString() + "\", ";
            json += "\"mode\": \"" + ds.Tables[4].Rows[x]["menu_mode"].ToString() + "\" ";
            json += "}";
        }


        if (ds.Tables[4].Rows.Count > 0)
        {
            json += "]}";
        }

        json += "]}";

        return json;
    }

    private string AddSystemAdminMenu()
    {
        string json = string.Empty;

        json += "{\"header\": \"System\", \"items\": " +
                            "[ " +
                                "{\"name\": \"Clients\", \"moniker\": \"/portal/sys-client/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Sales Notes\", \"moniker\": \"/portal/sys-note/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Applications\", \"moniker\": \"/portal/sys-application/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Access Features\", \"moniker\": \"/portal/sys-access/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Reason Categories\", \"moniker\": \"/portal/sys-reason-category/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Event Categories\", \"moniker\": \"/portal/sys-event-category/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Metrics\", \"moniker\": \"/metric/config/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"System Options\", \"moniker\": \"/portal/sys-option/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Valid Values\", \"moniker\": \"/portal/sys-value/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Async Queues\", \"moniker\": \"/async/async-queue/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Repository\", \"moniker\": \"/portal/sys-info/detail.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Testing\", \"moniker\": \"/portal/testing/detail.htm\", \"mode\": \"0\" }" +
                            "]" +
                        "},";

        return json;
    }

    private string AddQueueAdminMenu()
    {
        string json = string.Empty;

        json += "{\"header\": \"Queue\", \"items\": " +
                            "[ " +
                                "{\"name\": \"Queue Monitor\", \"moniker\": \"/async/async-monitor/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Async Jobs\", \"moniker\": \"/async/async-job/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Async Tasks\", \"moniker\": \"/async/async-task/browse.htm\", \"mode\": \"0\" }" +
                            "]" +
                        "},";

        return json;
    }

    private string AddClientAdminMenu(DataSet ds)
    {
        string json = string.Empty;

        json += "{\"header\": \"Portal\", \"items\": " +
                            "[ " +
                                "{\"name\": \"Users\", \"moniker\": \"/portal/sys-user/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Sites\", \"moniker\": \"/portal/sys-site/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Site Groups\", \"moniker\": \"/portal/sys-site-group/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Menus\", \"moniker\": \"/portal/sys-menu/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Roles\", \"moniker\": \"/portal/sys-role/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Reason Codes\", \"moniker\": \"/portal/sys-reason/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"System Options\", \"moniker\": \"/portal/sys-option-value/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Sessions\", \"moniker\": \"/portal/sys-session/browse.htm\", \"mode\": \"0\" }," +
                                "{\"name\": \"Events\", \"moniker\": \"/portal/sys-event/browse.htm\", \"mode\": \"0\" }" +
                            "]" +
                        "}";

        if (ds.Tables[4].Rows.Count > 0)
        {
            json += ",";
        }

        return json;
    }

}


