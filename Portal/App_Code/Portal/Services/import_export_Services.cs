using System;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Data;
using log4net;
using System.ServiceModel;


/// <summary>
/// Summary description for member
/// </summary>
[WebService(Namespace = "http://throwdownplanner.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class import_export_Services : System.Web.Services.WebService
{
    ILog logger = log4net.LogManager.GetLogger("SPALog");
    private string myMoniker = "ImportExport";

    SPA.spaResponse myResponse = new SPA.spaResponse();

    public import_export_Services()
    {
    }

    [WebMethod]
    public string FileUpload(string site_id, string fileName, byte[] f)
    {
        Objects.sys_import_log oImport = new Objects.sys_import_log();
        DataLayer.sys_site oSite = new DataLayer.sys_site();

        try
        {
            // Validate Site Id
            DataSet ds = oSite.GetBySiteGuid(site_id);

            if (ds.Tables[0].Rows.Count < 1)
            {
                logger.Error(myMoniker + ": Unknown site guid - " + site_id);
                throw new Exception("Unknown Site Guid");
            }

            string CLIENT = ds.Tables[0].Rows[0]["client_id"].ToString();
            string SITE = ds.Tables[0].Rows[0]["site_id"].ToString();
            string DATE = DateTime.Now.ToString("yyyy-MM-dd");
            string IMPORT = Guid.NewGuid().ToString();

            // get ImportPath
            string importPath = ConfigurationManager.AppSettings["ImportPath"] + @"\" + CLIENT + @"\" + SITE + @"\" + DATE;
            string filePath = importPath + @"\" + IMPORT + "_" + fileName;

            logger.Info(myMoniker + ": importPath = " + importPath);
            logger.Info(myMoniker + ": filePath = " + filePath);

            oImport.import_id = Guid.Parse(IMPORT);
            oImport.site_id = Guid.Parse(SITE);
            oImport.client_id = Guid.Parse(CLIENT);

            oImport.filename = fileName;
            oImport.path = importPath;
            oImport.status_code = "i";

            oImport.Save();

            Directory.CreateDirectory(importPath);
            MemoryStream ms = new MemoryStream(f);
            FileStream fs = new FileStream(filePath, FileMode.Create);

            ms.WriteTo(fs);

            ms.Close();
            fs.Close();
            fs.Dispose();

            oImport.status_code = "a";
            oImport.Save();

            myResponse.data = string.Empty;
            myResponse.result = true;
            myResponse.message = "OK";
        }
        catch (Exception ex)
        {
            if (ex.Message != "Unknown Site Guid")
            {
                oImport.status_code = "e";
                oImport.comment = ex.Message.ToString();
                oImport.Save();
            }

            myResponse.data = string.Empty;
            myResponse.result = false;
            myResponse.message = ex.Message.ToString();
        }

        return JsonConvert.SerializeObject(myResponse);
    }

   
}
