using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_event : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_event";

        public sys_event()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid event_id { get; set; }
        [DataMember]
        public Guid event_category_id { get; set; }
        [DataMember]
        public Guid event_type_id { get; set; }
        [DataMember]
        public DateTime event_date { get; set; }
        [DataMember]
        public String event_category { get; set; }
        [DataMember]
        public String event_type { get; set; }
        [DataMember]
        public String event_status { get; set; }
        [DataMember]
        public String event_priority { get; set; }
        [DataMember]
        public String event_summary { get; set; }
        [DataMember]
        public String event_details { get; set; }
        [DataMember]
        public Guid? creation_user_id { get; set; }
        [DataMember]
        public DateTime? creation_date { get; set; }
        [DataMember]
        public Guid? modified_user_id { get; set; }
        [DataMember]
        public DateTime? modified_date { get; set; }
        [DataMember]
        public DateTime? email_date { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {
 
        }
    }

}
