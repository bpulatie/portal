using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_feature : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_feature";

        public sys_feature()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid feature_id { get; set; }
        [DataMember]
        public Guid application_id { get; set; }
        [DataMember]
        public String feature_name { get; set; }
        [DataMember]
        public String moniker { get; set; }
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
            if (this.feature_name == null)
            {
                throw (new Exception("Please provide a Feature Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "feature_id", this.feature_id, "feature_name", this.feature_name))
            {
                throw (new Exception("Feature already exists - please choose another name"));
            }

         }
    }
}
