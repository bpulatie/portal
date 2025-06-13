using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_site_group : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_site_group";

        public sys_site_group()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid site_group_id { get; set; }
        [DataMember]
        public String site_group_code { get; set; }
        [DataMember]
        public String name { get; set; }
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

        public override void Before_Save()
        {
            if (this.site_group_code == null)
            {
                throw (new Exception("Please provide a Group Code"));
            }

            if (this.name == null)
            {
                throw (new Exception("Please provide a Group Name"));
            }

            DataLayer.sys_site_group oGroup = new DataLayer.sys_site_group();
            if (oGroup.Exists(this.client_id, this.site_group_id, this.name))
            {
                throw (new Exception("A Site Group with this name already exists - please choose another name"));
            }

        }
    }
}


