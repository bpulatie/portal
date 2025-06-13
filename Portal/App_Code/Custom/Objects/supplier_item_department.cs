using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class supplier_item_department : SPA.spaBase
    {
        const string database_connection = "spa_custom";
        const string database_table = "supplier_item_department";

        public supplier_item_department()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid supplier_item_department_id { get; set; }
        [DataMember]
        public Guid supplier_id { get; set; }
        [DataMember]
        public String department_name { get; set; }
        [DataMember]
        public String department_code { get; set; }
        [DataMember]
        public String external_id { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {


        }
    }

}
