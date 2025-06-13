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

    public class async_queue
    {
        SPA.spaDatabase DB = new SPA.spaDatabase("spa_async");
        private string db_pchar = "?";

        public async_queue()
        {
            db_pchar = DB.GetParameterCharacter();
        }

        public string GetAll(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      a.*, COALESCE(q.counter,0) as queue_count, COALESCE(p.counter,0) as process_count 
FROM        async_queue a
LEFT JOIN   (   SELECT      queue_id, count(*) as counter 
                FROM        async_execution 
                WHERE       status_code = 'q'
                GROUP BY    queue_id
            ) q
ON          a.queue_id = q.queue_id
LEFT JOIN   (   SELECT      queue_id, count(*) as counter 
                FROM        async_execution 
                WHERE       status_code = 'p'
                GROUP BY    queue_id
            ) p
ON          a.queue_id = q.queue_id
WHERE       1=1
";
            SQL += filter;

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

        public string GetByID(string queue_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("queue_id", typeof(string), queue_id));

            string SQL = @"
SELECT      *
FROM        async_queue
WHERE       queue_id = " + db_pchar + @"queue_id";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllExecution(string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT      e.*, j.job_name, q.queue_name,
            CASE e.status_code WHEN 'c' THEN 'Complete'
                               WHEN 'f' THEN 'Failed' 
                               WHEN 'q' THEN 'Queued' 
                               WHEN 'p' THEN 'Processing' 
                               ELSE 'Unknown' 
                               END as status_name
FROM        async_execution e
JOIN        async_job j
ON          e.job_id = j.job_id
JOIN        async_queue q
ON          e.queue_id = q.queue_id
WHERE       1=1
";
            SQL += filter;
            SQL += @"
ORDER BY    queued_time desc
";

            return DB.GetPagedDataSet(SQL, myParams, pageNo, rows);
        }

        public string GetByExecutionID(string execution_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("execution_id", typeof(string), execution_id));

            string SQL = @"
SELECT      e.*, j.job_name, q.queue_name,
            CASE e.status_code WHEN 'c' THEN 'Complete'
                               WHEN 'f' THEN 'Failed' 
                               WHEN 'q' THEN 'Queued' 
                               WHEN 'p' THEN 'Processing' 
                               ELSE 'Unknown' 
                               END as status_name
FROM        async_execution e
JOIN        async_job j
ON          e.job_id = j.job_id
JOIN        async_queue q
ON          e.queue_id = q.queue_id
WHERE       e.execution_id = " + db_pchar + @"execution_id 
";

            return DB.GetPagedDataSet(SQL, myParams, 1, 1);
        }

        public string GetAllTasksByExecutionID(string execution_id, string filter, int pageNo, int rows)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(DB.CreateParameter("execution_id", typeof(string), execution_id));

            string SQL = @"
SELECT      e.*, t.task_name, t.moniker,
            CASE e.status_code WHEN 's' THEN 'Started'
                               WHEN 'f' THEN 'Failed' 
                               WHEN 'i' THEN 'Information' 
                               WHEN 'c' THEN 'Complete' 
                               WHEN 'p' THEN 'Processing' 
                               ELSE 'Unknown' 
                               END as status_name
FROM        async_execution_detail e
LEFT JOIN   async_task t
ON          e.task_id = t.task_id
WHERE       e.execution_id = " + db_pchar + @"execution_id 
";
            SQL += filter;
            SQL += @"
ORDER BY    execution_time
";

            return DB.GetPagedDataSet(SQL, myParams, -1, -1);
        }

    }
}