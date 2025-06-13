using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AsyncLibrary;

namespace PetesOperstatsImport
{
    class shift
    {
        Database DB = new Database("spa_custom");
        
        public shift()
        {
        }

        public int bu_id { get; set; }
        public int employee_id { get; set; }
        public int shift_id { get; set; }
        public int drawer_id { get; set; }
        public String bu_name { get; set; }
        public String employee_name { get; set; }
        public DateTime bu_date { get; set; }
        public DateTime? eod_time { get; set; }
        public DateTime? shift_open_time { get; set; }
        public DateTime? shift_close_time { get; set; }
        public String shift_status_code { get; set; }
        public Decimal open_balance_amt { get; set; }
        public Decimal net_trans_qty { get; set; }
        public Decimal gross_sold_amt { get; set; }
        public Decimal adj_system_net_sold_amt { get; set; }
        public Decimal system_net_sold_amt { get; set; }
        public Decimal over_short_amt { get; set; }
        public Decimal cashier_override_amt { get; set; }
        public Decimal cashier_override_qty { get; set; }
        public Decimal net_discount_qty { get; set; }
        public Decimal net_discount_amt { get; set; }
        public Decimal net_coupon_qty { get; set; }
        public Decimal net_coupon_amt { get; set; }
        public Decimal no_sale_qty { get; set; }
        public Decimal refund_amt { get; set; }
        public Decimal payout_amt { get; set; }
        public Decimal item_cancel_amt { get; set; }
        public Decimal item_cancel_qty { get; set; }
        public Decimal trans_cancel_qty { get; set; }
        public Decimal trans_cancel_amt { get; set; }
        public Decimal fuel_gallons { get; set; }
        public Decimal waste_amt { get; set; }

        public void Save()
        {
            
            ArrayList myParams = new ArrayList();

            myParams.Add(DB.CreateParameter("bu_id", typeof(int), bu_id));
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), bu_date));
            myParams.Add(DB.CreateParameter("employee_id", typeof(int), employee_id));
            myParams.Add(DB.CreateParameter("shift_id", typeof(int), shift_id));
            myParams.Add(DB.CreateParameter("drawer_id", typeof(int), drawer_id));

            myParams.Add(DB.CreateParameter("shift_open_time", typeof(DateTime), shift_open_time));
            myParams.Add(DB.CreateParameter("shift_close_time", typeof(DateTime), shift_close_time));
            myParams.Add(DB.CreateParameter("shift_status_code", typeof(String), shift_status_code));
            myParams.Add(DB.CreateParameter("eod_time", typeof(DateTime), eod_time));

            myParams.Add(DB.CreateParameter("open_balance_amt", typeof(Decimal), open_balance_amt));
            myParams.Add(DB.CreateParameter("net_trans_qty", typeof(Decimal), net_trans_qty));
            myParams.Add(DB.CreateParameter("gross_sold_amt", typeof(Decimal), gross_sold_amt));
            myParams.Add(DB.CreateParameter("system_net_sold_amt", typeof(Decimal), system_net_sold_amt));
            myParams.Add(DB.CreateParameter("adj_system_net_sold_amt", typeof(Decimal), adj_system_net_sold_amt));
            myParams.Add(DB.CreateParameter("over_short_amt", typeof(Decimal), over_short_amt));
            myParams.Add(DB.CreateParameter("cashier_override_amt", typeof(Decimal), cashier_override_amt));
            myParams.Add(DB.CreateParameter("cashier_override_qty", typeof(Decimal), cashier_override_qty));
            myParams.Add(DB.CreateParameter("net_coupon_qty", typeof(Decimal), net_coupon_qty));
            myParams.Add(DB.CreateParameter("net_coupon_amt", typeof(Decimal), net_coupon_amt));
            myParams.Add(DB.CreateParameter("net_discount_qty", typeof(Decimal), net_discount_qty));
            myParams.Add(DB.CreateParameter("net_discount_amt", typeof(Decimal), net_discount_amt));
            myParams.Add(DB.CreateParameter("no_sale_qty", typeof(Decimal), no_sale_qty));
            myParams.Add(DB.CreateParameter("refund_amt", typeof(Decimal), refund_amt));
            myParams.Add(DB.CreateParameter("payout_amt", typeof(Decimal), payout_amt));
            myParams.Add(DB.CreateParameter("item_cancel_amt", typeof(Decimal), item_cancel_amt));
            myParams.Add(DB.CreateParameter("item_cancel_qty", typeof(Decimal), item_cancel_qty));
            myParams.Add(DB.CreateParameter("trans_cancel_qty", typeof(Decimal), trans_cancel_qty));
            myParams.Add(DB.CreateParameter("trans_cancel_amt", typeof(Decimal), trans_cancel_amt));
            myParams.Add(DB.CreateParameter("fuel_gallons", typeof(Decimal), fuel_gallons));
            myParams.Add(DB.CreateParameter("waste_amt", typeof(Decimal), waste_amt));

            myParams.Add(DB.CreateParameter("bu_name", typeof(string), bu_name));
            myParams.Add(DB.CreateParameter("employee_name", typeof(string), employee_name));

            DB.ExecuteSQL(SQL, myParams);
        }

        public string SQL = @"
IF EXISTS ( SELECT  1 
            FROM    custom_shifts
            WHERE   bu_id = @bu_id
            AND     bu_date = @bu_date
            AND     employee_id = @employee_id
            AND     shift_id = @shift_id
            AND     drawer_id = @drawer_id
           )
BEGIN
    UPDATE  custom_shifts
    SET     eod_time = @eod_time,
            shift_open_time = @shift_open_time,
            shift_close_time = @shift_close_time,
            shift_status_code = @shift_status_code,
            open_balance_amt = @open_balance_amt,
            net_trans_qty = @net_trans_qty,
            gross_sold_amt = @gross_sold_amt,
            system_net_sold_amt = @system_net_sold_amt,
            adj_system_net_sold_amt = @adj_system_net_sold_amt,
            over_short_amt = @over_short_amt,
            cashier_override_amt = @cashier_override_amt,
            cashier_override_qty = @cashier_override_qty,
            net_coupon_amt = @net_coupon_amt,
            net_discount_amt = @net_discount_amt,
            net_coupon_qty = @net_coupon_qty,
            net_discount_qty = @net_discount_qty,
            no_sale_qty = @no_sale_qty,
            refund_amt = @refund_amt,
            payout_amt = @payout_amt,
            item_cancel_amt = @item_cancel_amt,
            item_cancel_qty = @item_cancel_qty,
            trans_cancel_qty = @trans_cancel_qty,
            trans_cancel_amt = @trans_cancel_amt,
            fuel_gallons = @fuel_gallons,
            waste_amt = @waste_amt,
            modified_date = getdate()
            WHERE   bu_id = @bu_id
            AND     bu_date = @bu_date
            AND     employee_id = @employee_id
            AND     shift_id = @shift_id
            AND     drawer_id = @drawer_id
END
ELSE
BEGIN
    INSERT INTO custom_shifts
               (bu_id,
                bu_name,
                bu_date,
                employee_id,
                employee_name,
                shift_id,
                drawer_id,
                eod_time,
                shift_open_time,
                shift_close_time,
                shift_status_code,
                open_balance_amt,
                net_trans_qty,
                gross_sold_amt,
                system_net_sold_amt,
                adj_system_net_sold_amt,
                over_short_amt,
                cashier_override_amt,
                cashier_override_qty,
                net_discount_qty,
                net_discount_amt,
                net_coupon_qty,
                net_coupon_amt,
                no_sale_qty,
                refund_amt,
                payout_amt,
                item_cancel_amt,
                item_cancel_qty,
                trans_cancel_qty,
                trans_cancel_amt,
                fuel_gallons,
                waste_amt,
                created_date,
                modified_date)
         VALUES
               (@bu_id,
                @bu_name,
                @bu_date,
                @employee_id,
                @employee_name,
                @shift_id,
                @drawer_id,
                @eod_time,
                @shift_open_time,
                @shift_close_time,
                @shift_status_code,
                @open_balance_amt,
                @net_trans_qty,
                @gross_sold_amt,
                @system_net_sold_amt,
                @adj_system_net_sold_amt,
                @over_short_amt,
                @cashier_override_amt,
                @cashier_override_qty,
                @net_discount_qty,
                @net_discount_amt,
                @net_coupon_qty,
                @net_coupon_amt,
                @no_sale_qty,
                @refund_amt,
                @payout_amt,
                @item_cancel_amt,
                @item_cancel_qty,
                @trans_cancel_qty,
                @trans_cancel_amt,
                @fuel_gallons,
                @waste_amt,
                getdate(),
                getdate())
END
";


    }
}
