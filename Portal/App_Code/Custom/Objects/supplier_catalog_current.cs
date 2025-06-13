using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class supplier_catalog_current : SPA.spaBase
    {
        const string database_connection = "spa_custom";
        const string database_table = "supplier_catalog_current";

        public supplier_catalog_current()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid supplier_catalog_id { get; set; }
        [DataMember]
        public Guid supplier_id { get; set; }
        [DataMember]
        public String supplier_id_file { get; set; }
        [DataMember]
        public String supplier_item_code { get; set; }
        [DataMember]
        public String product_description { get; set; }
        [DataMember]
        public String uom_type { get; set; }
        [DataMember]
        public String uom_count { get; set; }
        [DataMember]
        public String cost_level { get; set; }
        [DataMember]
        public String cost { get; set; }
        [DataMember]
        public String status1 { get; set; }
        [DataMember]
        public String upc_pack { get; set; }
        [DataMember]
        public String upc_retail { get; set; }
        [DataMember]
        public String supplier_category { get; set; }
        [DataMember]
        public String status2 { get; set; }
        [DataMember]
        public String action { get; set; }
        [DataMember]
        public DateTime pending_delete { get; set; }
        [DataMember]
        public String exported_flag { get; set; }
        [DataMember]
        public DateTime last_modified { get; set; }
        [DataMember]
        public Guid supplier_item_department_id { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {


        }
    }

}
