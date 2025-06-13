using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Layout;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace TestPAYCOM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string context = "";
           // PAYCOMEmployeeImport.Main.OnExecute(context);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] Args = new string[] { @"C:\SPALog\PAYCOMEmployeeImport.txt",
                                            @"Data Source=172.86.160.27;Initial Catalog=SPA_Portal;Persist Security Info=True;User ID=spa_portal;Password=Kirstie92"};


            String logFile = Args[0];
            String connectionString = Args[1];
            String context = Args[1];

            SetLogger(logFile, "%date [%thread] %-5level %logger - %message%newline");
            getLog("PAYCOMEmployeeImport:", "Starting");
            getLog("PAYCOMEmployeeImport:", "Context = " + context);

            //Test
            String sid = "7cc648e4489d687ea49b280469ab650fbb3ac616bdb41962f809640dcc7c4924";
            String token = "6fd47cafb52f77b5191e65e972c40909b2fe9401256c1bbe8ad522f59a2911bf";
            //Production
            sid = "81de4df0ba30355462a619813ba79c64873e17d709712a74355aa01dc7550dab";
            token = "936b031fcb2c73380f142ca9e56309ee54b37229317f8a2c9716a3436dc70319";

            //Creating the client object
            paycomService.PaycomAPIClient client = new paycomService.PaycomAPIClient("BasicHttpsBinding_IPaycomAPI");

            //Creating an object that stores the Login response
            paycomService.PCMLoginResult loginresult = new paycomService.PCMLoginResult();

            //Calling api_login with client sid and token - required, use loginresult.session_id for the session id            
            loginresult = client.api_login(sid, token);

            //Calling api_login with client sid and token - required, use loginresult.session_id for the session id            
            loginresult = client.api_login(sid, token);

            //////////////////////////////////////////////////////// - end login

            if (loginresult.ErrorMessages.Count() > 0)
            {
                getLog("PAYCOMEmployeeImport:", "Login Error - " + loginresult.ErrorMessages[0].error);
                throw new Exception(loginresult.ErrorMessages[0].error);
            }

            getLog("PAYCOMEmployeeImport:", "Login Succeeded");



            //Creating an object that stores the response of Get Employee call
            paycomService.PCMEmployeeResult getEmployeeResult = new paycomService.PCMEmployeeResult();
            /*
            * Calling api_get_employee with
            * -> session_id generated from api_login call - required
            * -> an eecode e.g. "A234"- could be left as an empty string("") for all employee info
            * Use getEmployeeResult.EmployeeResult for employee info or getEmployeeResult.ErrorMessages for error messages
            */
            getEmployeeResult = client.api_get_employee(loginresult.session_id, "");

            if (getEmployeeResult.ErrorMessages.Count() > 0)
            {
                getLog("PAYCOMEmployeeImport:", "Retrieval Error - " + getEmployeeResult.ErrorMessages[0].error);
                throw new Exception(getEmployeeResult.ErrorMessages[0].error);
            }


            foreach (dynamic worker in getEmployeeResult.EmployeeResult)
            {
                Objects.EmpStaging oStage = new Objects.EmpStaging(connectionString, logFile);

                getLog("PAYCOMEmployeeImport:", "worker.Employee_Name=" + worker.Employee_Name);

                try { oStage.BUCode = worker.Supervisor_Primary_Code; }
                catch { };
                try { oStage.EmpID = worker.Employee_Code; }
                catch { };
                try { oStage.FirstName = worker.FirstName; }
                catch { };
                try { oStage.LastName = worker.LastName; }
                catch { };
                try { oStage.MiddleName = worker.MiddleName; }
                catch { };
                try { oStage.AddressLine1 = worker.Street; }
                catch { };
                try { oStage.AddressLine2 = worker.Street_Address; }
                catch { };
                try { oStage.Email = worker.Work_Email; }
                catch { };
                try { oStage.City = worker.City_Address; }
                catch { };
                try { oStage.State = worker.HomeState; }
                catch { };
                try { oStage.Country = "US"; }
                catch { };
                try { oStage.PostalCode = worker.ZipCode; }
                catch { };
                try { oStage.HomePhone = worker.HomePhone; }
                catch { };
                try { oStage.CellPhone = worker.Primary_Phone; }
                catch { };
                try { oStage.ContactRelation = worker.Emergency_1_Relationship; }
                catch { };
                try { oStage.ContactHomePhone = worker.Emergency_1_Phone; }
                catch { };
                try { oStage.ContactFullName = worker.Emergency_1_Contact; }
                catch { };
                try { oStage.ContactWorkPhone = worker.Emergency_2_Phone; }
                catch { };
                try { oStage.AlternatePhone = worker.Emergency_3_Phone; }
                catch { };
                try { oStage.Phone = worker.HomePhone; }
                catch { };
                try { oStage.NationalID = worker.Employee_Type_1099; }
                catch { };
                try { oStage.BirthDate = worker.Birth_Date; }
                catch { };
                try { oStage.HireDate = worker.Hire_Date; }
                catch { };
                try { oStage.SeniorityDate = ""; }
                catch { };
                try { oStage.InactiveDate = ""; }
                catch { };
                try { oStage.ReHireDate = worker.Rehire_Date; }
                catch { };
                try { oStage.OriginalHireDate = worker.Hire_Date; }
                catch { };
                try { oStage.StatusReportingDate = worker.New_Hire_Report_Date; }
                catch { };
                try { oStage.TerminationDate = worker.Termination_Date; }
                catch { };
                try { oStage.TerminationReason = worker.Termination_Reason; }
                catch { };
                try { oStage.TerminationType = ""; }
                catch { };
                try { oStage.TerminationDetail = ""; }
                catch { };
                try { oStage.PayrollSystemNumber = worker.ClockSequenceNumber; }
                catch { };
                try { oStage.RateType = worker.Pay_Frequency; }
                catch { };
                try { oStage.Rate = ""; }
                catch { };
                try { oStage.Status = worker.Pay_Class; }
                catch { };
                try { oStage.EmployeeStatusDate = ""; }
                catch { };
                try { oStage.RecordType = ""; }
                catch { };
                try { oStage.FullPartType = worker.FullTime_or_PartTime; }
                catch { };
                try { oStage.UserLevel = ""; }
                catch { };
                try { oStage.PreviousJob = ""; }
                catch { };
                try { oStage.JobStartDate = ""; }
                catch { };
                try { oStage.RateStartDate = ""; }
                catch { };
                try { oStage.JobID = worker.Pay_Class; }
                catch { };
                try { oStage.JobName = worker.Position; }
                catch { };
                try { oStage.PrimaryJob = ""; }
                catch { };
                try { oStage.JobTitle = worker.Position; }
                catch { };
                try { oStage.POSCode = ""; }
                catch { };
                try { oStage.EmploymentReason = ""; }
                catch { };
                try { oStage.EmploymentStatus = ""; }
                catch { };
                try { oStage.LeaveStatus = worker.Leave_Start; }
                catch { };
                try { oStage.LeaveEndDate = worker.Leave_End; }
                catch { };
                try { oStage.ReHireStatus = ""; }
                catch { };
                try { oStage.TerminationInvoluntary = ""; }
                catch { };
                try { oStage.Processed = ""; }
                catch { };

                oStage.Save();

            }


            //////////////////////////////////////////////////////// - Logout

            //Creating an object that stores the Logout response
            paycomService.PCMLogoutResult logoutresult = new paycomService.PCMLogoutResult();

            //Will end the session - required, use logoutresult.result for response
            logoutresult = client.api_logout(loginresult.session_id);

            //////////////////////////////////////////////////////// - Logout

            getLog("PAYCOMEmployeeImport:", " Ending");
        }

        public static void SetLogger(string pathName, string pattern)
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

        public static void getLog(string className, string message)
        {
            log4net.ILog iLOG = LogManager.GetLogger(className);
            iLOG.Info(message);    // Info, Fatal, Warn, Debug
        }

    }
}
