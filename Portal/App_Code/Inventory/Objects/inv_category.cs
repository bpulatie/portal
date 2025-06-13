using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class inv_category : SPA.spaBase
    {
        const string database_connection = "spa_inventory";
        const string database_table = "inv_category";

        public inv_category()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid category_id { get; set; }
        [DataMember]
        public Guid category_level_id { get; set; }
        [DataMember]
        public Guid parent_category_id { get; set; }
        [DataMember]
        public String name { get; set; }
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
            if (this.name == null)
            {
                throw (new Exception("Please provide a Category Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "category_id", this.category_id, "name", this.name))
            {
                throw (new Exception("A Category with this name already exists - please choose another name"));
            }
        }
    }

}
