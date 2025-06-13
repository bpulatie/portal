using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class supplier : SPA.spaBase
    {
        const string database_connection = "spa_custom";
        const string database_table = "supplier";

        public supplier()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public int supplier_id { get; set; }
        [DataMember]
        public String external_id { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public int safety_stock { get; set; }
        [DataMember]
        public String edi_supplier_id { get; set; }
        [DataMember]
        public int external_id2 { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {


        }
    }

}
