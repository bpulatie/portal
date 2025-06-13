using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AsyncLibrary;

namespace PetesSalesItemTransImport
{
    class sales_line
    {
        Database DB = new Database("spa_custom");
        
        public sales_line()
        {
        }

        public int bu_id { get; set; }
        public DateTime bu_date { get; set; }
        public int employee_id { get; set; }
        public int shift_id { get; set; }
        public int transaction_id { get; set; }
        public int transaction_line_id { get; set; }
        public DateTime transaction_timestamp { get; set; }
        public int sales_type_id { get; set; }
        public int sales_item_id { get; set; }
        public String barcode { get; set; }
        public Decimal sale_qty { get; set; }
        public Decimal retail_price { get; set; }
        public Decimal gross_amt { get; set; }
        public Decimal unit_price { get; set; }
        public Decimal promo_price { get; set; }
        public Decimal reduction_amt { get; set; }
        public Decimal refund_qty { get; set; }
        public Decimal refund_amt { get; set; }
        public Decimal refund_reduction_amt { get; set; }
        public int sales_dest_id { get; set; }
        public String component_flag { get; set; }
        public String item_name { get; set; }
        public String bu_name { get; set; }
        public int pump_number { get; set; }
        public int hose { get; set; }

        public void Save()
        {
            
            ArrayList myParams = new ArrayList();

            myParams.Add(DB.CreateParameter("bu_id", typeof(int), bu_id));
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), bu_date));
            myParams.Add(DB.CreateParameter("employee_id", typeof(int), employee_id));
            myParams.Add(DB.CreateParameter("shift_id", typeof(int), shift_id));
            myParams.Add(DB.CreateParameter("transaction_id", typeof(int), transaction_id));
            myParams.Add(DB.CreateParameter("transaction_line_id", typeof(int), transaction_line_id));
            myParams.Add(DB.CreateParameter("transaction_timestamp", typeof(DateTime), transaction_timestamp));
            myParams.Add(DB.CreateParameter("sales_type_id", typeof(int), sales_type_id));
            myParams.Add(DB.CreateParameter("sales_item_id", typeof(int), shift_id));
            myParams.Add(DB.CreateParameter("barcode", typeof(string), barcode));
            myParams.Add(DB.CreateParameter("sale_qty", typeof(Decimal), sale_qty));
            myParams.Add(DB.CreateParameter("retail_price", typeof(Decimal), retail_price));
            myParams.Add(DB.CreateParameter("gross_amt", typeof(Decimal), gross_amt));
            myParams.Add(DB.CreateParameter("unit_price", typeof(Decimal), unit_price));
            myParams.Add(DB.CreateParameter("promo_price", typeof(Decimal), promo_price));
            myParams.Add(DB.CreateParameter("reduction_amt", typeof(Decimal), reduction_amt));
            myParams.Add(DB.CreateParameter("refund_qty", typeof(Decimal), refund_qty));
            myParams.Add(DB.CreateParameter("refund_amt", typeof(Decimal), refund_amt));
            myParams.Add(DB.CreateParameter("refund_reduction_amt", typeof(Decimal), refund_reduction_amt));
            myParams.Add(DB.CreateParameter("sales_dest_id", typeof(int), sales_dest_id));
            myParams.Add(DB.CreateParameter("component_flag", typeof(String), component_flag));
            myParams.Add(DB.CreateParameter("item_name", typeof(string), item_name));
            myParams.Add(DB.CreateParameter("bu_name", typeof(string), bu_name));
            myParams.Add(DB.CreateParameter("pump_number", typeof(int), pump_number));
            myParams.Add(DB.CreateParameter("hose", typeof(int), hose));

            DB.ExecuteSQL(SQL, myParams);
        }

        public string SQL = @"
IF EXISTS ( SELECT  1 
            FROM    custom_sales_lines
            WHERE   bu_id = @bu_id
            AND     bu_date = @bu_date
            AND     employee_id = @employee_id
            AND     shift_id = @shift_id
            AND     transaction_id = @transaction_id
            AND     transaction_line_id = @transaction_line_id
           )
BEGIN
    UPDATE  custom_sales_lines
    SET     transaction_timestamp = @transaction_timestamp,
            sales_type_id = @sales_type_id,
            sales_item_id = @sales_item_id,
            barcode = @barcode,
            sale_qty = @sale_qty,
            retail_price = @retail_price,
            gross_amt = @gross_amt,
            unit_price = @unit_price,
            promo_price = @promo_price,
            reduction_amt = @reduction_amt,
            refund_qty = @refund_qty,
            refund_amt = @refund_amt,
            refund_reduction_amt = @refund_reduction_amt,
            sales_dest_id = @sales_dest_id,
            component_flag = @component_flag,
            item_name = @item_name,
            bu_name = @bu_name,
            pump_number = @pump_number,
            hose = @hose,
            modified_date = getdate()
            WHERE   bu_id = @bu_id
            AND     bu_date = @bu_date
            AND     employee_id = @employee_id
            AND     shift_id = @shift_id
            AND     transaction_id = @transaction_id
            AND     transaction_line_id = @transaction_line_id
END
ELSE
BEGIN
    INSERT INTO custom_sales_lines
               (bu_id,
                bu_date,
                employee_id,
                shift_id,
                transaction_id,
                transaction_line_id,
                transaction_timestamp,
                sales_type_id,
                sales_item_id,
                barcode,
                sale_qty,
                retail_price,
                gross_amt,
                unit_price,
                promo_price,
                reduction_amt,
                refund_qty,
                refund_amt,
                refund_reduction_amt,
                sales_dest_id,
                component_flag,
                item_name,
                bu_name,
                pump_number,
                hose,
                created_date,
                modified_date)
         VALUES
               (@bu_id,
                @bu_date,
                @employee_id,
                @shift_id,
                @transaction_id,
                @transaction_line_id,
                @transaction_timestamp,
                @sales_type_id,
                @sales_item_id,
                @barcode,
                @sale_qty,
                @retail_price,
                @gross_amt,
                @unit_price,
                @promo_price,
                @reduction_amt,
                @refund_qty,
                @refund_amt,
                @refund_reduction_amt,
                @sales_dest_id,
                @component_flag,
                @item_name,
                @bu_name,
                @pump_number,
                @hose,
                getdate(),
                getdate())
END
";


    }
}
