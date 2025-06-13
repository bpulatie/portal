using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_employee : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_employee";

        public pay_employee()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid employee_id { get; set; }
        [DataMember]
        public String employee_no { get; set; }
        [DataMember]
        public String site_code { get; set; }
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
        public String status_code { get; set; }
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
            DataLayer.sys_session oSession = new DataLayer.sys_session();
            if (this.client_id == Guid.Empty)
            {
                this.client_id = Guid.Parse(oSession.client_id);
            }

            if (this.employee_no == null)
            {
                throw (new Exception("Please provide an Employee No"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "employee_id", this.employee_id, "employee_no", this.employee_no))
            {
                throw (new Exception("An Employee with this Employee No already exists - please choose another Employee No"));
            }

        }

    }
}
