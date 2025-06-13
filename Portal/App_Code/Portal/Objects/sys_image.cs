using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_image : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_image";

        public sys_image()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid image_id { get; set; }
        [DataMember]
        public String image_name { get; set; }
        [DataMember]
        public byte[] the_image { get; set; }
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
