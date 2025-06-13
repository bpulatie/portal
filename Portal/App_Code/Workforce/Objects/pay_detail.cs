using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_detail : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_detail";

        public pay_detail()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid pay_period_id { get; set; }
        [DataMember]
        public Guid site_id { get; set; }
        [DataMember]
        public Guid employee_id { get; set; }
        [DataMember]
        public Guid job_id { get; set; }
        [DataMember]
        public String company { get; set; }
        [DataMember]
        public String project { get; set; }
        [DataMember]
        public String earning_code { get; set; }
        [DataMember]
        public Decimal earning_hours { get; set; }
        [DataMember]
        public Decimal rate { get; set; }
        [DataMember]
        public Decimal earning_amount { get; set; }
        [DataMember]
        public DateTime charge_date { get; set; }
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
