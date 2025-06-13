using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class async_job : SPA.spaBase
    {
        const string database_connection = "spa_async";
        const string database_table = "async_job";

        public async_job()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid job_id { get; set; }
        [DataMember]
        public String job_name { get; set; }
        [DataMember]
        public String active_flag { get; set; }
        [DataMember]
        public String schedule_code { get; set; }
        [DataMember]
        public int schedule_frequency { get; set; }
        [DataMember]
        public DateTime schedule_time { get; set; }
        [DataMember]
        public String job_context { get; set; }
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

            if (this.job_name == null)
            {
                throw (new Exception("Please provide a Job Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "job_id", this.job_id, "job_name", this.job_name))
            {
                throw (new Exception("Async Job already exists - please choose another name"));
            }

            if (this.schedule_frequency == null || this.schedule_frequency < 2)
            {
                this.schedule_frequency = 2;
            }

            if (this.schedule_time == DateTime.MinValue)
            {
                this.schedule_time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm"));
            }

            if (this.active_flag == null)
            {
                this.active_flag = "n";
            }

        }

        public override void Before_Delete()
        {
            DataLayer.sys_utils oData = new DataLayer.sys_utils();

            oData.DeleteDependancies(database_connection, "async_job_task_list", "job_id", this.job_id);
        }


    }

}
