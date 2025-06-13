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
public class image_Services : System.Web.Services.WebService
{
    SPA.spaResponse myResponse = new SPA.spaResponse();

    public image_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void SaveFile()
    {
        try
        {
            //HTTP Context to get access to the submitted data
            HttpContext postedContext = HttpContext.Current;

            //File Collection that was submitted with posted data
            HttpFileCollection Files = postedContext.Request.Files;

            int len = Files[0].ContentLength;
            byte[] attachment = new byte[len];
            Files[0].InputStream.Read(attachment, 0, len);

            //Make sure a file was posted
            string fileName = Files[0].FileName.Substring(Files[0].FileName.LastIndexOf(@"\") + 1);

            string user_id = postedContext.Request.Form["user_id"];
            string id = postedContext.Request.Form["id"];

            if (Guid.Parse(id) == Guid.Empty)
            {
                id = Guid.NewGuid().ToString();
            }

            Objects.sys_image oImage = new Objects.sys_image();

            oImage.image_id = Guid.Parse(id);
            oImage.image_name = fileName;
            oImage.the_image = attachment;
            oImage.Save();

            DataLayer.sys_user oUser = new DataLayer.sys_user();
            oUser.UpdateImage(user_id, id);

            myResponse.result = true;
            myResponse.data = id;
            myResponse.message = "OK";

        }
        catch (Exception ex)
        {
            myResponse.result = false;
            myResponse.data = string.Empty;
            myResponse.message = ex.Message;
        }

        Context.Response.Flush();
        Context.Response.Write(JsonConvert.SerializeObject(myResponse));
    }
    
}
