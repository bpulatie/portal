using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;
using System.Timers;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

namespace AsyncProcessor
{
    public partial class Main : ServiceBase
    {
        ILog logger = log4net.LogManager.GetLogger("SPALog");

        private System.Timers.Timer aTimer;
        // Set the Interval to 15 seconds (15000 milliseconds).
        private int interval = 15000;

        public Main()
        {
            InitializeComponent();

            if (!System.Diagnostics.EventLog.SourceExists("AsyncSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("AsyncSource", "AsyncLog");
            }
            asyncLog.Source = "AsyncSource";
            asyncLog.Log = "AsyncLog";

            log4net.Config.XmlConfigurator.Configure();
        }

        protected override void OnStart(string[] args)
        {
            asyncLog.WriteEntry("AsyncProcessor Starting");
            logger.Info("AysncProcessor Started.");

            aTimer = new System.Timers.Timer(15000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = interval;
            aTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            logger.Info("AysncProcessor Stopping.");
            asyncLog.WriteEntry("AsyncProcessor Stopping");
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            aTimer.Stop();

            try
            {
                QueueScheduledJobs();
                ExecuteQueuedJobs();
            }
            catch (Exception ex)
            {
                logger.Error("AysncProcessor: Error - " + ex.Message);
            }

            aTimer.Start();
        }

        private void QueueScheduledJobs()
        {
            //logger.Info("AysncProcessor: Queuing Hourly Jobs");
            //DataTable dtHourly = GetHourlyJobs();
            //QueueJobs(dtHourly);

            //logger.Info("AysncProcessor: Queuing Daily Jobs");
            //DataTable dtDaily = GetDailyJobs();
            //QueueJobs(dtDaily);

            logger.Info("AysncProcessor: Queuing Scheduled Jobs");
            DataTable dtSchedule = GetScheduledJobs();
            QueueJobs(dtSchedule);
        }

        private DataTable GetScheduledJobs()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT	*
FROM	(
	        SELECT		COALESCE(e.last_queued_time, schedule_time) as last_queued_time,
				        DATEADD(minute, schedule_frequency, COALESCE(e.last_queued_time, schedule_time)) as next_queued_time,
				        DATEDIFF(minute, schedule_time, getdate()) % schedule_frequency as factor,
				        j.*

	        FROM		async_job j
	        LEFT JOIN
				        (
					        SELECT		job_id, MAX(queued_time) as last_queued_time
					        FROM		async_execution
							WHERE		execution_type = 's'
					        GROUP BY	job_id
				        ) e
	        ON			j.job_id = e.job_id
	        WHERE		active_flag = 'y'
	        AND         schedule_code <> 'o'
	        AND			schedule_time < GetDate()
		) a
";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncProcessor: GetScheduledJobs = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private DataTable GetHourlyJobs()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT		j.job_id, last_queued_time, schedule_code, schedule_time, job_name, job_context, client_id,
			DATEPART(minute, schedule_time) as schedule_minute, 
			DATEADD(minute, DATEPART(minute, schedule_time)  , DATEADD(hour, DATEPART(HOUR, GETDATE()), DATEADD(dd, DATEDIFF(dd, 0, getdate()), 0))) as latest_schedule_time

FROM		async_job j
LEFT JOIN	(SELECT job_id, COALESCE(max(queued_time), GETDATE()) as last_queued_time from async_execution GROUP BY job_id) e
ON			j.job_id = e.job_id 
WHERE		schedule_code = 'h'
AND			last_queued_time < DATEADD(minute, DATEPART(minute, schedule_time)  , DATEADD(hour, DATEPART(HOUR, GETDATE()), DATEADD(dd, DATEDIFF(dd, 0, getdate()), 0)))
";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncProcessor: GetHourlyJobs = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private DataTable GetDailyJobs()
        {
            ArrayList myParams = new ArrayList();

            string SQL = @"
SELECT		j.job_id, last_queued_time, schedule_code, schedule_time, job_name, job_context, client_id,
			DATEPART(minute, schedule_time) as schedule_minute, 
			DATEADD(minute, DATEPART(minute, schedule_time)  , DATEADD(hour, DATEPART(HOUR, schedule_time), DATEADD(dd, DATEDIFF(dd, 0, getdate()), 0))) as latest_schedule_time

FROM		async_job j
LEFT JOIN	(SELECT job_id, COALESCE(max(queued_time), GETDATE()) as last_queued_time from async_execution GROUP BY job_id) e
ON			j.job_id = e.job_id 
WHERE		schedule_code = 'd'
AND			last_queued_time < DATEADD(minute, DATEPART(minute, schedule_time)  , DATEADD(hour, DATEPART(HOUR, schedule_time), DATEADD(dd, DATEDIFF(dd, 0, getdate()), 0)))

";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncProcessor: GetDailyJobs = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private DateTime GetSQLDateTime()
        {
            ArrayList Params = new ArrayList();
            string SQL = @"
SELECT  GetDate() as sqlTime 
";
            DataSet ds = GetDataSet(SQL, Params);

            logger.Info("AysncProcessor: GetSQLDateTime = " + ds.Tables[0].Rows[0]["sqlTime"].ToString());

            return DateTime.Parse(ds.Tables[0].Rows[0]["sqlTime"].ToString());
        }

        private void QueueJobs(DataTable jobs)
        {
            logger.Info("AsyncProcessing: Queued Job - " + jobs.Rows.Count.ToString());

            // Maybe check the queue to see if we are already queued to prevent queing duplicate jobs

            for (int x = 0; x < jobs.Rows.Count; x++)
            {
                DataRow row = jobs.Rows[x];

                logger.Info("AsyncProcessing: Queued Job ID = " + row["job_id"].ToString());
                logger.Info("AsyncProcessing: Next Queue Time = " + row["next_queued_time"].ToString());

                DateTime nextDte;
                try
                {
                    nextDte = DateTime.Parse(row["next_queued_time"].ToString());

                    if (DateTime.Now > nextDte)
                    {
                        ArrayList myParams = new ArrayList();
                        myParams.Add(CreateParameter("job_id", typeof(string), row["job_id"].ToString()));
                        myParams.Add(CreateParameter("job_name", typeof(string), row["job_name"].ToString()));
                        myParams.Add(CreateParameter("queue_id", typeof(string), Guid.Empty));
                        myParams.Add(CreateParameter("client_id", typeof(string), row["client_id"].ToString()));
                        myParams.Add(CreateParameter("context", typeof(string), row["job_context"].ToString()));

                        logger.Info("AsyncProcessing: Queued Job Added Parameters ");

                        string SQL = @"
    INSERT INTO async_execution
    (execution_id, process_id, async_name, queued_time, start_time, end_time, status_code, queue_id, user_id, client_id, context, job_id, task_id, task_no, execution_type)
    VALUES (newid(), null, @job_name, GetDate(), null, null, 'q', @queue_id, null, @client_id, @context, @job_id, null, null, 's')
    ";

                        logger.Info("AsyncProcessing: Queued Job - Run SQL to insert job execution");

                        ExecuteSQL(SQL, myParams);

                        logger.Info("AsyncProcessing: Queued Job - " + row["job_id"].ToString());
                    }
                    else
                    {
                        logger.Info("AsyncProcessing: Not Queued too early - Job - " + row["job_id"].ToString() + " Net Queued Time = " + row["next_queued_time"].ToString());
                    }
                }
                catch
                {
                    logger.Info("AsyncProcessing: Unable to parse next queued time - Job - " + row["job_id"].ToString() + " Net Queued Time = " + row["next_queued_time"].ToString());
                }

            }
        }        

        private void ExecuteQueuedJobs()
        {
            logger.Info("AysncProcessor: Execute Queued Jobs");

            DataTable dtQueue = GetAsyncQueues();
            foreach (DataRow row in dtQueue.Rows)
            {
                string queue_id = row["queue_id"].ToString();
                int thread_count = int.Parse(row["thread_count"].ToString());
                int iProcessing = GetInProcessByQueue(queue_id);
                if (iProcessing < thread_count)
                {
                    int iJobs = thread_count - iProcessing;
                    DataTable dtJobs = GetNextJobsForQueue(queue_id, iJobs);

                    foreach (DataRow job in dtJobs.Rows)
                    {
                        LaunchAsyncJob(job);
                    }
                }
            }
        }

        private void LaunchAsyncJob(DataRow job)
        {
            logger.Info("LaunchAsyncJob: " + job["async_name"].ToString());

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSettings = configuration.AppSettings;
            string filename = appSettings.Settings["workingFolder"].Value;
            if (!filename.EndsWith(@"\"))
            {
                filename += @"\";
            }

            filename += "AsyncExecution.exe";

            logger.Info("LaunchAsyncJob: Creating Process " + filename);

            try
            {
                logger.Info("LaunchAsyncJob: ExecutionID: " + job["execution_id"].ToString());

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = filename;
                startInfo.Arguments = job["execution_id"].ToString();

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                logger.Info("LaunchAsyncJob: Failed: " + ex.Message);
                return;
            }

            logger.Info("LaunchAsyncJob: Job " + job["async_name"].ToString() + " Launched");
        }

        private DataTable GetAsyncQueues()
        {
            ArrayList Params = new ArrayList();
            string SQL = @"
SELECT  * 
FROM    async_queue
WHERE   1=1    
";
            DataSet ds = GetDataSet(SQL, Params);

            logger.Info("AysncProcessor: GetAsyncQueues = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private int GetInProcessByQueue(string queue_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("queue_id", typeof(string), queue_id));

            string SQL = @"
SELECT  count(*) as processes
FROM    async_execution
WHERE   queue_id = @queue_id
AND     status_code = 'p'    
";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncProcessor: GetInProcessByQueue = " + ds.Tables[0].Rows[0]["processes"].ToString());

            return int.Parse(ds.Tables[0].Rows[0]["processes"].ToString());
        }

        private DataTable GetNextJobsForQueue(string queue_id, int jobs)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("queue_id", typeof(string), queue_id));

            string SQL = @"
SELECT  TOP " + jobs.ToString() + @" *
FROM    async_execution
WHERE   queue_id = @queue_id
AND     status_code = 'q'    
";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncProcessor: GetNextJobsForQueue = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private DataSet GetDataSet(string SQL, ArrayList Params)
        {
            DataSet result = new DataSet();

            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection appSettings = configuration.AppSettings;
                string _connectionString = appSettings.Settings["spa_async"].Value;

                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter myCommand = new SqlDataAdapter("", myConnection))
                    {
                        myCommand.SelectCommand.CommandText = SQL;

                        // Add any parameters
                        myCommand.SelectCommand.Parameters.Clear();
                        myCommand.SelectCommand.Parameters.AddRange(Params.ToArray());

                        // Create and Fill the DataSet
                        myCommand.Fill(result);

                        myCommand.SelectCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AysncProcessor.GetDataSet: Error - " + ex.Message);
                logger.Error("AysncProcessor.GetDataSet: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error("AysncProcessor.GetDataSet: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in GetDataSet"));
            }

            // Return the DataSet
            return result;
        }

        private void ExecuteSQL(string SQL, ArrayList Params)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection appSettings = configuration.AppSettings;
                string _connectionString = appSettings.Settings["spa_async"].Value;

                // Create Instance of Connection and Command Object
                using (SqlConnection myConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand myCommand = new SqlCommand("", myConnection))
                    {
                        // The SQL
                        myCommand.CommandText = SQL;

                        // Add any parameters
                        myCommand.Parameters.AddRange(Params.ToArray());

                        // Execute the command
                        myConnection.Open();
                        myCommand.ExecuteNonQuery();

                        myCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AsyncProcessor: ExecuteSQL: Error - " + ex.Message);
                logger.Error("AsyncProcessor: ExecuteSQL: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error("AsyncProcessor: ExecuteSQL: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in ExecuteSQL"));
            }
        }

        public object CreateParameter(string name, Type myType, object value)
        {
            // Create Parameter
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@" + name;

            //Set the data type for the parameter
            if (myType == typeof(Nullable<int>) || myType == typeof(int))
            {
                param.SqlDbType = SqlDbType.Int;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<DateTime>) || myType == typeof(DateTime))
            {
                param.SqlDbType = SqlDbType.DateTime;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<bool>) || myType == typeof(bool))
            {
                param.SqlDbType = SqlDbType.Char;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                {
                    if ((Boolean)value == true)
                        param.Value = 1;
                    else
                        param.Value = 0;
                }
            }
            else if (myType == typeof(Nullable<decimal>) || myType == typeof(decimal))
            {
                param.SqlDbType = SqlDbType.Decimal;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else if (myType == typeof(Nullable<Byte>[]) || myType == typeof(Byte[]))
            {
                param.SqlDbType = SqlDbType.Image;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value;
            }
            else
            {
                param.SqlDbType = SqlDbType.VarChar;
                if (value == null)
                    param.Value = DBNull.Value;
                else
                    param.Value = value.ToString();
            }

            return (object)param;
        }
    }
}
