using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class async_queue : SPA.spaBase
    {
        const string database_connection = "spa_async";
        const string database_table = "async_queue";

        public async_queue()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid queue_id { get; set; }
        [DataMember]
        public String queue_name { get; set; }
        [DataMember]
        public int thread_count { get; set; }
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

        }
    }

}
