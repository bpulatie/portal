using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_site_group_list : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_site_group_list";

        public sys_site_group_list()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid site_group_id { get; set; }
        [DataMember]
        public Guid site_id { get; set; }
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


