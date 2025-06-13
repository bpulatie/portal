using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class requirement : SPA.spaBase
    {
        const string database_connection = "RDM_Repository";
        const string database_table = "requirement";

        public requirement()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid requirement_id { get; set; }
        [DataMember]
        public String reference_no { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String sort_order { get; set; }
        [DataMember]
        public String keywords { get; set; }
        [DataMember]
        public String clients { get; set; }
        [DataMember]
        public String version { get; set; }
        [DataMember]
        public String detail { get; set; }
    }
}
