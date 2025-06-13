using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_client_note : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_client_note";

        public sys_client_note()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid client_note_id { get; set; }
        [DataMember]
        public Guid client_id { get; set; }
        [DataMember]
        public String summary { get; set; }
        [DataMember]
        public String description { get; set; }
        [DataMember]
        public DateTime? follow_up_date { get; set; }
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
            DataLayer.sys_client oClient = new DataLayer.sys_client();
            if (this.summary == null)
            {
                throw (new Exception("Please provide a Note Summary"));
            }

            if (this.description == null)
            {
                throw (new Exception("Please provide Note details"));
            }


        }

        public override void After_Save()
        {
        }
    }
}
