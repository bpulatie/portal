using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_employee_history : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_employee_history";

        public pay_employee_history()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid employee_history_id { get; set; }
        [DataMember]
        public Guid employee_id { get; set; }
        [DataMember]
        public String employee_no { get; set; }
        [DataMember]
        public DateTime start_date { get; set; }
        [DataMember]
        public DateTime termination_date { get; set; }
        [DataMember]
        public String email { get; set; }
        [DataMember]
        public String cellphone { get; set; }
        [DataMember]
        public String contact { get; set; }
        [DataMember]
        public String contact_relation { get; set; }
        [DataMember]
        public String contact_phone { get; set; }
        [DataMember]
        public String rate_type { get; set; }
        [DataMember]
        public Decimal pay_rate { get; set; }
        [DataMember]
        public String national_code { get; set; }
        [DataMember]
        public String site_code { get; set; }
        [DataMember]
        public String org_code { get; set; }
        [DataMember]
        public String termination_reason { get; set; }
        [DataMember]
        public String payroll_system_no { get; set; }
        [DataMember]
        public String user_level { get; set; }
        [DataMember]
        public String job_code { get; set; }
        [DataMember]
        public String job_name { get; set; }
        [DataMember]
        public String primary_job { get; set; }
        [DataMember]
        public String pos_code { get; set; }
        [DataMember]
        public Byte mag_only { get; set; }
        [DataMember]
        public int? security_level { get; set; }
        [DataMember]
        public int? access_level { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public Byte? meal_break_waiver { get; set; }
        [DataMember]
        public Guid? creation_user_id { get; set; }
        [DataMember]
        public DateTime? creation_date { get; set; }
        [DataMember]
        public Guid? modified_user_id { get; set; }
        [DataMember]
        public DateTime? modified_date { get; set; }
    }
}
