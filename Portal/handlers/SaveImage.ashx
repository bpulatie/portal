<%@ WebHandler Language="C#" Class="SaveImage" %>

using System;
using System.Web;
using System.Web.Script.Serialization;

public class SaveImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        SPA.spaResponse myResponse = new SPA.spaResponse();
        DataLayer.sys_session oSession = new DataLayer.sys_session();
        
        try
        {
            string id = context.Request.QueryString["id"];
            if (id == null)
                id = Guid.NewGuid().ToString();
            
            //HTTP Context to get access to the submitted data
            HttpContext postedContext = HttpContext.Current;

            //File Collection that was submitted with posted data
            HttpFileCollection Files = postedContext.Request.Files;

            int len = Files[0].ContentLength;
            byte[] attachment = new byte[len];
            Files[0].InputStream.Read(attachment, 0, len);

            //Make sure a file was posted
            string fileName = Files[0].FileName.Substring(Files[0].FileName.LastIndexOf(@"\") + 1);

            Objects.sys_image oImage = new Objects.sys_image();
            oImage.image_id = Guid.Parse(id);
            oImage.image_name = fileName;
            oImage.the_image = attachment;
            oImage.Save();

            myResponse.data = string.Empty;
            myResponse.result = true;
            myResponse.message = "OK";            

        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.message = ex.Message;
        }

        Newtonsoft.Json.JsonConvert.SerializeObject(myResponse);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}