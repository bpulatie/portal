using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AsyncLibrary;

namespace MetricImport
{
    class metric
    {
        Database DB = new Database("spa_metric");
        
        public metric()
        {
        }

        public Guid metric_id { get; set; }
        public Guid client_id { get; set; }
        public DateTime business_date { get; set; }
        public int dimension_1_id { get; set; }
        public String dimension_1_name { get; set; }
        public int dimension_2_id { get; set; }
        public String dimension_2_name { get; set; }
        public int dimension_3_id { get; set; }
        public String dimension_3_name { get; set; }
        public decimal value_1 { get; set; }
        public decimal value_2 { get; set; }
        public decimal value_3 { get; set; }

        public void Save()
        {
            
            ArrayList myParams = new ArrayList();

            myParams.Add(DB.CreateParameter("metric_id", typeof(String), metric_id));
            myParams.Add(DB.CreateParameter("client_id", typeof(String), client_id));
            myParams.Add(DB.CreateParameter("business_date", typeof(DateTime), business_date));
            myParams.Add(DB.CreateParameter("dimension_1_id", typeof(int), dimension_1_id));
            myParams.Add(DB.CreateParameter("dimension_1_name", typeof(string), dimension_1_name));
            myParams.Add(DB.CreateParameter("dimension_2_id", typeof(int), dimension_2_id));
            myParams.Add(DB.CreateParameter("dimension_2_name", typeof(string), dimension_2_name));
            myParams.Add(DB.CreateParameter("dimension_3_id", typeof(int), dimension_3_id));
            myParams.Add(DB.CreateParameter("dimension_3_name", typeof(string), dimension_3_name));
            myParams.Add(DB.CreateParameter("value_1", typeof(Decimal), value_1));
            myParams.Add(DB.CreateParameter("value_2", typeof(Decimal), value_2));
            myParams.Add(DB.CreateParameter("value_3", typeof(Decimal), value_3));

            DB.ExecuteSQL(SQL, myParams);
        }

        public string SQL = @"
IF EXISTS ( SELECT  1 
            FROM    metric 
            WHERE   metric_id = @metric_id
            AND     client_id = @client_id
            AND     business_date = @business_date
            AND     dimension_1_id = @dimension_1_id
            AND     dimension_2_id = @dimension_2_id
            AND     dimension_3_id = @dimension_3_id
           )
BEGIN
    UPDATE  metric
    SET     value_1 = @value_1,
            value_2 = @value_2,
            value_3 = @value_3,
            modified_date = getdate()
    WHERE   metric_id = @metric_id
    AND     client_id = @client_id
    AND     business_date = @business_date
    AND     dimension_1_id = @dimension_1_id
    AND     dimension_2_id = @dimension_2_id
    AND     dimension_3_id = @dimension_3_id
END
ELSE
BEGIN
    INSERT INTO metric
               (metric_id,
                client_id,
                business_date,
                dimension_1_id,
                dimension_1_name,
                dimension_2_id,
                dimension_2_name,
                dimension_3_id,
                dimension_3_name,
                value_1,
                value_2,
                value_3,
                created_date,
                modified_date)
         VALUES
               (@metric_id,
                @client_id,
                @business_date,
                @dimension_1_id,
                @dimension_1_name,
                @dimension_2_id,
                @dimension_2_name,
                @dimension_3_id,
                @dimension_3_name,
                @value_1,
                @value_2,
                @value_3,
                getdate(),
                getdate())
END
";


    }
}
