using System.Text;
using System.Web;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;
using System;

namespace SPA
{
    public static class spaGlobals
    {
        static spaGlobals()
        {
        }

        public static string ProcessFilters(HttpRequest request)
        {
            request.InputStream.Position = 0;
            string rawJson = null;
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                rawJson = reader.ReadToEnd();
            }
            
            return ProcessJsonFilters(rawJson);
        }

        public static string ProcessJsonFilters(string rawJson)
        {
            //dynamic oRequest = (dynamic)JsonConvert.DeserializeObject(rawJson);
            dynamic oRequest = JObject.Parse(rawJson);

            string sFilter = string.Empty;

            for (int x = 0; x < oRequest["filter"].Count; x++)
            {

                string column = oRequest["filter"][x]["column"];
                string comparison = oRequest["filter"][x]["comparison"];
                string value = oRequest["filter"][x]["value"];

                switch (comparison)
                {
                    case "greaterthan":
                        sFilter += " AND " + column + " > '" + value + "'";
                        break;

                    case "lessthan":
                        sFilter += " AND " + column + " < '" + value + "'";
                        break;

                    case "contains":
                        sFilter += " AND " + column + " like '%" + value + "%'";
                        break;

                    case "startswith":
                        sFilter += " AND " + column + " like '%" + value + "'";
                        break;

                    case "endswith":
                        sFilter += " AND " + column + " like '" + value + "%'";
                        break;

                    case "equals":
                        sFilter += " AND " + column + " = '" + value + "'";
                        break;

                    default:
                        sFilter += " AND " + column + " " + comparison + " '" + value + "'";
                        break;

                }
            }

            return sFilter;
        }

        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        
        public static string GetHashedPassword(string password)
        {
            byte[] hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            return System.Text.Encoding.Default.GetString(hash); 
        }

        public static void SendMail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("postmaster@virtuallyracing.com", "Do Not Reply");
            mail.To.Add(new MailAddress(email));

            //set the content 
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpClient smtp = new SmtpClient("mail.virtuallyracing.com");

            NetworkCredential Credentials = new NetworkCredential("postmaster@virtuallyracing.com", "Kirstie@92");
            smtp.Credentials = Credentials;
            smtp.Send(mail);
        }
    }
}
