using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Objects
{
    [DataContract]
    public class inv_category_level : SPA.spaBase
    {
        const string database_connection = "spa_inventory";
        const string database_table = "inv_category_level";

        public inv_category_level()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid category_level_id { get; set; }
        [DataMember]
        public String level_name { get; set; }
        [DataMember]
        public int depth { get; set; }
        [DataMember]
        public String item_level { get; set; }
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
            if (this.level_name == null)
            {
                throw (new Exception("Please provide a Category Level Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "category_level_id", this.category_level_id, "level_name", this.level_name))
            {
                throw (new Exception("A Category Level with this name already exists - please choose another name"));
            }

        }

    }

}
