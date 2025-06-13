using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_group : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_group";

        public pay_group()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid pay_group_id { get; set; }
        [DataMember]
        public String pay_group_code { get; set; }
        [DataMember]
        public String filter_description { get; set; }
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

            if (this.pay_group_code == null)
            {
                throw (new Exception("Please provide a Pay Group Code"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUniqueByClient(database_connection, this.client_id, database_table, "pay_group_id", this.pay_group_id, "pay_group_code", this.pay_group_code))
            {
                throw (new Exception("Pay Group already exists - please choose another Pay Group Code"));
            }

        }
    }
}
