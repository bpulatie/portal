using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Appender;
using log4net;

namespace Objects
{

    public class EmpStaging
    {
        String _connectionString = String.Empty;

        public EmpStaging(String connectionString, String logFile)
        {
            _connectionString = connectionString;

            SetLogger(logFile, "%date [%thread] %-5level %logger - %message%newline");

        }

        public String BUCode { get; set; }
        public String EmpID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String Email { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public String PostalCode { get; set; }
        public String HomePhone { get; set; }
        public String CellPhone { get; set; }
        public String ContactRelation { get; set; }
        public String ContactHomePhone { get; set; }
        public String ContactFullName { get; set; }
        public String ContactWorkPhone { get; set; }
        public String AlternatePhone { get; set; }
        public String Phone { get; set; }
        public String NationalID { get; set; }
        public String BirthDate { get; set; }
        public String HireDate { get; set; }
        public String SeniorityDate { get; set; }
        public String InactiveDate { get; set; }
        public String ReHireDate { get; set; }
        public String OriginalHireDate { get; set; }
        public String StatusReportingDate { get; set; }
        public String TerminationDate { get; set; }
        public String TerminationReason { get; set; }
        public String TerminationType { get; set; }
        public String TerminationDetail { get; set; }
        public String PayrollSystemNumber { get; set; }
        public String RateType { get; set; }
        public String Rate { get; set; }
        public String RateReason { get; set; }
        public String RateDetail { get; set; }
        public String Status { get; set; }
        public String EmployeeStatusDate { get; set; }
        public String RecordType { get; set; }
        public String FullPartType { get; set; }
        public String UserLevel { get; set; }
        public String PreviousJob { get; set; }
        public String JobStartDate { get; set; }
        public String RateStartDate { get; set; }
        public String JobID { get; set; }
        public String JobName { get; set; }
        public String PrimaryJob { get; set; }
        public String JobTitle { get; set; }
        public String POSCode { get; set; }
        public String EmploymentReason { get; set; }
        public String EmploymentDetail { get; set; }
        public String EmploymentStatus { get; set; }
        public String LeaveStatus { get; set; }
        public String LeaveEndDate { get; set; }
        public String ReHireStatus { get; set; }
        public String TerminationInvoluntary { get; set; }
        public String Processed { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public void Save()
        {
            ArrayList myParams = new ArrayList();

            myParams.Add(CreateParameter("BUCode", typeof(string), BUCode));
            myParams.Add(CreateParameter("EmpID", typeof(string), EmpID));
            myParams.Add(CreateParameter("FirstName", typeof(string), FirstName));
            myParams.Add(CreateParameter("LastName", typeof(string), LastName));
            myParams.Add(CreateParameter("MiddleName", typeof(string), MiddleName));
            myParams.Add(CreateParameter("AddressLine1", typeof(string), AddressLine1));
            myParams.Add(CreateParameter("AddressLine2", typeof(string), AddressLine2));
            myParams.Add(CreateParameter("Email", typeof(string), Email));
            myParams.Add(CreateParameter("City", typeof(string), City));
            myParams.Add(CreateParameter("State", typeof(string), State));
            myParams.Add(CreateParameter("Country", typeof(string), Country));
            myParams.Add(CreateParameter("PostalCode", typeof(string), PostalCode));
            myParams.Add(CreateParameter("HomePhone", typeof(string), HomePhone));
            myParams.Add(CreateParameter("CellPhone", typeof(string), CellPhone));
            myParams.Add(CreateParameter("ContactRelation", typeof(string), ContactRelation));
            myParams.Add(CreateParameter("ContactHomePhone", typeof(string), ContactHomePhone));
            myParams.Add(CreateParameter("ContactFullName", typeof(string), ContactFullName));
            myParams.Add(CreateParameter("ContactWorkPhone", typeof(string), ContactWorkPhone));
            myParams.Add(CreateParameter("AlternatePhone", typeof(string), AlternatePhone));
            myParams.Add(CreateParameter("Phone", typeof(string), Phone));
            myParams.Add(CreateParameter("NationalID", typeof(string), NationalID));
            myParams.Add(CreateParameter("BirthDate", typeof(string), BirthDate));
            myParams.Add(CreateParameter("HireDate", typeof(string), HireDate));
            myParams.Add(CreateParameter("SeniorityDate", typeof(string), SeniorityDate));
            myParams.Add(CreateParameter("InactiveDate", typeof(string), InactiveDate));
            myParams.Add(CreateParameter("ReHireDate", typeof(string), ReHireDate));
            myParams.Add(CreateParameter("OriginalHireDate", typeof(string), OriginalHireDate));
            myParams.Add(CreateParameter("StatusReportingDate", typeof(string), StatusReportingDate));
            myParams.Add(CreateParameter("TerminationDate", typeof(string), TerminationDate));
            myParams.Add(CreateParameter("TerminationReason", typeof(string), TerminationReason));
            myParams.Add(CreateParameter("TerminationType", typeof(string), TerminationType));
            myParams.Add(CreateParameter("TerminationDetail", typeof(string), TerminationDetail));
            myParams.Add(CreateParameter("PayrollSystemNumber", typeof(string), PayrollSystemNumber));
            myParams.Add(CreateParameter("RateType", typeof(string), RateType));
            myParams.Add(CreateParameter("Rate", typeof(string), Rate));
            myParams.Add(CreateParameter("RateReason", typeof(string), RateReason));
            myParams.Add(CreateParameter("RateDetail", typeof(string), RateDetail));
            myParams.Add(CreateParameter("Status", typeof(string), Status));
            myParams.Add(CreateParameter("EmployeeStatusDate", typeof(string), EmployeeStatusDate));
            myParams.Add(CreateParameter("RecordType", typeof(string), RecordType));
            myParams.Add(CreateParameter("FullPartType", typeof(string), FullPartType));
            myParams.Add(CreateParameter("UserLevel", typeof(string), UserLevel));
            myParams.Add(CreateParameter("PreviousJob", typeof(string), PreviousJob));
            myParams.Add(CreateParameter("JobStartDate", typeof(string), JobStartDate));
            myParams.Add(CreateParameter("RateStartDate", typeof(string), RateStartDate));
            myParams.Add(CreateParameter("JobID", typeof(string), JobID));
            myParams.Add(CreateParameter("JobName", typeof(string), JobName));
            myParams.Add(CreateParameter("PrimaryJob", typeof(string), PrimaryJob));
            myParams.Add(CreateParameter("JobTitle", typeof(string), JobTitle));
            myParams.Add(CreateParameter("POSCode", typeof(string), POSCode));
            myParams.Add(CreateParameter("EmploymentReason", typeof(string), EmploymentReason));
            myParams.Add(CreateParameter("EmploymentDetail", typeof(string), EmploymentDetail));
            myParams.Add(CreateParameter("EmploymentStatus", typeof(string), EmploymentStatus));
            myParams.Add(CreateParameter("LeaveStatus", typeof(string), LeaveStatus));
            myParams.Add(CreateParameter("LeaveEndDate", typeof(string), LeaveEndDate));
            myParams.Add(CreateParameter("ReHireStatus", typeof(string), ReHireStatus));
            myParams.Add(CreateParameter("TerminationInvoluntary", typeof(string), TerminationInvoluntary));
            myParams.Add(CreateParameter("Processed", typeof(string), Processed));

            ExecuteSQL(SQL, myParams);
        }

        private DataSet GetDataSet(string SQL, ArrayList Params)
        {
            DataSet result = new DataSet();

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

            // Return the DataSet
            return result;
        }

        private void ExecuteSQL(string SQL, ArrayList Params)
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
                getLog("EmpStaging:", "ExecuteSQL: Error - " + ex.Message);
                getLog("EmpStaging:", "ExecuteSQL: SQL - " + SQL);
                foreach (SqlParameter p in Params)
                {
                    getLog("EmpStaging:", "ExecuteSQL: Parameter " + p.ParameterName + " = " + p.Value.ToString());
                }
                throw (new Exception("Error in ExecuteSQL"));
            }
        }
        
        private object CreateParameter(string name, Type myType, object value)
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

        public void SetLogger(string pathName, string pattern)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = pattern;
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = false;
            roller.File = pathName;
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "1GB";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = log4net.Core.Level.Info;
            hierarchy.Configured = true;
        }

        public void getLog(string className, string message)
        {
            log4net.ILog iLOG = LogManager.GetLogger(className);
            iLOG.Info(message);    // Info, Fatal, Warn, Debug
        }

        public string SQL = @"
INSERT INTO [dbo].[EmpStaging]
           ([BUCode]
           ,[EmpID]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[Email]
           ,[City]
           ,[State]
           ,[Country]
           ,[PostalCode]
           ,[HomePhone]
           ,[CellPhone]
           ,[ContactRelation]
           ,[ContactHomePhone]
           ,[ContactFullName]
           ,[ContactWorkPhone]
           ,[AlternatePhone]
           ,[Phone]
           ,[NationalID]
           ,[BirthDate]
           ,[HireDate]
           ,[SeniorityDate]
           ,[InactiveDate]
           ,[ReHireDate]
           ,[OriginalHireDate]
           ,[StatusReportingDate]
           ,[TerminationDate]
           ,[TerminationReason]
           ,[TerminationType]
           ,[TerminationDetail]
           ,[PayrollSystemNumber]
           ,[RateType]
           ,[Rate]
           ,[RateReason]
           ,[RateDetail]
           ,[Status]
           ,[EmployeeStatusDate]
           ,[RecordType]
           ,[FullPartType]
           ,[UserLevel]
           ,[PreviousJob]
           ,[JobStartDate]
           ,[RateStartDate]
           ,[JobID]
           ,[JobName]
           ,[PrimaryJob]
           ,[JobTitle]
           ,[POSCode]
           ,[EmploymentReason]
           ,[EmploymentDetail]
           ,[EmploymentStatus]
           ,[LeaveStatus]
           ,[LeaveEndDate]
           ,[ReHireStatus]
           ,[TerminationInvoluntary]
           ,[Processed]
           ,[LastModifiedDate])
     VALUES
           (@BUCode
           ,@EmpID
           ,@FirstName
           ,@LastName
           ,@MiddleName
           ,@AddressLine1
           ,@AddressLine2
           ,@Email
           ,@City
           ,@State
           ,@Country
           ,@PostalCode
           ,@HomePhone
           ,@CellPhone
           ,@ContactRelation
           ,@ContactHomePhone
           ,@ContactFullName
           ,@ContactWorkPhone
           ,@AlternatePhone
           ,@Phone
           ,@NationalID
           ,@BirthDate
           ,@HireDate
           ,@SeniorityDate
           ,@InactiveDate
           ,@ReHireDate
           ,@OriginalHireDate
           ,@StatusReportingDate
           ,@TerminationDate
           ,@TerminationReason
           ,@TerminationType
           ,@TerminationDetail
           ,@PayrollSystemNumber
           ,@RateType
           ,@Rate
           ,@RateReason
           ,@RateDetail
           ,@Status
           ,@EmployeeStatusDate
           ,@RecordType
           ,@FullPartType
           ,@UserLevel
           ,@PreviousJob
           ,@JobStartDate
           ,@RateStartDate
           ,@JobID
           ,@JobName
           ,@PrimaryJob
           ,@JobTitle
           ,@POSCode
           ,@EmploymentReason
           ,@EmploymentDetail
           ,@EmploymentStatus
           ,@LeaveStatus
           ,@LeaveEndDate
           ,@ReHireStatus
           ,@TerminationInvoluntary
           ,@Processed
           ,GetDate())
";


    }
}
