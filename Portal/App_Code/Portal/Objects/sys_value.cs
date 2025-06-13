using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_value : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_value";

        public sys_value()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid group_id { get; set; }
        [DataMember]
        public Guid value_id { get; set; }
        [DataMember]
        public String value_code { get; set; }
        [DataMember]
        public String value_text { get; set; }
        [DataMember]
        public int sort_order { get; set; }
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
    }
}
