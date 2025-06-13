using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class async_job_task_list : SPA.spaBase
    {
        const string database_connection = "spa_async";
        const string database_table = "async_job_task_list";

        public async_job_task_list()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid job_task_id { get; set; }
        [DataMember]
        public Guid job_id { get; set; }
        [DataMember]
        public Guid task_id { get; set; }
        [DataMember]
        public int sort_order { get; set; }
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
