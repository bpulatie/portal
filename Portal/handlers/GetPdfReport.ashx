<%@ WebHandler Language="C#" Class="GetPdfReport" %>

using System;
using System.Web;
using System.IO;

public class GetPdfReport : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.Cache.SetNoStore();
        context.Response.Cache.SetExpires(DateTime.MinValue);

        //string json = new StreamReader(context.Request.InputStream).ReadToEnd();
        string user_id = context.Request.QueryString["user_id"].ToString();
        string id = context.Request.QueryString["id"].ToString();
        string pgid = context.Request.QueryString["pgid"].ToString();
        string rawJson = context.Request.QueryString["filter"].ToString();
                
        Reports.PaySummary oReport = new Reports.PaySummary();

        byte[] document = oReport.GenerateReport(user_id, pgid, id, rawJson);

        context.Response.Clear();
        context.Response.ContentType = "application/pdf";
        context.Response.AddHeader("Content-Disposition", "attachment;filename=\"FileName.pdf\"");
        context.Response.AddHeader("content-length", document.Length.ToString());
        context.Response.BinaryWrite(document);
        context.Response.Flush();
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    
}