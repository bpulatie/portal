using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Objects
{
    [DataContract]
    public class sys_application : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_application";

        public sys_application()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid application_id { get; set; }
        [DataMember]
        public String application_name { get; set; }
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
            if (this.application_name == null)
            {
                throw (new Exception("Please provide a Application Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "application_id", this.application_id, "application_name", this.application_name))
            {
                throw (new Exception("Application already exists - please choose another name"));
            }

        }

        public override void Before_Delete()
        {
            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.HasDependancies(database_connection, "sys_feature", "application_id", this.application_id))
            {
                throw (new Exception("Application has associated Features - please delete these first"));
            }

        }
    }
}
