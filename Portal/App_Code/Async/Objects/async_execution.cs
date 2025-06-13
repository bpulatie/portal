using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class async_execution : SPA.spaBase
    {
        const string database_connection = "spa_async";
        const string database_table = "async_execution";

        public async_execution()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid execution_id { get; set; }
        [DataMember]
        public int process_id { get; set; }
        [DataMember]
        public Guid job_id { get; set; }
        [DataMember]
        public Guid task_id { get; set; }
        [DataMember]
        public Guid queue_id { get; set; }
        [DataMember]
        public String async_name { get; set; }
        [DataMember]
        public int task_no { get; set; }
        [DataMember]
        public DateTime queued_time { get; set; }
        [DataMember]
        public DateTime? start_time { get; set; }
        [DataMember]
        public DateTime? end_time { get; set; }
        [DataMember]
        public String status_code { get; set; }
        [DataMember]
        public String context { get; set; }
        [DataMember]
        public Guid user_id { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {


        }
    }
 
}
