using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_access : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_access";

        public sys_access()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid access_id { get; set; }
        [DataMember]
        public String access_name { get; set; }
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
            if (this.access_name == null)
            {
                throw (new Exception("Please provide a Access Feature Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "access_id", this.access_id, "access_name", this.access_name))
            {
                throw (new Exception("Access Feature already exists - please choose another name"));
            }

        }

        public override void Before_Delete()
        {
            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.HasDependancies(database_connection, "sys_role_access_list", "access_id", this.access_id))
            {
                throw (new Exception("Roles have associated Access Features - please delete these first"));
            }

        }
    }
}
