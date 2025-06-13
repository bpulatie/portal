using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_client : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_client";

        public sys_client()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid client_id { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String status_code { get; set; }
        [DataMember]
        public String contact_name { get; set; }
        [DataMember]
        public String contact_email { get; set; }
        [DataMember]
        public String contact_phone { get; set; }
        [DataMember]
        public int category_levels { get; set; }
        [DataMember]
        public string client_root { get; set; }
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

        public override void Before_Save()
        {
            DataLayer.sys_client oClient = new DataLayer.sys_client();
            if (oClient.Exists(this.client_id, this.name))
            {
                throw (new Exception("A Client with this name already exists - please choose another name"));
            }

            if (this.contact_name == null)
            {
                throw (new Exception("Please provide a Contact Name"));
            }

            if (this.contact_email == null)
            {
                throw (new Exception("Please provide a Contact Email"));
            }

            if (this.contact_phone == null)
            {
                throw (new Exception("Please provide a Contact Phone No"));
            }

            if (this.status_code == null)
            {
                throw (new Exception("Please select a Status"));
            }

        }

        public override void After_Save()
        {
            if (this.category_levels != null)
            {
                Services.inv_category_level oLevel = new Services.inv_category_level();
                oLevel.SetLevels(this.client_id, this.category_levels);
            }
        }
    }
}
