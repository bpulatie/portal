using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class requirement_list : SPA.spaBase
    {
        const string database_connection = "RDM_Repository";
        const string database_table = "requirement_list";

        public requirement_list()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid parent_list_id { get; set; }
        [DataMember]
        public Guid requirement_id { get; set; }
        [DataMember]
        public String list_type { get; set; }
    }
}
