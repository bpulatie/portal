using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_reason : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_reason";

        public sys_reason()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid reason_id { get; set; }
        [DataMember]
        public Guid reason_category_id { get; set; }
        [DataMember]
        public String reason_code { get; set; }
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
            if (this.reason_category_id == Guid.Empty)
            {
                throw (new Exception("Error: Please select a Reason Category"));
            }

            if (this.reason_code == null)
            {
                throw (new Exception("Error: Please enter a Reason Code"));
            }


        }    
    }
}
