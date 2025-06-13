using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_option_value : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_option_value";

        public sys_option_value()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid client_id { get; set; }
        [DataMember]
        public Guid option_id { get; set; }
        [DataMember]
        public String option_value { get; set; }
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
