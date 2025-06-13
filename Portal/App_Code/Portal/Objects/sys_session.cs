using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_session : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_session";

        public sys_session()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid session_id { get; set; }
        [DataMember]
        public Guid user_id { get; set; }
        [DataMember]
        public DateTime start_time { get; set; }
        [DataMember]
        public DateTime last_activity_time { get; set; }
        [DataMember]
        public DateTime? end_time { get; set; }
        [DataMember]
        public String ip_address { get; set; }
        [DataMember]
        public String username { get; set; }
        [DataMember]
        public String context { get; set; }
        [DataMember]
        public String session_status { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {


        }
    }
 
    public enum session_status
    {
        sign_on = 0,
        sign_off = 1,
        expired = 2
    }

}
