using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class requirement_attachment : SPA.spaBase
    {
        const string database_connection = "RDM_Repository";
        const string database_table = "requirement_attachment";

        public requirement_attachment()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid attachment_id { get; set; }
        [DataMember]
        public Guid requirement_id { get; set; }
        [DataMember]
        public String filename { get; set; }
        [DataMember]
        public String mime_type { get; set; }
        [DataMember]
        public DateTime attach_date { get; set; }
        [DataMember]
        public byte[] attachment { get; set; }
        [DataMember]
        public int size { get; set; }
    }
}
