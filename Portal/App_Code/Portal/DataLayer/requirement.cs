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

    public class requirement
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("RDM_Repository");
        private string db_pchar = "?";

        public requirement()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetTopLevelRequirements()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT		r.*, coalesce(pl.counter,0) as counter 
FROM		requirement r
LEFT JOIN	requirement_list rl
ON			rl.requirement_id = r.requirement_id
LEFT JOIN   (SELECT		parent_list_id, count(*) as counter 
             FROM		requirement_list group by parent_list_id) pl
ON			pl.parent_list_id = r.requirement_id
WHERE		rl.parent_list_id = '00000000-0000-0000-0000-000000000000'
ORDER BY	sort_order, name      
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetRequirementById(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT		r.*, m.moniker, coalesce(pl.counter,0) as counter 
FROM		requirement r
LEFT JOIN   (SELECT		parent_list_id, count(*) as counter 
             FROM		requirement_list group by parent_list_id) pl
ON			pl.parent_list_id = r.requirement_id
JOIN		(select 		r.requirement_id, coalesce(r7.name+'->','')+coalesce(r6.name+'->','')+coalesce(r5.name+'->','')+coalesce(r4.name+'->','')+coalesce(r3.name+'->','')+coalesce(r2.name+'->','')+coalesce(r1.name+'->','')+r.name as moniker

			from 		requirement r
			left join	requirement_list rl
			on 		r.requirement_id = rl.requirement_id

			left join 	requirement r1
			on		r1.requirement_id = rl.parent_list_id
			left join	requirement_list rl1
			on 		r1.requirement_id = rl1.requirement_id

			left join 	requirement r2
			on		r2.requirement_id = rl1.parent_list_id
			left join	requirement_list rl2
			on 		r2.requirement_id = rl2.requirement_id

			left join 	requirement r3
			on		r3.requirement_id = rl2.parent_list_id
			left join	requirement_list rl3
			on 		r3.requirement_id = rl3.requirement_id

			left join 	requirement r4
			on		r4.requirement_id = rl3.parent_list_id
			left join	requirement_list rl4
			on 		r4.requirement_id = rl4.requirement_id

			left join 	requirement r5
			on		r5.requirement_id = rl4.parent_list_id
			left join	requirement_list rl5
			on 		r5.requirement_id = rl5.requirement_id

			left join 	requirement r6
			on		r6.requirement_id = rl5.parent_list_id
			left join	requirement_list rl6
			on 		r6.requirement_id = rl6.requirement_id

			left join 	requirement r7
			on		r7.requirement_id = rl6.parent_list_id
			left join	requirement_list rl7
			on 		r7.requirement_id = rl7.requirement_id
			) m
ON			m.requirement_id = r.requirement_id
WHERE		r.requirement_id = " + db_pchar + @"id
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string ChildRequirements(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT		r.*, coalesce(pl.counter,0) as counter 
FROM		requirement r
LEFT JOIN	requirement_list rl
ON			rl.requirement_id = r.requirement_id
LEFT JOIN   (SELECT		parent_list_id, count(*) as counter 
             FROM		requirement_list group by parent_list_id) pl
ON			pl.parent_list_id = r.requirement_id
WHERE		rl.parent_list_id = " + db_pchar + @"id
ORDER BY	sort_order, name
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }


        internal void DeleteLink(string from)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("from", typeof(string), from));

            string sSQL = @"
DELETE
FROM	requirement_list	
WHERE   requirement_id = @from
";

            DB.ExecuteSQL(sSQL, myParams);
        }

        internal void LinkTo(string from, string to)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("to", typeof(string), to));
            myParams.Add(DB.CreateParameter("from", typeof(string), from));
            myParams.Add(DB.CreateParameter("status", typeof(string), "a"));

            string sSQL = @"
INSERT	INTO requirement_list	
        (parent_list_id, requirement_id, list_type)
VALUES  (@to, @from, @status)
";

            DB.ExecuteSQL(sSQL, myParams);
        }

        internal void DeleteRequirement(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string sSQL = @"
DELETE
FROM	requirement
WHERE   requirement_id = @id
";

            DB.ExecuteSQL(sSQL, myParams);
        }

        internal void DeleteAttachments(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string sSQL = @"
DELETE
FROM	requirement_attachment
WHERE   requirement_id = @id
";

            DB.ExecuteSQL(sSQL, myParams);
        }

        internal object GetAttachments(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), id));

            string SQL = @"
SELECT		attachment_id, filename, attach_date, size 
FROM		requirement_attachment
WHERE		requirement_id = " + db_pchar + @"id
ORDER BY	attach_date 
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        internal void DeleteAttachment(string key)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("id", typeof(string), key));

            string SQL = @"
DELETE		
FROM		requirement_attachment
WHERE		attachment_id = " + db_pchar + @"id
";

            DB.ExecuteSQL(SQL, myParams);
        }

        public string GetSingleReportXML(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("ID", typeof(string), id));

            string SQL = @"
SELECT '1' as level_id, requirement_id, reference_no as reference, name, detail
FROM    requirement
WHERE   requirement_id = @ID";

            DataSet ds = DB.GetDataSet(SQL, myParams);

            return ds.GetXml();
        }

        public string GetReportXML(string id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("ID", typeof(string), id));

            string SQL = @"
EXEC 		list_ALL_REQUIREMENTS ";

            if (id != String.Empty)
                SQL += " @ID";

            DataSet ds = DB.GetDataSet(SQL, myParams);

            return ds.GetXml();
        }


        internal bool ValidateDrop(string from, string to)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("FROM", typeof(string), from));
            myParams.Add(DB.CreateParameter("TO", typeof(string), to));

            string SQL = @"
EXEC 		validate_drop @FROM, @TO";

            DataSet ds = DB.GetDataSet(SQL, myParams);

            if (ds.Tables[0].Rows.Count == 0)
                return true;

            return false;
        }
    }
}

