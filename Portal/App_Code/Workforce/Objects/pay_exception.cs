using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_exception : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_exception";

        public pay_exception()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid pay_period_id { get; set; }
        [DataMember]
        public Guid pay_exception_id { get; set; }
        [DataMember]
        public Guid employee_id { get; set; }
        [DataMember]
        public Guid reason_id { get; set; }
        [DataMember]
        public String exception_code { get; set; }
        [DataMember]
        public String status_code { get; set; }
        [DataMember]
        public String comment { get; set; }
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

        }
    }
}
