using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Security;

namespace Objects
{
    [DataContract]
    public class sys_user : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_user";

        public sys_user()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid user_id { get; set; }
        [DataMember]
        public String login_name { get; set; }
        [DataMember]
        public String password { get; set; }
        [DataMember]
        public String user_type { get; set; }
        [DataMember]
        public String last_name { get; set; }
        [DataMember]
        public String first_name { get; set; }
        [DataMember]
        public String email { get; set; }
        [DataMember]
        public String gender { get; set; }
        [DataMember]
        public String menu_location { get; set; }
        [DataMember]
        public String style_preference { get; set; }
        [DataMember]
        public DateTime? date_of_birth { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public Guid? creation_user_id { get; set; }
        [DataMember]
        public DateTime? creation_date { get; set; }
        [DataMember]
        public Guid? modified_user_id { get; set; }
        [DataMember]
        public DateTime? modified_date { get; set; }
        [DataMember]
        public Guid client_id { get; set; }
        [DataMember]
        public Guid image_id { get; set; }
        [DataMember]
        public String temp_password { get; set; }

        public override void Before_Save()
        {
            if (this.first_name == null)
            {
                throw (new Exception("Please provide a First Name"));
            }

            if (this.last_name == null)
            {
                throw (new Exception("Please provide a Last Name"));
            }

            if (this.login_name == null || this.login_name.Length < 5)
            {
                throw (new Exception("Please provide a Login Name of at least 5 characters"));
            }

            DataLayer.sys_user oUser = new DataLayer.sys_user();
            if (oUser.Exists(this.user_id, this.login_name))
            {
                throw (new Exception("Login name already in use - please choose another name"));
            }

            if (this.email == null)
            {
                throw (new Exception("Please provide an Email Address"));
            }

            if (this.user_type == null)
            {
                throw (new Exception("Please select a User Type"));
            }

            /*
            if (this.gender == null)
            {
                throw (new Exception("Please select a Gender"));
            }
            */

            if (this.password == null)
            {
                String random = Membership.GeneratePassword(6, 3);
                this.password = SPA.spaGlobals.GetHashedPassword(random);
                this.temp_password = "y";

                SendPasswordEmail(this.first_name + " " + this.last_name, this.login_name, this.email, random);
            }

        }

        public void SendPasswordEmail(string name, string username, string email, string password)
        {
            string body = "<table>" +
                                "<tr><td>Hello " + name + ",</td></tr>" +
                                "<tr><td>&nbsp;</td></tr>" +
                                "<tr><td>You have been granted access to our system.</td></tr>" +
                                "<tr><td>Your username is " + username + "</td></tr>" +
                                "<tr><td>Your initial temporary password is " + password + "</td></tr>" +
                        "</table";

            SPA.spaGlobals.SendMail(email, "Initial Password", body);
        }
    }
}
