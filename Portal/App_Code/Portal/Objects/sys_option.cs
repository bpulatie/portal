using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_option : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_option";

        public sys_option()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid option_id { get; set; }
        [DataMember]
        public String option_name { get; set; }
        [DataMember]
        public String value { get; set; }
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
            if (this.option_name == null)
            {
                throw (new Exception("Please provide an Option Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "option_id", this.option_id, "option_name", this.option_name))
            {
                throw (new Exception("Option already exists - please choose another name"));
            }


        }
    }
}
