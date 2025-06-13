using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_role_feature_list : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_role_feature_list";

        public sys_role_feature_list()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid role_feature_id { get; set; }
        [DataMember]
        public Guid role_id { get; set; }
        [DataMember]
        public Guid feature_id { get; set; }
        [DataMember]
        public Guid menu_id { get; set; }
        [DataMember]
        public String menu_item { get; set; }
        [DataMember]
        public int menu_mode { get; set; }
        [DataMember]
        public int sort_order { get; set; }
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
