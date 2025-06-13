using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class async_task_parameter : SPA.spaBase
    {
        const string database_connection = "spa_async";
        const string database_table = "async_task_parameter";

        public async_task_parameter()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid parameter_id { get; set; }
        [DataMember]
        public Guid task_id { get; set; }
        [DataMember]
        public String parameter_name { get; set; }
        [DataMember]
        public String data_type { get; set; }
        [DataMember]
        public String required { get; set; }
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
