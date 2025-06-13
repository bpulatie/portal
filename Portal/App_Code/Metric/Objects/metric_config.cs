using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class metric_config : SPA.spaBase
    {
        const string database_connection = "spa_metric";
        const string database_table = "metric_config";

        public metric_config()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid metric_id { get; set; }
        [DataMember]
        public String metric_name { get; set; }
        [DataMember]
        public String metric_detail { get; set; }
        [DataMember]
        public String dimension_1_label { get; set; }
        [DataMember]
        public String dimension_2_label { get; set; }
        [DataMember]
        public String dimension_3_label { get; set; }
        [DataMember]
        public String value_1_label { get; set; }
        [DataMember]
        public String value_2_label { get; set; }
        [DataMember]
        public String value_3_label { get; set; }
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
            if (this.metric_name == null)
            {
                throw (new Exception("Please provide a Metric Name"));
            }

            DataLayer.sys_utils oData = new DataLayer.sys_utils();
            if (oData.IsNameUnique(database_connection, database_table, "metric_id", this.metric_id, "metric_name", this.metric_name))
            {
                throw (new Exception("A Metric with this name already exists - please choose another name"));
            }
        }
    }

}
