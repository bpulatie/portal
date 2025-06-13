<%@ WebHandler Language="C#" Class="GetImage" %>

using System;
using System.Web;
using System.IO;

public class GetImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.Cache.SetNoStore();
        context.Response.Cache.SetExpires(DateTime.MinValue);        

        try
        {
            string id = context.Request.QueryString["id"];
            Objects.sys_image oImage = new Objects.sys_image();
            oImage.image_id = Guid.Parse(id);
            oImage.Get();
            if (oImage.image_name.EndsWith(".png"))
                context.Response.ContentType = "image/png";
            else
                context.Response.ContentType = "image/jpeg";

            context.Response.AddHeader("Content-length", oImage.the_image.Length.ToString());
            context.Response.OutputStream.Write(oImage.the_image, 0, oImage.the_image.Length);
        }
        catch(Exception ex)
        {
            MemoryStream ms = new MemoryStream();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("../images/default_avatar_male.jpg")))
            {
                fs.CopyTo(ms);
            }
                        
            context.Response.ContentType = "image/jpeg";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=default_avatar_male.jpg");
            ms.WriteTo(context.Response.OutputStream);
        }

        context.Response.Flush();
        context.Response.Close();
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}