using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_event_category : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_event_category";

        public sys_event_category()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid event_category_id { get; set; }
        [DataMember]
        public String event_category_name { get; set; }
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
            if (this.event_category_name == null)
            {
                throw (new Exception("Please provide a Event Category Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "event_category_id", this.event_category_id, "event_category_name", this.event_category_name))
            {
                throw (new Exception("Event Category already exists - please choose another name"));
            }

          }
    }

}
