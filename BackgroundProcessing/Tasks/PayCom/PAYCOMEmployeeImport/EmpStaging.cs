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
using AsyncLibrary;

namespace Objects
{

    public class EmpStaging
    {
        Database DB = new Database("spa_workforce");

        public EmpStaging()
        {
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

            myParams.Add(DB.CreateParameter("BUCode", typeof(string), BUCode));
            myParams.Add(DB.CreateParameter("EmpID", typeof(string), EmpID));
            myParams.Add(DB.CreateParameter("FirstName", typeof(string), FirstName));
            myParams.Add(DB.CreateParameter("LastName", typeof(string), LastName));
            myParams.Add(DB.CreateParameter("MiddleName", typeof(string), MiddleName));
            myParams.Add(DB.CreateParameter("AddressLine1", typeof(string), AddressLine1));
            myParams.Add(DB.CreateParameter("AddressLine2", typeof(string), AddressLine2));
            myParams.Add(DB.CreateParameter("Email", typeof(string), Email));
            myParams.Add(DB.CreateParameter("City", typeof(string), City));
            myParams.Add(DB.CreateParameter("State", typeof(string), State));
            myParams.Add(DB.CreateParameter("Country", typeof(string), Country));
            myParams.Add(DB.CreateParameter("PostalCode", typeof(string), PostalCode));
            myParams.Add(DB.CreateParameter("HomePhone", typeof(string), HomePhone));
            myParams.Add(DB.CreateParameter("CellPhone", typeof(string), CellPhone));
            myParams.Add(DB.CreateParameter("ContactRelation", typeof(string), ContactRelation));
            myParams.Add(DB.CreateParameter("ContactHomePhone", typeof(string), ContactHomePhone));
            myParams.Add(DB.CreateParameter("ContactFullName", typeof(string), ContactFullName));
            myParams.Add(DB.CreateParameter("ContactWorkPhone", typeof(string), ContactWorkPhone));
            myParams.Add(DB.CreateParameter("AlternatePhone", typeof(string), AlternatePhone));
            myParams.Add(DB.CreateParameter("Phone", typeof(string), Phone));
            myParams.Add(DB.CreateParameter("NationalID", typeof(string), NationalID));
            myParams.Add(DB.CreateParameter("BirthDate", typeof(string), BirthDate));
            myParams.Add(DB.CreateParameter("HireDate", typeof(string), HireDate));
            myParams.Add(DB.CreateParameter("SeniorityDate", typeof(string), SeniorityDate));
            myParams.Add(DB.CreateParameter("InactiveDate", typeof(string), InactiveDate));
            myParams.Add(DB.CreateParameter("ReHireDate", typeof(string), ReHireDate));
            myParams.Add(DB.CreateParameter("OriginalHireDate", typeof(string), OriginalHireDate));
            myParams.Add(DB.CreateParameter("StatusReportingDate", typeof(string), StatusReportingDate));
            myParams.Add(DB.CreateParameter("TerminationDate", typeof(string), TerminationDate));
            myParams.Add(DB.CreateParameter("TerminationReason", typeof(string), TerminationReason));
            myParams.Add(DB.CreateParameter("TerminationType", typeof(string), TerminationType));
            myParams.Add(DB.CreateParameter("TerminationDetail", typeof(string), TerminationDetail));
            myParams.Add(DB.CreateParameter("PayrollSystemNumber", typeof(string), PayrollSystemNumber));
            myParams.Add(DB.CreateParameter("RateType", typeof(string), RateType));
            myParams.Add(DB.CreateParameter("Rate", typeof(string), Rate));
            myParams.Add(DB.CreateParameter("RateReason", typeof(string), RateReason));
            myParams.Add(DB.CreateParameter("RateDetail", typeof(string), RateDetail));
            myParams.Add(DB.CreateParameter("Status", typeof(string), Status));
            myParams.Add(DB.CreateParameter("EmployeeStatusDate", typeof(string), EmployeeStatusDate));
            myParams.Add(DB.CreateParameter("RecordType", typeof(string), RecordType));
            myParams.Add(DB.CreateParameter("FullPartType", typeof(string), FullPartType));
            myParams.Add(DB.CreateParameter("UserLevel", typeof(string), UserLevel));
            myParams.Add(DB.CreateParameter("PreviousJob", typeof(string), PreviousJob));
            myParams.Add(DB.CreateParameter("JobStartDate", typeof(string), JobStartDate));
            myParams.Add(DB.CreateParameter("RateStartDate", typeof(string), RateStartDate));
            myParams.Add(DB.CreateParameter("JobID", typeof(string), JobID));
            myParams.Add(DB.CreateParameter("JobName", typeof(string), JobName));
            myParams.Add(DB.CreateParameter("PrimaryJob", typeof(string), PrimaryJob));
            myParams.Add(DB.CreateParameter("JobTitle", typeof(string), JobTitle));
            myParams.Add(DB.CreateParameter("POSCode", typeof(string), POSCode));
            myParams.Add(DB.CreateParameter("EmploymentReason", typeof(string), EmploymentReason));
            myParams.Add(DB.CreateParameter("EmploymentDetail", typeof(string), EmploymentDetail));
            myParams.Add(DB.CreateParameter("EmploymentStatus", typeof(string), EmploymentStatus));
            myParams.Add(DB.CreateParameter("LeaveStatus", typeof(string), LeaveStatus));
            myParams.Add(DB.CreateParameter("LeaveEndDate", typeof(string), LeaveEndDate));
            myParams.Add(DB.CreateParameter("ReHireStatus", typeof(string), ReHireStatus));
            myParams.Add(DB.CreateParameter("TerminationInvoluntary", typeof(string), TerminationInvoluntary));
            myParams.Add(DB.CreateParameter("Processed", typeof(string), Processed));
            myParams.Add(DB.CreateParameter("LastModifiedDate", typeof(string), LastModifiedDate));

            DB.ExecuteSQL(SQL, myParams);
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
           ,@LastModifiedDate)
";


    }
}
