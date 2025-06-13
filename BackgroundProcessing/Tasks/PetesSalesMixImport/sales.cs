using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AsyncLibrary;

namespace PetesSalesMixImport
{
    class sales
    {
        Database DB = new Database("spa_custom");
        
        public sales()
        {
        }

        public int bu_id { get; set; }
        public String bu_name { get; set; }
        public DateTime bu_date { get; set; }
        public String department { get; set; }
        public String category { get; set; }
        public String sub_category { get; set; }
        public String item_name { get; set; }
        public Decimal gross_sold_qty { get; set; }
        public Decimal gross_sold_amt { get; set; }
        public Decimal net_sold_amt { get; set; }
        public Decimal refund_amt { get; set; }
        public Decimal reduction_amt { get; set; }

        public void Save()
        {
            
            ArrayList myParams = new ArrayList();

            myParams.Add(DB.CreateParameter("bu_id", typeof(int), bu_id));
            myParams.Add(DB.CreateParameter("bu_name", typeof(string), bu_name));
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), bu_date));
            myParams.Add(DB.CreateParameter("department", typeof(string), department));
            myParams.Add(DB.CreateParameter("category", typeof(string), category));
            myParams.Add(DB.CreateParameter("sub_category", typeof(string), sub_category));
            myParams.Add(DB.CreateParameter("item_name", typeof(string), item_name));
            myParams.Add(DB.CreateParameter("gross_sold_qty", typeof(Decimal), gross_sold_qty));
            myParams.Add(DB.CreateParameter("gross_sold_amt", typeof(Decimal), gross_sold_amt));
            myParams.Add(DB.CreateParameter("net_sold_amt", typeof(Decimal), net_sold_amt));
            myParams.Add(DB.CreateParameter("refund_amt", typeof(Decimal), refund_amt));
            myParams.Add(DB.CreateParameter("reduction_amt", typeof(Decimal), reduction_amt));

            DB.ExecuteSQL(SQL, myParams);
        }

        public string SQL = @"
IF EXISTS ( SELECT  1 
            FROM    custom_sales
            WHERE   bu_id = @bu_id
            AND     bu_date = @bu_date
            AND     department = @department
            AND     category = @category
            AND     sub_category = @sub_category
            AND     item_name = @item_name
           )
BEGIN
    UPDATE  custom_sales
    SET     gross_sold_qty = @gross_sold_qty,
            gross_sold_amt = @gross_sold_amt,
            net_sold_amt = @net_sold_amt,
            refund_amt = @refund_amt,
            reduction_amt = @reduction_amt,
            modified_date = getdate()
            WHERE   bu_id = @bu_id
            AND     bu_date = @bu_date
            AND     department = @department
            AND     category = @category
            AND     sub_category = @sub_category
            AND     item_name = @item_name
END
ELSE
BEGIN
    INSERT INTO custom_sales
               (bu_id,
                bu_name,
                bu_date,
                department,
                category,
                sub_category,
                item_name,
                gross_sold_qty,
                gross_sold_amt,
                net_sold_amt,
                refund_amt,
                reduction_amt,
                created_date,
                modified_date)
         VALUES
               (@bu_id,
                @bu_name,
                @bu_date,
                @department,
                @category,
                @sub_category,
                @item_name,
                @gross_sold_qty,
                @gross_sold_amt,
                @net_sold_amt,
                @refund_amt,
                @reduction_amt,
                getdate(),
                getdate())
END
";


    }
}
