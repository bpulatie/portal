using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using Newtonsoft.Json;


/// <summary>
/// Summary description for member
/// </summary>
[WebService(Namespace = "http://throwdownplanner.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class requirement_Services : System.Web.Services.WebService
{

    JavaScriptSerializer serializer = new JavaScriptSerializer();
    DataLayer.requirement oData = new DataLayer.requirement();
    SPA.spaResponse myResponse = new SPA.spaResponse();

    public requirement_Services()
    {
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetTopLevelRequirements()
    {
        try
        {
            myResponse.data = oData.GetTopLevelRequirements();
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
    public void GetRequirementById(string id)
    {
        try
        {
            myResponse.data = oData.GetRequirementById(id);
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
    public void GetChildRequirements(string id)
    {
        try
        {
            String Header = oData.GetRequirementById(id);
            String Children = oData.ChildRequirements(id);

            myResponse.data = "{ \"Header\": " + Header + ", \"Children\": " + Children + "}";
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
    public void GetRequirementOnly(string id)
    {
        try
        {
            String Header = oData.GetRequirementById(id);

            myResponse.data = "{ \"Header\": " + Header + "}";
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
    public void SaveRequirement(string action, string parent, string id, string name, string reference, string order, string keywords, string clients, string version, string detail)
    {
        try
        {
            Objects.requirement oEle = new Objects.requirement();

            if (Guid.Parse(id) == Guid.Empty)
            {
                oEle.requirement_id = Guid.NewGuid();
            }
            else
            {
                oEle.requirement_id = Guid.Parse(id);
                oEle.Get();
            }

            oEle.name = name;
            oEle.reference_no = reference;
            oEle.sort_order = order;
            oEle.keywords = keywords;
            oEle.clients = clients;
            oEle.version = version;
            oEle.detail = detail;

            oEle.Save();

            if (action == "ADD")
            {
                Objects.requirement_list oList = new Objects.requirement_list();
                oList.parent_list_id = Guid.Parse(parent);
                oList.requirement_id = oEle.requirement_id;
                oList.list_type = "a";
                oList.Save();
            }

            myResponse.data = oEle.requirement_id.ToString();
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
    public void MoveRequirement(string from, string to)
    {
        try
        {
            if (!oData.ValidateDrop(from, to))
            {
                throw new Exception("Cannot Move Requirement one of its children"); 
            }
            oData.DeleteLink(from);
            oData.LinkTo(from, to);

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
    public void DeleteRequirement(string id)
    {
        try
        {
            oData.DeleteLink(id);
            oData.DeleteRequirement(id);
            oData.DeleteAttachments(id);

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
            string mType = fileName.Substring(fileName.LastIndexOf(@".") + 1);

            string id = postedContext.Request.Form[1];
            Objects.requirement_attachment oAttach = new Objects.requirement_attachment();

            oAttach.attachment_id = Guid.NewGuid();
            oAttach.requirement_id = Guid.Parse(id);

            /*
             * Mime Types
             * application/msword					*.doc
             * application/msaccess					*.mdb
             * application/pdf						*.pdf
             * application/vnd.ms-excel				*.xls
             * application/vnd.ms-powerpoint		*.ppt
             * application/vnd.ms-project			*.mpp
             * application/vnd.visio				*.vsd
             * application/zip						*.zip
             * text/plain							*.txt
             * text/richtext						*.rtx
             * text/xml								*.xml
            */
            
            switch (mType)
            {
                case "doc":
                case "docx":
                    oAttach.mime_type = "application/msword";
                    break;

                case "xlsx":
                case "xls":
                    oAttach.mime_type = "application/vnd.ms-excel";
                    break;

                case "ppt":
                    oAttach.mime_type = "application/vnd.ms-powerpoint";
                    break;

                case "zip":
                    oAttach.mime_type = "application/zip";
                    break;

                case "txt":
                    oAttach.mime_type = "text/plain";
                    break;

                case "xml":
                    oAttach.mime_type = "text/xml";
                    break;

                case "rtx":
                    oAttach.mime_type = "text/richtext";
                    break;

                case "pdf":
                    oAttach.mime_type = "application/pdf";
                    break;

                default:
                    oAttach.mime_type = "text/plain";
                    break;
            }

            oAttach.filename = fileName;
            oAttach.size = attachment.Length;
            oAttach.attach_date = DateTime.Now;
            oAttach.attachment = attachment;
            oAttach.Save();

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

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void GetAttachments(string id)
    {
        try
        {
            myResponse.data = oData.GetAttachments(id);
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
    public void DeleteAttachment(string key)
    {
        try
        {
            oData.DeleteAttachment(key);

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


