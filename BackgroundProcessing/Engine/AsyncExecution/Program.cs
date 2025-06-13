using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace AsyncExecution
{
    class Program
    {
        private static ILog logger = log4net.LogManager.GetLogger("SPALog");
        private static string _connectionString = string.Empty;

        static void Main(string[] args)
        {
            string execution_id = string.Empty;
            string taskFolder = string.Empty;
            string job_id = string.Empty;
            string task_id = string.Empty;
            string path = string.Empty;
            int task_no = -1;

            try
            {
                string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AsyncProcessor.exe");

                Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
                AppSettingsSection appSettings = configuration.AppSettings;

                log4net.Config.XmlConfigurator.Configure();

                logger.Info("AysncExecution Started.");

                int x = 0;
                foreach (string arg in args)
                {
                    x++;
                    logger.Info("AysncExecution: Arg" + x.ToString() + " = " + arg);

                    execution_id = arg;
                }

                path = appSettings.Settings["taskFolder"].Value;
                if (!path.EndsWith(@"\"))
                {
                    path += @"\";
                }

                _connectionString = appSettings.Settings["spa_async"].Value;
            }
            catch (Exception ex)
            {
                logger.Error("AsyncExecution " + ex.Message);
            }

            Process p = Process.GetCurrentProcess();
            logger.Info("AysncExecution: ProcessID = " + p.Id.ToString());

            try
            {
                DataTable ds = GetExecutionJob(execution_id);
                job_id = ds.Rows[0]["job_id"].ToString();

                UpdateStatusToStarted(execution_id, job_id, p);

                DataTable dsTasks = GetTasks(ds.Rows[0]["job_id"].ToString());
                for(int y = 0; y < dsTasks.Rows.Count; y++)
                {
                    DataRow row = dsTasks.Rows[y];

                    task_id = row["task_id"].ToString();
                    try
                    {
                        task_no = int.Parse(row["task_no"].ToString());
                    }
                    catch (Exception ex)
                    {
                        task_no = -1;
                    }

                    DataTable dsExecute = GetExecutionJob(execution_id);
                    UpdateTaskStatusToStarted(execution_id, job_id, task_id, task_no, p);

                    logger.Info("AysncExecution: LoadFile = " + path + row["moniker"].ToString() + ".dll");
                    Assembly a = Assembly.LoadFile(path + row["moniker"].ToString() + ".dll");

                    logger.Info("AysncExecution: GetType - Class " + row["moniker"].ToString() + ".Main");
                    Type t = a.GetType(row["moniker"].ToString() + ".Main");

                    logger.Info("AysncExecution: GetMethod - OnExecute");
                    MethodInfo m = t.GetMethod("OnExecute");

                    logger.Info("AysncExecution: Invoking Execute Method - Context " + dsExecute.Rows[0]["context"].ToString());
                    string[] Args = new string[] { execution_id, dsExecute.Rows[0]["context"].ToString() };
                    var result = m.Invoke(null, new object[] { Args });

                    UpdateTaskStatusToComplete(execution_id, job_id, task_id, task_no, p);
                }
            }
            catch (Exception ex)
            {
                logger.Error("AysncExecution: GetExecutionJob Failed = " + execution_id, ex);

                UpdateTaskStatusToFailed(execution_id, job_id, task_id, task_no, p, ex);
                throw;
            }

            UpdateStatusToComplete(execution_id);

            logger.Info("AysncExecution Completed.");
        }

        private static void UpdateStatusToStarted(string execution_id, string job_id, Process p)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(CreateParameter("job_id", typeof(string), job_id));
            myParams.Add(CreateParameter("process_id", typeof(int), p.Id));

            string SQL = @"
UPDATE      async_execution
SET         status_code = 'p',
            job_id = @job_id, 
            process_id = @process_id,
            start_time = GetDate()
WHERE       execution_id = @execution_id
";

            ExecuteSQL(SQL, myParams);
        }

        private static void UpdateStatusToComplete(string execution_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("execution_id", typeof(string), execution_id));

            string SQL = @"
UPDATE      async_execution
SET         status_code = 'c',
            end_time = GetDate()
WHERE       execution_id = @execution_id
";

            ExecuteSQL(SQL, myParams);
        }

        private static void UpdateTaskStatusToStarted(string execution_id, string job_id, string task_id, int task_no, Process p)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(CreateParameter("process_id", typeof(int), p.Id));
            myParams.Add(CreateParameter("job_id", typeof(string), job_id));
            myParams.Add(CreateParameter("task_id", typeof(string), task_id));
            myParams.Add(CreateParameter("task_no", typeof(int), task_no));
            myParams.Add(CreateParameter("status_message", typeof(string), "Starting"));

            string SQL = @"
UPDATE      async_execution
SET         task_id = @task_id
WHERE       execution_id = @execution_id;

INSERT INTO async_execution_detail
(execution_detail_id, execution_id, process_id, job_id, task_id, task_no, execution_time, status_code, status_message)
VALUES (newid(), @execution_id, @process_id, @job_id, @task_id, @task_no, GetDate(), 's', @status_message)
";

            ExecuteSQL(SQL, myParams);
        }

        private static void UpdateTaskStatusToFailed(string execution_id, string job_id, string task_id, int task_no, Process p, Exception ex)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(CreateParameter("process_id", typeof(int), p.Id));
            myParams.Add(CreateParameter("job_id", typeof(string), job_id));
            myParams.Add(CreateParameter("task_id", typeof(string), task_id));
            myParams.Add(CreateParameter("task_no", typeof(int), task_no));
            if (ex.InnerException == null)
            {
                myParams.Add(CreateParameter("status_message", typeof(string), "Failed: " + ex.Message));
            }
            else
            {
                myParams.Add(CreateParameter("status_message", typeof(string), "Failed: " + ex.InnerException.Message));
            }

            string SQL = @"
UPDATE      async_execution
SET         status_code = 'f',
            end_time = GetDate()
WHERE       execution_id = @execution_id;

INSERT INTO async_execution_detail
(execution_detail_id, execution_id, process_id, job_id, task_id, task_no, execution_time, status_code, status_message)
VALUES (newid(), @execution_id, @process_id, @job_id, @task_id, @task_no, GetDate(), 'f', @status_message)
";

            ExecuteSQL(SQL, myParams);
        }

        private static void UpdateTaskStatusToComplete(string execution_id, string job_id, string task_id, int task_no, Process p)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("execution_id", typeof(string), execution_id));
            myParams.Add(CreateParameter("process_id", typeof(int), p.Id));
            myParams.Add(CreateParameter("job_id", typeof(string), job_id));
            myParams.Add(CreateParameter("task_id", typeof(string), task_id));
            myParams.Add(CreateParameter("task_no", typeof(int), task_no));
            myParams.Add(CreateParameter("status_message", typeof(string), "Complete"));

            string SQL = @"
UPDATE      async_execution
SET         task_id = @task_id
WHERE       execution_id = @execution_id;

INSERT INTO async_execution_detail
(execution_detail_id, execution_id, process_id, job_id, task_id, task_no, execution_time, status_code, status_message)
VALUES (newid(), @execution_id, @process_id, @job_id, @task_id, @task_no, GetDate(), 'c', @status_message)
";

            ExecuteSQL(SQL, myParams);
        }

        private static DataTable GetExecutionJob(string execution_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("execution_id", typeof(string), execution_id));

            string SQL = @"
SELECT  *
FROM    async_execution
WHERE   execution_id = @execution_id
";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncExecution: GetExecutionJob = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private static DataTable GetTasks(string job_id)
        {
            ArrayList myParams = new ArrayList();
            myParams.Add(CreateParameter("job_id", typeof(string), job_id));

            string SQL = @"
SELECT      t.*, l.sort_order as task_no
FROM        async_job_task_list l
JOIN        async_task t
ON          l.task_id = t.task_id
WHERE       job_id = @job_id
ORDER BY    l.sort_order
";
            DataSet ds = GetDataSet(SQL, myParams);

            logger.Info("AysncExecution: GetTasks = " + ds.Tables[0].Rows.Count.ToString());

            return ds.Tables[0];
        }

        private static void ExecuteSQL(string SQL, ArrayList Params)
        {
            try
            {
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
                logger.Error("AsyncExecution: ExecuteSQL: Error - " + ex.Message);
                logger.Error("AsyncExecution: ExecuteSQL: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error("AsyncExecution: ExecuteSQL: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in ExecuteSQL"));
            }
        }

        private static DataSet GetDataSet(string SQL, ArrayList Params)
        {
            DataSet result = new DataSet();

            try
            {
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
                logger.Error("AysncExecution.GetDataSet: Error - " + ex.Message);
                logger.Error("AysncExecution.GetDataSet: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    logger.Error("AysncExecution.GetDataSet: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in GetDataSet " + ex.Message));
            }

            // Return the DataSet
            return result;
        }

        public static object CreateParameter(string name, Type myType, object value)
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
