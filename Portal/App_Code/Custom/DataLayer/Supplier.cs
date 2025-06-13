using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for member
/// </summary>
/// 
namespace DataLayer
{

    public class supplier
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_custom");
        private string db_pchar = "?";

        public supplier()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string client_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("client_id", typeof(string), client_id));

            string SQL = @"
SELECT      *
FROM        supplier
WHERE       client_id = " + db_pchar + @"client_id
";

            SQL += filter;
            SQL += @"
ORDER BY    name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByID(string supplier_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("supplier_id", typeof(string), supplier_id));

            string SQL = @"
SELECT      *
FROM        supplier
WHERE       supplier_id = " + db_pchar + @"supplier_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }


        internal object GetOperationReport(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      s.bu_id, s.bu_name, s.bu_date, s.eod_time, 
            sum(s.net_trans_qty) as net_trans_qty,
            sum(s.gross_sold_amt) as gross_sold_amt,
            sum(s.system_net_sold_amt) as system_net_sold_amt,
            sum(s.adj_system_net_sold_amt) as adj_system_net_sold_amt,
            sum(s.over_short_amt) as over_short_amt,
            sum(COALESCE(o.override_amt, 0)) as cashier_override_amt,
            sum(COALESCE(o.override_qty, 0)) as cashier_override_qty,
            sum(s.net_coupon_amt) as net_coupon_amt,
            sum(s.net_discount_amt) as net_discount_amt,
            sum(s.no_sale_qty) as no_sale_qty,
            sum(s.refund_amt) as refund_amt,
            sum(s.payout_amt) as payout_amt,
            sum(s.item_cancel_amt) as item_cancel_amt,
            sum(s.item_cancel_qty) as item_cancel_qty,
            sum(s.trans_cancel_qty) as trans_cancel_qty,
            sum(f.fuel_gallons) as fuel_gallons,
            sum(COALESCE(d.drive_off_amt, 0)) as drive_off_amt
FROM        custom_shifts s

LEFT JOIN   (
				SELECT		bu_id, bu_date, employee_id, shift_id, sum(gross_amt) as drive_off_amt
				FROM		custom_sales_lines
				WHERE		sales_type_id = 6
				GROUP BY	bu_id, bu_date, employee_id, shift_id
			) d 
ON			s.bu_id = d.bu_id
AND			s.bu_date = d.bu_date
AND         s.employee_id = d.employee_id
AND         s.shift_id = d.shift_id

LEFT JOIN   (
				SELECT		bu_id, bu_date, employee_id, shift_id, count(*) as override_qty, sum(retail_price - unit_price) as override_amt
				FROM		custom_sales_lines
				WHERE		sales_type_id = 0
                AND         retail_price <> 0
				AND			retail_price <> unit_price
				GROUP BY	bu_id, bu_date, employee_id, shift_id
			) o
ON			s.bu_id = o.bu_id
AND			s.bu_date = o.bu_date
AND         s.employee_id = o.employee_id
AND         s.shift_id = o.shift_id

LEFT JOIN   (
				SELECT		bu_id, bu_date, employee_id, shift_id, sum(sale_qty) as fuel_gallons
				FROM		custom_sales_lines
				WHERE		pump_number is not null
				GROUP BY	bu_id, bu_date, employee_id, shift_id
			) f
ON			s.bu_id = f.bu_id
AND			s.bu_date = f.bu_date
AND         s.employee_id = f.employee_id
AND         s.shift_id = f.shift_id

WHERE       1=1
";

            SQL += filter;
            SQL += @"
GROUP BY    s.bu_id, s.bu_name, s.bu_date, s.eod_time
ORDER BY    s.bu_date desc, s.bu_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal object GetOperationReportByEmployee(string client_id, string bu_id, DateTime bu_date, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_id", typeof(string), bu_id));
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), bu_date));

            string SQL = @"
SELECT      s.bu_id, s.bu_name, s.employee_id, s.employee_name, s.bu_date, s.eod_time, 
            s.net_trans_qty as net_trans_qty,
            s.gross_sold_amt as gross_sold_amt,
            s.system_net_sold_amt as system_net_sold_amt,
            s.adj_system_net_sold_amt as adj_system_net_sold_amt,
            s.over_short_amt as over_short_amt,
            COALESCE(o.override_amt, 0) as cashier_override_amt,
            COALESCE(o.override_qty, 0) as cashier_override_qty,
            s.net_coupon_amt as net_coupon_amt,
            s.net_discount_amt as net_discount_amt,
            s.no_sale_qty as no_sale_qty,
            s.refund_amt as refund_amt,
            s.payout_amt as payout_amt,
            s.item_cancel_amt as item_cancel_amt,
            s.item_cancel_qty as item_cancel_qty,
            s.trans_cancel_qty as trans_cancel_qty,
            f.fuel_gallons as fuel_gallons,
            COALESCE(d.drive_off_amt, 0) as drive_off_amt
FROM        custom_shifts s

LEFT JOIN   (
				SELECT		bu_id, bu_date, employee_id, shift_id, sum(gross_amt) as drive_off_amt
				FROM		custom_sales_lines
				WHERE		sales_type_id = 6
				GROUP BY	bu_id, bu_date, employee_id, shift_id
			) d 
ON			s.bu_id = d.bu_id
AND			s.bu_date = d.bu_date
AND         s.employee_id = d.employee_id
AND         s.shift_id = d.shift_id

LEFT JOIN   (
				SELECT		bu_id, bu_date, employee_id, shift_id, count(*) as override_qty, sum(retail_price - unit_price) as override_amt
				FROM		custom_sales_lines
				WHERE		sales_type_id = 0
                AND         retail_price <> 0
				AND			retail_price <> unit_price
				GROUP BY	bu_id, bu_date, employee_id, shift_id
			) o
ON			s.bu_id = o.bu_id
AND			s.bu_date = o.bu_date
AND         s.employee_id = o.employee_id
AND         s.shift_id = o.shift_id

LEFT JOIN   (
				SELECT		bu_id, bu_date, employee_id, shift_id, sum(sale_qty) as fuel_gallons
				FROM		custom_sales_lines
				WHERE		pump_number is not null
				GROUP BY	bu_id, bu_date, employee_id, shift_id
			) f
ON			s.bu_id = f.bu_id
AND			s.bu_date = f.bu_date
AND         s.employee_id = f.employee_id
AND         s.shift_id = f.shift_id
AND         s.employee_id = f.employee_id
AND         s.shift_id = f.shift_id

WHERE       s.bu_id = " + db_pchar + @"bu_id
AND         s.bu_date = " + db_pchar + @"bu_date
";

            SQL += filter;
            SQL += @"

ORDER BY    s.employee_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal object GetOverridesReport(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        custom_reduction
WHERE       1=1
";

            SQL += filter;
            SQL += @"
ORDER BY    bu_date desc, bu_name, employee_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal object GetOverridesByEmployee(string client_id, string bu_id, DateTime bu_date, string employee_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_id", typeof(string), bu_id));
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), bu_date));
            myParams.Add(DB.CreateParameter("employee_id", typeof(string), employee_id));

            string SQL = @"
SELECT      i.*, s.employee_name
FROM        custom_sales_lines i
JOIN        custom_shifts s
ON          i.bu_id = s.bu_id
AND         i.bu_date = s.bu_date
AND         i.employee_id = s.employee_id
AND         i.shift_id = s.shift_id
WHERE       i.bu_id = " + db_pchar + @"bu_id
AND         i.bu_date = " + db_pchar + @"bu_date
AND         i.employee_id = " + db_pchar + @"employee_id
AND         i.retail_price <> 0
AND         i.retail_price <> i.unit_price
";

            SQL += filter;
            SQL += @"
ORDER BY    i.bu_date desc, i.bu_name, i.transaction_id, i.transaction_line_id
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal object GetSalesMixReport(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      *
FROM        custom_sales
WHERE       1=1
";

            SQL += filter;
            SQL += @"
ORDER BY    bu_date desc, bu_name, department, category, sub_category, item_name
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        internal object GetLevelOneSummary()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      1 as level, 
            CONVERT(char(8), bu_date, 112) as p1, bu_date,
            sum(gross_sold_qty) as gross_sold_qty, 
            sum(gross_sold_amt) as gross_sold_amt, 
            sum(net_sold_amt) as net_sold_amt, 
            sum(refund_amt) as refund_amt, 
            sum(reduction_amt) as reduction_amt
FROM        custom_sales
WHERE       1=1
GROUP BY    bu_date
ORDER BY    bu_date desc
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetLevelTwoSummary(string p1)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), p1));

            string SQL = @"
SELECT      2 as level, 
            CONVERT(char(8), bu_date, 112) as p1, 
            REPLACE(REPLACE(bu_name, ' ', ''), '&', '') as p2, 
            bu_name,
            sum(gross_sold_qty) as gross_sold_qty, 
            sum(gross_sold_amt) as gross_sold_amt, 
            sum(net_sold_amt) as net_sold_amt, 
            sum(refund_amt) as refund_amt, 
            sum(reduction_amt) as reduction_amt
FROM        custom_sales
WHERE       1=1
AND         bu_date = " + db_pchar + @"bu_date
GROUP BY    bu_date, bu_name
ORDER BY    bu_date desc, bu_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetLevelThreeSummary(string p1, string p2)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), p1));
            myParams.Add(DB.CreateParameter("bu_name", typeof(String), p2));

            string SQL = @"
SELECT      3 as level, 
            CONVERT(char(8), bu_date, 112) as p1, 
            REPLACE(REPLACE(bu_name, ' ', ''), '&', '') as p2, 
            REPLACE(REPLACE(department, ' ', ''), '&', '') as p3, 
            department,
            sum(gross_sold_qty) as gross_sold_qty, 
            sum(gross_sold_amt) as gross_sold_amt, 
            sum(net_sold_amt) as net_sold_amt, 
            sum(refund_amt) as refund_amt, 
            sum(reduction_amt) as reduction_amt
FROM        custom_sales
WHERE       1=1
AND         bu_date = " + db_pchar + @"bu_date
AND         REPLACE(REPLACE(bu_name, ' ', ''), '&', '') = " + db_pchar + @"bu_name
GROUP BY    bu_date, bu_name, department
ORDER BY    bu_date desc, bu_name, department
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetLevelFourSummary(string p1, string p2, string p3)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), p1));
            myParams.Add(DB.CreateParameter("bu_name", typeof(String), p2));
            myParams.Add(DB.CreateParameter("department", typeof(String), p3));

            string SQL = @"
SELECT      4 as level, 
            CONVERT(char(8), bu_date, 112) as p1, 
            REPLACE(REPLACE(bu_name, ' ', ''), '&', '') as p2, 
            REPLACE(REPLACE(department, ' ', ''), '&', '') as p3, 
            REPLACE(REPLACE(category, ' ', ''), '&', '') as p4, 
            category,
            sum(gross_sold_qty) as gross_sold_qty, 
            sum(gross_sold_amt) as gross_sold_amt, 
            sum(net_sold_amt) as net_sold_amt, 
            sum(refund_amt) as refund_amt, 
            sum(reduction_amt) as reduction_amt
FROM        custom_sales
WHERE       1=1
AND         bu_date = " + db_pchar + @"bu_date
AND         REPLACE(REPLACE(bu_name, ' ', ''), '&', '') = " + db_pchar + @"bu_name
AND         REPLACE(REPLACE(department, ' ', ''), '&', '') = " + db_pchar + @"department
GROUP BY    bu_date, bu_name, department, category
ORDER BY    bu_date desc, bu_name, department, category
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetLevelFiveSummary(string p1, string p2, string p3, string p4)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), p1));
            myParams.Add(DB.CreateParameter("bu_name", typeof(String), p2));
            myParams.Add(DB.CreateParameter("department", typeof(String), p3));
            myParams.Add(DB.CreateParameter("category", typeof(String), p4));

            string SQL = @"
SELECT      5 as level, 
            CONVERT(char(8), bu_date, 112) as p1, 
            REPLACE(REPLACE(bu_name, ' ', ''), '&', '') as p2, 
            REPLACE(REPLACE(department, ' ', ''), '&', '') as p3, 
            REPLACE(REPLACE(category, ' ', ''), '&', '') as p4, 
            REPLACE(REPLACE(sub_category, ' ', ''), '&', '') as p5, 
            sub_category, 
            sum(gross_sold_qty) as gross_sold_qty, 
            sum(gross_sold_amt) as gross_sold_amt, 
            sum(net_sold_amt) as net_sold_amt, 
            sum(refund_amt) as refund_amt, 
            sum(reduction_amt) as reduction_amt
FROM        custom_sales
WHERE       1=1
AND         bu_date = " + db_pchar + @"bu_date
AND         REPLACE(REPLACE(bu_name, ' ', ''), '&', '') = " + db_pchar + @"bu_name
AND         REPLACE(REPLACE(department, ' ', ''), '&', '') = " + db_pchar + @"department
AND         REPLACE(REPLACE(category, ' ', ''), '&', '') = " + db_pchar + @"category
GROUP BY    bu_date, bu_name, department, category, sub_category
ORDER BY    bu_date desc, bu_name, department, category, sub_category
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal object GetLevelSixSummary(string p1, string p2, string p3, string p4, string p5)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("bu_date", typeof(DateTime), p1));
            myParams.Add(DB.CreateParameter("bu_name", typeof(String), p2));
            myParams.Add(DB.CreateParameter("department", typeof(String), p3));
            myParams.Add(DB.CreateParameter("category", typeof(String), p4));
            myParams.Add(DB.CreateParameter("sub_category", typeof(String), p5));

            string SQL = @"
SELECT      6 as level, 
            CONVERT(char(8), bu_date, 112) as p1, 
            REPLACE(REPLACE(bu_name, ' ', ''), '&', '') as p2, 
            REPLACE(REPLACE(department, ' ', ''), '&', '') as p3, 
            REPLACE(REPLACE(category, ' ', ''), '&', '') as p4, 
            REPLACE(REPLACE(sub_category, ' ', ''), '&', '') as p5, 
            REPLACE(REPLACE(item_name, ' ', ''), '&', '') as p6, 
            item_name, 
            sum(gross_sold_qty) as gross_sold_qty, 
            sum(gross_sold_amt) as gross_sold_amt, 
            sum(net_sold_amt) as net_sold_amt, 
            sum(refund_amt) as refund_amt, 
            sum(reduction_amt) as reduction_amt
FROM        custom_sales
WHERE       1=1
AND         bu_date = " + db_pchar + @"bu_date
AND         REPLACE(REPLACE(bu_name, ' ', ''), '&', '') = " + db_pchar + @"bu_name
AND         REPLACE(REPLACE(department, ' ', ''), '&', '') = " + db_pchar + @"department
AND         REPLACE(REPLACE(category, ' ', ''), '&', '') = " + db_pchar + @"category
AND         REPLACE(REPLACE(sub_category, ' ', ''), '&', '') = " + db_pchar + @"sub_category
GROUP BY    bu_date, bu_name, department, category, sub_category, item_name
ORDER BY    bu_date desc, bu_name, department, category, sub_category, item_name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }
    }
}