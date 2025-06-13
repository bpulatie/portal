using System;
using System.Web;
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
public class pay_detail_Services : System.Web.Services.WebService
{
    DataLayer.pay_detail oData = new DataLayer.pay_detail();
    SPA.spaResponse myResponse = new SPA.spaResponse();
    DataLayer.sys_session oSession = new DataLayer.sys_session();

    public pay_detail_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetAllByPayPeriod(string psid, string pgid, string pay_period_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllByPayPeriod(psid, pgid, pay_period_id, filter, pageNo, rows);
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
    public void GetAllExceptionsByPayPeriod(string psid, string pay_period_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllExceptionsByPayPeriod(psid, pay_period_id, filter, pageNo, rows);
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
    public void GetExceptionByID(string pay_exception_id, int pageNo, int rows)
    {
        try
        {
            myResponse.data = oData.GetExceptionByID(pay_exception_id);
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
    public void GetAllByEmployee(string employee_id, int pageNo, int rows)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllByEmployee(employee_id, filter, pageNo, rows);
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
    public void GetPaySummary(string psid, string pgid, string pay_period_id, int pageNo, int rows)
    {
        try
        {
            //string user_id = HttpContext.Current.Request.Cookies["spa"]["user"];

            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetPaySummary(psid, pgid, pay_period_id, filter, pageNo, rows);
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
    public void GetPayDetail(string psid, string pgid, string ppid, int level, string p1, string p2, string p3, string p4)
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            switch (level)
            {
                case 1:
                    myResponse.data = oData.GetLevelOneSummary(psid, pgid, ppid);
                    break;

                case 2:
                    myResponse.data = oData.GetLevelTwoSummary(pgid, ppid, p1);
                    break;

                case 3:
                    myResponse.data = oData.GetLevelThreeSummary(pgid, ppid, p1, p2);
                    break;

                case 4:
                    myResponse.data = oData.GetLevelFourSummary(pgid, ppid, p1, p2, p3);
                    break;

                case 5:
                    myResponse.data = oData.GetLevelFiveSummary(pgid, ppid, p1, p2, p3, p4);
                    break;

                default:
                    throw new Exception("Unknown Data level supplied");
            }
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
    public void CloseAllExceptions(string pay_period_id, string exception_code, string status_code, string reason_id, string comment)
    {
        try
        {
            string user_id = HttpContext.Current.Request.Cookies["spa"]["user"];

            oData.CloseAllExceptions(pay_period_id, exception_code, status_code, reason_id, comment, user_id);

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
    public void GetAllPayCodes()
    {
        try
        {
            string filter = SPA.spaGlobals.ProcessFilters(this.Context.Request);

            myResponse.data = oData.GetAllPayCodes();
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


