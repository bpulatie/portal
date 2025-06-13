using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class sys_site : SPA.spaBase
    {
        const string database_connection = "spa_portal";
        const string database_table = "sys_site";

        public sys_site()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid site_id { get; set; }
        [DataMember]
        public String site_code { get; set; }
        [DataMember]
        public String name { get; set; }
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
        public String source_site { get; set; }
        [DataMember]
        public String target_site { get; set; }
        [DataMember]
        public DateTime? go_live { get; set; }
        [DataMember]
        public String site_status { get; set; }
        [DataMember]
        public String site_type { get; set; }
        [DataMember]
        public String org_id { get; set; }
        [DataMember]
        public String payroll_status { get; set; }
        [DataMember]
        public String state { get; set; }
        [DataMember]
        public Guid site_guid { get; set; }
        [DataMember]
        public Guid client_id { get; set; }

        public override void Before_Save()
        {
            if (this.site_code == null)
            {
                throw (new Exception("Please provide a Site Code"));
            }

            if (this.name == null)
            {
                throw (new Exception("Please provide a Site Name"));
            }

            DataLayer.sys_site oSite = new DataLayer.sys_site();
            if (oSite.NameExists(this.client_id, this.site_id, this.name))
            {
                throw (new Exception("A Site with this name already exists - please choose another name"));
            }

            if (oSite.CodeExists(this.client_id, this.site_id, this.site_code))
            {
                throw (new Exception("A Site with this Site Code already exists - please choose another Site Code"));
            }

            if (this.state != null)
            {
                if (this.state.Length != 2)
                {
                    throw (new Exception("Error: Please enter a two character State"));
                }
            }
        }

        public override void Before_Delete()
        {
            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.HasDependancies(database_connection, "pay_detail", "site_id", this.site_id))
            {
                throw (new Exception("Cannot delete Site until Pay Data has been purged"));
            }

            oData.DeleteDependancies(database_connection, "sys_user_site_list", "site_id", this.site_id);
            oData.DeleteDependancies(database_connection, "sys_site_group_list", "site_id", this.site_id);
            oData.DeleteDependancies(database_connection, "pay_exception", "site_id", this.site_id);
        }

    }
}


