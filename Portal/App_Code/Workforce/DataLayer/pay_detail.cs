using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for member
/// </summary>
/// 
namespace DataLayer
{

    public class pay_detail
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_workforce");
        private string db_pchar = "?";

        public pay_detail()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAllByPayPeriod(string psid, string pgid, string pay_period_id, string filter, int pageNo, int rows)
        {
            string siteFilter = GetSiteFilter(psid);
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT      d.*, s.site_code, s.name as bu_name, j.job_code, j.name as job_name, e.last_name, e.first_name, c.target_code, c.description
FROM        pay_detail d

JOIN        pay_period p
ON          d.pay_period_id = p.pay_period_id

JOIN        pay_code c
ON          d.earning_code = c.source_code

JOIN        sys_site s
ON          s.site_id = d.site_id

JOIN        pay_employee e
ON          e.employee_id = d.employee_id

JOIN        pay_job j
ON          j.job_id = d.job_id

JOIN        pay_employee u
ON          u.employee_id = d.employee_id

WHERE       d.pay_period_id =  " + db_pchar + @"pay_period_id
";
            SQL += siteFilter;
            SQL += groupFilter;
            SQL += filter;
            SQL += @"
ORDER BY    last_name, first_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetAllExceptionsByPayPeriod(string psid, string pay_period_id, string filter, int pageNo, int rows)
        {
            string siteFilter = GetSiteFilter(psid);

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT      *
FROM        (
                SELECT      d.*, u.last_name, u.first_name, r.reason_code, s.site_code,
                            CASE d.exception_code WHEN 'a' THEN 'Open Pay Period'
                                                WHEN 'b' THEN 'Store Level Employee Count'
                                                WHEN 'c' THEN 'Store Level Employee Pay'
                                                WHEN 'd' THEN 'Store Level Average Hours'
                                                WHEN 'e' THEN 'Store Level OT Hour'
                                                WHEN 'f' THEN 'Employee Level Pay'
                                                WHEN 'g' THEN 'Employee Level Hours'
                                                WHEN 'h' THEN 'Day Count'
                                                WHEN 'i' THEN '40/80 Rule'
                                                WHEN 'j' THEN 'Excessive Tips'
                                                ELSE 'Unknown Exception' 
                                                END as exception_code_name,
            
                            CASE d.status_code WHEN 'o' THEN 'Open'
                                             WHEN 'c' THEN 'Closed'
                                             WHEN 'p' THEN 'Pending'
                                             ELSE 'Unknown' 
                                             END as status_code_name

                FROM        pay_exception d

                JOIN        pay_period p
                ON          d.pay_period_id = p.pay_period_id

                LEFT JOIN   sys_site s
                ON          d.site_id = s.site_id

                LEFT JOIN   pay_employee u
                ON          u.employee_id = d.employee_id

                LEFT JOIN   sys_reason r
                ON          r.reason_id = d.reason_id

                WHERE       d.pay_period_id =  " + db_pchar + @"pay_period_id
";

            SQL += siteFilter;
            SQL += filter;
            SQL += @"
) a
ORDER BY    site_code, exception_code_name";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetExceptionByID(string pay_exception_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_exception_id", typeof(string), pay_exception_id));

            string SQL = @"
SELECT      d.*, u.last_name, u.first_name, s.site_code,
            CASE d.exception_code WHEN 'a' THEN 'Open Pay Period'
                                WHEN 'b' THEN 'Store Level Employee Count'
                                WHEN 'c' THEN 'Store Level Employee Pay'
                                WHEN 'd' THEN 'Store Level Average Hours'
                                WHEN 'e' THEN 'Store Level OT Hour'
                                WHEN 'f' THEN 'Employee Level Pay'
                                WHEN 'g' THEN 'Employee Level Hours'
                                WHEN 'h' THEN 'Day Count'
                                WHEN 'i' THEN '40/80 Rule'
                                WHEN 'j' THEN 'Excessive Tips'
                                ELSE 'Unknown Exception' 
                                END as exception_code_name,
            
            CASE d.status_code WHEN 'o' THEN 'Open'
                             WHEN 'c' THEN 'Closed'
                             WHEN 'p' THEN 'Pending'
                             ELSE 'Unknown' 
                             END as status_code_name

FROM        pay_exception d

LEFT JOIN   sys_site s
ON          d.site_id = s.site_id

LEFT JOIN   pay_employee u
ON          u.employee_id = d.employee_id

WHERE       d.pay_exception_id =  " + db_pchar + @"pay_exception_id
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllByEmployee(string employee_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("employee_id", typeof(string), employee_id));

            string SQL = @"
SELECT      d.*, s.site_code, s.name as bu_name, j.job_code, j.name as job_name, c.target_code, c.description
FROM        pay_employee e

JOIN        pay_detail d
ON          e.employee_id = d.employee_id

JOIN        pay_period p
ON          d.pay_period_id = p.pay_period_id

LEFT JOIN   sys_site s
ON          s.site_id = d.site_id

LEFT JOIN   pay_job j
ON          j.job_id = d.job_id

LEFT JOIN   pay_code c
ON          c.source_code = d.earning_code

WHERE       e.employee_id =  " + db_pchar + @"employee_id
";

            SQL += filter;
            SQL += @"
ORDER BY    charge_date desc, target_code
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetLevelOneSummary(string user_id, string pgid, string pay_period_id)
        {
            string siteFilter = GetSiteFilter(user_id);
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT			1 as level, d.pay_period_id, 
                d.site_id as p1, 
                s.site_code,
				sum(d.earning_hours) as earning_hours,
				sum(d.earning_amount) as earning_amount

FROM			pay_detail d

JOIN            sys_site s
ON              d.site_id = s.site_id

WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id
";

            SQL += siteFilter;
            SQL += groupFilter;
            
            SQL += @"

GROUP BY		d.pay_period_id, d.site_id,
                s.site_code

ORDER BY        s.site_code
";

            return DB.GetJSONDataSet(SQL, myParams);
        }

        public string GetLevelTwoSummary(string pgid, string pay_period_id, string site_id)
        {
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id));

            string SQL = @"
SELECT			2 as level, d.pay_period_id, 
                d.site_id as p1, 
                j.include_in_mgr_group as p2, 
                CASE j.include_in_mgr_group WHEN 'y' THEN 'Hourly Team Members'
                                            ELSE 'Hourly Managers' END AS manager,
				sum(d.earning_hours) as earning_hours,
				sum(d.earning_amount) as earning_amount

FROM			pay_detail d

JOIN			sys_site s
ON				d.site_id = s.site_id

JOIN			pay_job j
ON				d.job_id = j.job_id

WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id
AND             d.site_id = " + db_pchar + @"site_id ";

                SQL += groupFilter;
                SQL += @"

GROUP BY		d.pay_period_id, d.site_id, j.include_in_mgr_group,
                s.site_code

ORDER BY        j.include_in_mgr_group desc
";

            return DB.GetJSONDataSet(SQL, myParams);
        }

        public string GetLevelThreeSummary(string pgid, string pay_period_id, string site_id, string include_in_mgr_group)
        {
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id));
            myParams.Add(DB.CreateParameter("include_in_mgr_group", typeof(string), include_in_mgr_group));

            string SQL = @"
SELECT			3 as level, d.pay_period_id, 
                d.site_id as p1, 
                j.include_in_mgr_group as p2, 
                u.employee_id as p3,
                u.last_name + ', ' + u.first_name as username,
				sum(d.earning_hours) as earning_hours,
				sum(d.earning_amount) as earning_amount

FROM			pay_detail d

JOIN			sys_site s
ON				d.site_id = s.site_id

JOIN			pay_job j
ON				d.job_id = j.job_id

JOIN			pay_employee u
ON				d.employee_id = u.employee_id

WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id
AND             d.site_id = " + db_pchar + @"site_id
AND             j.include_in_mgr_group = " + db_pchar + @"include_in_mgr_group ";

            SQL += groupFilter;
            SQL += @"

GROUP BY		d.pay_period_id, d.site_id, j.include_in_mgr_group, u.employee_id,
                s.site_code, u.last_name, u.first_name

ORDER BY        u.last_name, u.first_name
";

            return DB.GetJSONDataSet(SQL, myParams);
        }

        public string GetLevelFourSummary(string pgid, string pay_period_id, string site_id, string include_in_mgr_group, string user_id)
        {
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id));
            myParams.Add(DB.CreateParameter("include_in_mgr_group", typeof(string), include_in_mgr_group));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
SELECT			4 as level, d.pay_period_id, 
                d.site_id as p1, 
                j.include_in_mgr_group as p2, 
                u.employee_id as p3, 
                DATEDIFF(day, '2001-01-01', d.charge_date) as p4,
                d.charge_date, 
				sum(d.earning_hours) as earning_hours,
				sum(d.earning_amount) as earning_amount

FROM			pay_detail d

JOIN			sys_site s
ON				d.site_id = s.site_id

JOIN			pay_job j
ON				d.job_id = j.job_id

JOIN			pay_employee u
ON				d.employee_id = u.employee_id

WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id
AND             d.site_id = " + db_pchar + @"site_id
AND             j.include_in_mgr_group = " + db_pchar + @"include_in_mgr_group
AND             d.employee_id = " + db_pchar + @"user_id ";

                SQL += groupFilter;
                SQL += @"

GROUP BY		d.pay_period_id, d.site_id, j.include_in_mgr_group, u.employee_id, d.charge_date,
                s.site_code, u.last_name, u.first_name

ORDER BY        d.charge_date
";

            return DB.GetJSONDataSet(SQL, myParams);
        }

        public string GetLevelFiveSummary(string pgid, string pay_period_id, string site_id, string include_in_mgr_group, string user_id, string charge_date)
        {
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("site_id", typeof(string), site_id));
            myParams.Add(DB.CreateParameter("include_in_mgr_group", typeof(string), include_in_mgr_group));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));
            myParams.Add(DB.CreateParameter("charge_date", typeof(int), charge_date));

            string SQL = @"
SELECT			5 as level, d.pay_period_id,
                d.site_id as p1, 
                j.include_in_mgr_group as p2, 
                u.employee_id as p3, 
                DATEDIFF(day, '2001-01-01', d.charge_date) as p4,
                d.earning_code as p5,
                c.target_code, c.description, d.rate,
				sum(d.earning_hours) as earning_hours,
				sum(d.earning_amount) as earning_amount

FROM			pay_detail d

JOIN			sys_site s
ON				d.site_id = s.site_id

JOIN			pay_job j
ON				d.job_id = j.job_id

JOIN			pay_employee u
ON				d.employee_id = u.employee_id

JOIN			pay_code c
ON				d.earning_code = c.source_code

WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id
AND             d.site_id = " + db_pchar + @"site_id
AND             j.include_in_mgr_group = " + db_pchar + @"include_in_mgr_group
AND             d.employee_id = " + db_pchar + @"user_id
AND             d.charge_date = DATEADD( day, " + db_pchar + @"charge_date, '2001-01-01') ";

                SQL += groupFilter;
                SQL += @"

GROUP BY		d.pay_period_id, d.site_id, j.include_in_mgr_group, u.employee_id, d.charge_date, d.earning_code,
                s.site_code, u.last_name, u.first_name, c.target_code, c.description, d.rate

ORDER BY        c.target_code
";

            return DB.GetJSONDataSet(SQL, myParams);
        }

        public string GetPaySummary(string user_id, string pgid, string pay_period_id, string filter, int pageNo, int rows)
        {
            string siteFilter = GetSiteFilter(user_id);
            string groupFilter = "";
            if (pgid != "")
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT			site_code, 
				SUM(COALESCE(total_hours,0)) as total_hours, 
				SUM(COALESCE(total_amount,0)) as total_amount, 
				SUM(COALESCE(reg_hours,0)) as reg_hours,
				SUM(COALESCE(ot_hours,0)) as ot_hours,
				SUM(COALESCE(rptme_hours,0)) as rptme_hours,
				SUM(COALESCE(rptme_amount,0)) as rptme_amount,
                SUM(COALESCE(spsft_hours,0)) as spsft_hours,
                SUM(COALESCE(spsft_amount,0)) as spsft_amount,
				SUM(COALESCE(mlbrk_hours,0)) as mlbrk_hours,
				SUM(COALESCE(dbltm_hours,0)) as dbltm_hours,
				SUM(COALESCE(jury_hours,0)) as jury_hours,
				SUM(COALESCE(breav_hours,0)) as breav_hours,
				SUM(COALESCE(hday_hours,0)) as hday_hours,
                SUM(COALESCE(tip_amount,0)) as tip_amount,
				SUM(COALESCE(other_hours,0)) as other_hours
				
				 
FROM (
		SELECT			s.site_code,   

						SUM(COALESCE(d.earning_hours,0)) as total_hours,
						SUM(COALESCE(d.earning_amount,0)) as total_amount,
			
						CASE WHEN c.target_code IN ('REG')          THEN sum(coalesce(d.earning_hours, 0))	END as reg_hours,
			
						CASE WHEN c.target_code IN ('OT')           THEN sum(coalesce(d.earning_hours, 0)) 	END as ot_hours,

						CASE WHEN c.target_code IN ('RPTME')        THEN sum(coalesce(d.earning_hours, 0))	END as rptme_hours,

						CASE WHEN c.target_code IN ('SPSFT')        THEN sum(coalesce(d.earning_hours, 0))	END as spsft_hours,

                        CASE WHEN c.target_code IN ('MLBRK')        THEN sum(coalesce(d.earning_hours, 0))	END as mlbrk_hours,

                        CASE WHEN c.target_code IN ('JURY')         THEN sum(coalesce(d.earning_hours, 0))	END as jury_hours,

						CASE WHEN c.target_code IN ('BREAV')        THEN sum(coalesce(d.earning_hours, 0))	END as breav_hours,

						CASE WHEN c.target_code IN ('HDAY')         THEN sum(coalesce(d.earning_hours, 0))	END as hday_hours,

						CASE WHEN c.target_code IN ('DBLTM')         THEN sum(coalesce(d.earning_hours, 0))	END as dbltm_hours,

                        
						CASE WHEN c.target_code IN ('RPTME')        THEN sum(coalesce(d.earning_amount, 0))	END as rptme_amount,

						CASE WHEN c.target_code IN ('SPSFT')        THEN sum(coalesce(d.earning_amount, 0))	END as spsft_amount,

						CASE WHEN c.target_code IN ('TIPCS')        THEN sum(coalesce(d.earning_amount, 0))	END as tip_amount,


                        CASE WHEN c.target_code NOT IN ('REG', 'OT', 'RPTME', 'SPSFT', 'MLBRK', 'JURY', 'BREAV', 'HDAY', 'DBLTM')
                                                                    THEN sum(coalesce(d.earning_hours, 0))	END as other_hours

		FROM			pay_detail d

		JOIN			sys_site s
		ON				d.site_id = s.site_id

		JOIN			pay_job j
		ON				d.job_id = j.job_id

		JOIN			pay_employee u
		ON				d.employee_id = u.employee_id

		JOIN			pay_code c
		ON				d.earning_code = c.source_code

		WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id 
";
            SQL += siteFilter;
            SQL += groupFilter;

            SQL += filter + @" 

		GROUP BY		s.site_code, c.target_code
) a 

GROUP BY		site_code
ORDER BY        site_code
";

            return DB.GetJSONDataSet(SQL, myParams);
        }


        private string GetSiteFilter(string user_id)
        {
            if (user_id == "ALL")
                return string.Empty;

            ArrayList myParams1 = new ArrayList();
            myParams1.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string mySQL = @"
SELECT      site_id 
FROM        sys_user_site_list
WHERE       user_id = " + db_pchar + @"user_id
";

            DataSet ds = DB.GetDataSet(mySQL, myParams1);

            string siteFilter = string.Empty;
            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
            {
                if (siteFilter == string.Empty)
                {
                    siteFilter = "'" + ds.Tables[0].Rows[x]["site_id"].ToString() + "'";
                }
                else
                {
                    siteFilter += ", '" + ds.Tables[0].Rows[x]["site_id"].ToString() + "'";
                }
            }

            if (siteFilter == string.Empty)
                return "        AND             d.site_id IN ( '" + Guid.Empty.ToString() + "' ) ";
            else
                return "        AND             d.site_id IN ( " + siteFilter + " ) ";

        }

        public string GetAllPayCodes()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      * 
FROM        pay_code
WHERE       1=1
ORDER BY    target_code
";

            return DB.GetJSONDataSet(SQL, myParams);
        }

        public void CloseAllExceptions(string pay_period_id, string exception_code, string status_code, string reason_id, string comment, string user_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));
            myParams.Add(DB.CreateParameter("exception_code", typeof(string), exception_code));
            myParams.Add(DB.CreateParameter("status_code", typeof(string), status_code));
            myParams.Add(DB.CreateParameter("reason_id", typeof(string), reason_id));
            myParams.Add(DB.CreateParameter("comment", typeof(string), comment));
            myParams.Add(DB.CreateParameter("user_id", typeof(string), user_id));

            string SQL = @"
UPDATE      pay_exception
SET         status_code = " + db_pchar + @"status_code,
            reason_id = " + db_pchar + @"reason_id,
            comment =  " + db_pchar + @"comment,
            modified_user_id = " + db_pchar + @"user_id,
            modified_date = GETDATE()
WHERE       pay_period_id = " + db_pchar + @"pay_period_id 
AND         exception_code = " + db_pchar + @"exception_code 
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public DataSet GetPaySummaryReport(string user_id, string pgid, string pay_period_id, string filter)
        {
            string siteFilter = GetSiteFilter(user_id);
            string groupFilter = "";
            if (Guid.Parse(pgid) != Guid.Empty)
            {
                groupFilter = " AND d.pay_group_id = '" + pgid + "' ";
            }

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT			site_code, 
				SUM(COALESCE(total_hours,0)) as total_hours, 
				SUM(COALESCE(total_amount,0)) as total_amount, 
				SUM(COALESCE(reg_hours,0)) as reg_hours,
				SUM(COALESCE(ot_hours,0)) as ot_hours,
				SUM(COALESCE(rptme_hours,0)) as rptme_hours,
				SUM(COALESCE(rptme_amount,0)) as rptme_amount,
                SUM(COALESCE(spsft_hours,0)) as spsft_hours,
                SUM(COALESCE(spsft_amount,0)) as spsft_amount,
				SUM(COALESCE(mlbrk_hours,0)) as mlbrk_hours,
				SUM(COALESCE(dbltm_hours,0)) as dbltm_hours,
				SUM(COALESCE(jury_hours,0)) as jury_hours,
				SUM(COALESCE(breav_hours,0)) as breav_hours,
				SUM(COALESCE(hday_hours,0)) as hday_hours,
				SUM(COALESCE(tip_amount,0)) as tip_amount,
				SUM(COALESCE(other_hours,0)) as other_hours
				
				 
FROM (
		SELECT			s.site_code,   

						SUM(COALESCE(d.earning_hours,0)) as total_hours,
						SUM(COALESCE(d.earning_amount,0)) as total_amount,
			
						CASE WHEN c.target_code IN ('REG')          THEN sum(coalesce(d.earning_hours, 0))	END as reg_hours,
			
						CASE WHEN c.target_code IN ('OT')           THEN sum(coalesce(d.earning_hours, 0)) 	END as ot_hours,

						CASE WHEN c.target_code IN ('RPTME')        THEN sum(coalesce(d.earning_hours, 0))	END as rptme_hours,

						CASE WHEN c.target_code IN ('SPSFT')        THEN sum(coalesce(d.earning_hours, 0))	END as spsft_hours,

                        CASE WHEN c.target_code IN ('MLBRK')        THEN sum(coalesce(d.earning_hours, 0))	END as mlbrk_hours,

                        CASE WHEN c.target_code IN ('JURY')         THEN sum(coalesce(d.earning_hours, 0))	END as jury_hours,

						CASE WHEN c.target_code IN ('BREAV')        THEN sum(coalesce(d.earning_hours, 0))	END as breav_hours,

						CASE WHEN c.target_code IN ('HDAY')         THEN sum(coalesce(d.earning_hours, 0))	END as hday_hours,

						CASE WHEN c.target_code IN ('DBLTM')         THEN sum(coalesce(d.earning_hours, 0))	END as dbltm_hours,

                        
						CASE WHEN c.target_code IN ('RPTME')        THEN sum(coalesce(d.earning_amount, 0))	END as rptme_amount,

						CASE WHEN c.target_code IN ('SPSFT')        THEN sum(coalesce(d.earning_amount, 0))	END as spsft_amount,

						CASE WHEN c.target_code IN ('TIPCS')        THEN sum(coalesce(d.earning_amount, 0))	END as tip_amount,


                        CASE WHEN c.target_code NOT IN ('REG', 'OT', 'RPTME', 'SPSFT', 'MLBRK', 'JURY', 'BREAV', 'HDAY', 'DBLTM')
                                                                    THEN sum(coalesce(d.earning_hours, 0))	END as other_hours

		FROM			pay_detail d

		JOIN			sys_site s
		ON				d.site_id = s.site_id

		JOIN			pay_job j
		ON				d.job_id = j.job_id

		JOIN			sys_user u
		ON				d.employee_id = u.user_id

		JOIN			pay_code c
		ON				d.earning_code = c.source_code

		WHERE			d.pay_period_id = " + db_pchar + @"pay_period_id 
";
            SQL += siteFilter;
            SQL += groupFilter;

            SQL += filter + @" 

		GROUP BY		s.site_code, c.target_code
) a 

GROUP BY		site_code
ORDER BY        site_code
";

            return DB.GetDataSet(SQL, myParams);
        }

        public DataSet GetAllExceptionsByPayPeriodReport(string psid, string pay_period_id, string filter)
        {
            string siteFilter = GetSiteFilter(psid);

            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("pay_period_id", typeof(string), pay_period_id));

            string SQL = @"
SELECT      *
FROM        (
                SELECT      d.*, u.last_name, u.first_name, r.reason_code, s.site_code,
                            CASE d.exception_code WHEN 'a' THEN 'Open Pay Period'
                                                WHEN 'b' THEN 'Store Level Employee Count'
                                                WHEN 'c' THEN 'Store Level Employee Pay'
                                                WHEN 'd' THEN 'Store Level Average Hours'
                                                WHEN 'e' THEN 'Store Level OT Hour'
                                                WHEN 'f' THEN 'Employee Level Pay'
                                                WHEN 'g' THEN 'Employee Level Hours'
                                                WHEN 'h' THEN 'Day Count'
                                                WHEN 'i' THEN '40/80 Rule'
                                                WHEN 'j' THEN 'Excessive Tips'
                                                ELSE 'Unknown Exception' 
                                                END as exception_code_name,
            
                            CASE d.status_code WHEN 'o' THEN 'Open'
                                             WHEN 'c' THEN 'Closed'
                                             WHEN 'p' THEN 'Pending'
                                             ELSE 'Unknown' 
                                             END as status_code_name

                FROM        pay_exception d

                JOIN        pay_period p
                ON          d.pay_period_id = p.pay_period_id

                LEFT JOIN   site s
                ON          d.site_id = s.site_id

                LEFT JOIN   sys_user u
                ON          u.user_id = d.employee_id

                LEFT JOIN   sys_reason r
                ON          r.reason_id = d.reason_id

                WHERE       d.pay_period_id =  " + db_pchar + @"pay_period_id
";

            SQL += siteFilter;
            SQL += filter;
            SQL += @"
) a
ORDER BY    site_code, exception_code_name";

            return DB.GetDataSet(SQL, myParams);
        }
    }
}