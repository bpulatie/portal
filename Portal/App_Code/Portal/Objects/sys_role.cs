using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_role : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_role";

        public sys_role()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid role_id { get; set; }
        [DataMember]
        public String role_name { get; set; }
        [DataMember]
        public String external_name { get; set; }
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
            if (this.role_name == null)
            {
                throw (new Exception("Please enter a Role Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "role_id", this.role_id, "role_name", this.role_name))
            {
                throw (new Exception("Role already exists - please choose another name"));
            } 
            
 
        }
    }
}
