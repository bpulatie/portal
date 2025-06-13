using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_reason_category : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_reason_category";

        public sys_reason_category()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid reason_category_id { get; set; }
        [DataMember]
        public String reason_category { get; set; }
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
            if (this.reason_category == null)
            {
                throw (new Exception("Error: Please enter a Reason Code"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "reason_category_id", this.reason_category_id, "reason_category", this.reason_category))
            {
                throw (new Exception("Reason Category already exists - please choose another name"));
            } 
            
 
        }    
    }
}
