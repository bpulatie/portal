using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_menu : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_menu";

        public sys_menu()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid menu_id { get; set; }
        [DataMember]
        public String menu_name { get; set; }
        [DataMember]
        public int sort_order { get; set; }
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
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {
            if (this.menu_name == null)
            {
                throw (new Exception("Please provide a Menu Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "menu_id", this.menu_id, "menu_name", this.menu_name))
            {
                throw (new Exception("A Menu with this name already exists - please choose another name"));
            }

        }
    }

}
