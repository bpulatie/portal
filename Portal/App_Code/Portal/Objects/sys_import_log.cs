using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_import_log : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_import_log";

        public sys_import_log()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid import_id { get; set; }
        [DataMember]
        public Guid site_id { get; set; }
        [DataMember]
        public String path { get; set; }
        [DataMember]
        public String filename { get; set; }
        [DataMember]
        public String status_code { get; set; }
        [DataMember]
        public String comment { get; set; }
        [DataMember]
        public Guid? creation_user_id { get; set; }
        [DataMember]
        public DateTime? creation_date { get; set; }
        [DataMember]
        public Guid? modified_user_id { get; set; }
        [DataMember]
        public DateTime? modified_date { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

    }
}


