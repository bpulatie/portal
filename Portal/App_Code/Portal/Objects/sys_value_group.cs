using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_value_group : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_value_group";

        public sys_value_group()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid group_id { get; set; }
        [DataMember]
        public String group_name { get; set; }
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

        public override void Before_Save()
        {
            if (this.group_name == null)
            {
                throw (new Exception("Error: Please enter a Value Group Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "group_id", this.group_id, "group_name", this.group_name))
            {
                throw (new Exception("Value Group already exists - please choose another name"));
            }

        }
    }
}
