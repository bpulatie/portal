using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_event_subscription : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_event_subscription";

        public sys_event_subscription()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid event_subscription_id { get; set; }
        [DataMember]
        public Guid event_category_id { get; set; }
        [DataMember]
        public Guid event_type_id { get; set; }
        [DataMember]
        public Guid user_id { get; set; }
        [DataMember]
        public String notification_type { get; set; }
        [DataMember]
        public String email { get; set; }
        [DataMember]
        public DateTime? email_time { get; set; }
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
        }
    }

}
