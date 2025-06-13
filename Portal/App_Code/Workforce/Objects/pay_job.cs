using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_job : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_job";

        public pay_job()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid job_id { get; set; }
        [DataMember]
        public String company { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String job_code { get; set; }
        [DataMember]
        public String external_code { get; set; }
        [DataMember]
        public int access_level { get; set; }
        [DataMember]
        public int security_level { get; set; }
        [DataMember]
        public String mag_only { get; set; }
        [DataMember]
        public String mask_pay { get; set; }
        [DataMember]
        public String active_flag { get; set; }
        [DataMember]
        public String include_in_mgr_group { get; set; }
        [DataMember]
        public String tip_status_code { get; set; }
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
                throw (new Exception("Please enter a Job Name"));
            }

            DataLayer.pay_job oJob = new DataLayer.pay_job();
            if (oJob.NameExists(this.client_id, this.job_id, this.name))
            {
                throw (new Exception("A Job with this name already exists - please choose another name"));
            }

            if (this.job_code == null)
            {
                throw (new Exception("Please enter a Job Code"));
            }

            if (this.company == null)
            {
                throw (new Exception("Please enter a Company"));
            }

            if (this.external_code == null)
            {
                throw (new Exception("Please enter an External Code"));
            }

            if (this.tip_status_code == null)
            {
                throw (new Exception("Please select a Tip Status Code"));
            }

        }
    }

}
