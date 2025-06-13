using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_menu_item : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_menu_item";

        public sys_menu_item()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid menu_id { get; set; }
        [DataMember]
        public Guid feature_id { get; set; }
        [DataMember]
        public String menu_item_name { get; set; }
        [DataMember]
        public int sort_order { get; set; }
        [DataMember]
        public int menu_mode { get; set; }
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
            if (this.menu_item_name == null)
            {
                throw (new Exception("Please provide a Menu Item Name"));
            }

        }
    }

}
