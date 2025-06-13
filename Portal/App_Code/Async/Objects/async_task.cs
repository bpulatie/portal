using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class async_task : SPA.spaBase
    {
        const string database_connection = "spa_async";
        const string database_table = "async_task";

        public async_task()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid task_id { get; set; }
        [DataMember]
        public String task_name { get; set; }
        [DataMember]
        public String moniker { get; set; }
        [DataMember]
        public String task_context { get; set; }
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

         }
    }

}
