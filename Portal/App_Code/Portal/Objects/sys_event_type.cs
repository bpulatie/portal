using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_event_type : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_event_type";

        public sys_event_type()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid event_type_id { get; set; }
        [DataMember]
        public String event_name { get; set; }
        [DataMember]
        public Guid event_category_id { get; set; }
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
            if (this.event_name == null)
            {
                throw (new Exception("Please provide a Event Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "event_type_id", this.event_type_id, "event_name", this.event_name))
            {
                throw (new Exception("Event Type already exists - please choose another name"));
            }

         }
    }

}
