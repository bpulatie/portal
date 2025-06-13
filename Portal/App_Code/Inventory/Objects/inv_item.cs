using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class inv_item : SPA.spaBase
    {
        const string database_connection = "spa_inventory";
        const string database_table = "inv_item";

        public inv_item()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid item_id { get; set; }
        [DataMember]
        public Guid category_id { get; set; }
        [DataMember]
        public String item_name { get; set; }
        [DataMember]
        public String external_id { get; set; }
        [DataMember]
        public String item_type { get; set; }
        [DataMember]
        public String active_flag { get; set; }
        [DataMember]
        public String buy_flag { get; set; }
        [DataMember]
        public String sell_flag { get; set; }
        [DataMember]
        public String count_flag { get; set; }
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
            if (this.item_name == null)
            {
                throw (new Exception("Please provide an Item Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "item_id", this.item_id, "item_name", this.item_name))
            {
                throw (new Exception("An Item with this name already exists - please choose another name"));
            }

        }
    }

}
