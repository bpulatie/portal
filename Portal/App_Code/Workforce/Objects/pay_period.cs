using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Objects
{
    [DataContract]
    public class pay_period : SPA.spaBase
    {
        const string database_connection = "spa_workforce";
        const string database_table = "pay_period";

        public pay_period()
            : base(database_connection, database_table)
        {
        }

        [DataMember]
        public Guid pay_period_id { get; set; }
        [DataMember]
        public String week_no { get; set; }
        [DataMember]
        public DateTime start_date { get; set; }
        [DataMember]
        public DateTime end_date { get; set; }
        [DataMember]
        public String status_code { get; set; }
        [DataMember]
        public String cycle_code { get; set; }
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
            DateTime tempDate = this.start_date;
            this.start_date = tempDate.Date;

            tempDate = this.end_date;
            this.end_date = tempDate.Date;

            if (this.start_date >= this.end_date)
            {
                throw (new Exception("Error: Start Date cannot be before End Date"));
            }

            if (this.week_no == null || this.week_no == string.Empty)
            {
                throw (new Exception("Error: Please enter a Week No"));
            }

        }
    }
}
