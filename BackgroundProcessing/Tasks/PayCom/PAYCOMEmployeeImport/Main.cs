using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using AsyncLibrary;
using Newtonsoft.Json;

namespace PAYCOMEmployeeImport
{
    public class Main
    {
        private static Database portalDB = new Database("spa_portal");
        private static Database workforceDB = new Database("spa_workforce");
        private static Logger logger = new Logger();
        private static Utils utils = new Utils();
        private static Async async = new Async();

        private static string execution_id = string.Empty;

        private static int pDaysBack = 5;

        public static void OnExecute(string[] Args)
        {
            logger.Info("PAYCOMEmployeeImport: Starting");

            execution_id = Args[0];
            logger.Info("PAYCOMEmployeeImport: Execution_id=" + execution_id);

            string context = async.GetJobContext(Args[0]);
            logger.Info("PAYCOMEmployeeImport: Context=" + context); 
            
            pDaysBack = int.Parse(utils.GetParameter(context, "DaysBack"));
            
            //Test
            String sid = "7cc648e4489d687ea49b280469ab650fbb3ac616bdb41962f809640dcc7c4924";
            String token = "6fd47cafb52f77b5191e65e972c40909b2fe9401256c1bbe8ad522f59a2911bf";
            //Production
            sid = "81de4df0ba30355462a619813ba79c64873e17d709712a74355aa01dc7550dab";
            token = "936b031fcb2c73380f142ca9e56309ee54b37229317f8a2c9716a3436dc70319";

            try
            {
                BasicHttpBinding basicbinding = new BasicHttpBinding();
                basicbinding.SendTimeout = TimeSpan.FromSeconds(300);
                basicbinding.OpenTimeout = TimeSpan.FromSeconds(300);
                basicbinding.ReceiveTimeout = TimeSpan.FromSeconds(300);
                basicbinding.Security.Mode = BasicHttpSecurityMode.Transport;
                basicbinding.MaxBufferSize = 2147483647;
                basicbinding.MaxBufferPoolSize = 2147483647;
                basicbinding.MaxReceivedMessageSize = 2147483647;
                basicbinding.ReaderQuotas.MaxDepth = 2147483647;
                basicbinding.ReaderQuotas.MaxStringContentLength = 2147483647;
                basicbinding.ReaderQuotas.MaxArrayLength = 2147483647;
                basicbinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                basicbinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

                ChannelFactory<paycomService.IPaycomAPI> myChannel = new ChannelFactory<paycomService.IPaycomAPI>(basicbinding, new EndpointAddress(new Uri("https://www.paycomonline.net/PaycomAPI/v1.1/PaycomAPI.svc")));
                
                //Creating the client object
                paycomService.IPaycomAPI client = myChannel.CreateChannel();

                //Creating an object that stores the Login response
                paycomService.PCMLoginResult loginresult = new paycomService.PCMLoginResult();
                
                //Calling api_login with client sid and token - required, use loginresult.session_id for the session id            
                loginresult = client.api_login(sid, token);

                //////////////////////////////////////////////////////// - end login

                if (loginresult.ErrorMessages.Count() > 0)
                {
                    logger.Error("PAYCOMEmployeeImport: Login Error - " + loginresult.ErrorMessages[0].error);
                    throw new Exception(loginresult.ErrorMessages[0].error);
                }

                logger.Info("PAYCOMEmployeeImport: Login Succeeded");

                string[] eecodes = new string[1] { "" };
                bool GetData = true;

                logger.Info("PAYCOMEmployeeImport: DaysBack=" + pDaysBack.ToString());
                async.Notify(execution_id, "DaysBack=" + pDaysBack.ToString());

                if (pDaysBack > 0)
                {
                    DateTime startDate = DateTime.Now;
                    startDate = startDate.AddDays(pDaysBack * -1);
                    
                    logger.Info("PAYCOMEmployeeImport: Look for Changes since " + startDate.ToShortDateString());
                    async.Notify(execution_id, "Look for Changes since " + startDate.ToShortDateString());

                    DateTime endDate = DateTime.Now;
                    List<string> emps = new List<string>();

                    //Creating an object that stores the response of Get Employee call
                    paycomService.PCMEmployeeChangeResult result = new paycomService.PCMEmployeeChangeResult();
                    result = client.api_get_eechanges(loginresult.session_id, startDate, endDate, "");


                    var resultString = JsonConvert.SerializeObject(result);
                    logger.Error("Employee Change result - " + resultString);

                    foreach (dynamic employee in result.EmployeeChanges)
                    {
                        if (emps.Contains(employee.eecode) == false)
                        {
                            logger.Info("PAYCOMEmployeeImport: Changes for - " + employee.eecode);
                            emps.Add(employee.eecode);
                        }
                    }

                    logger.Info("PAYCOMEmployeeImport: Employee Count = " + emps.Count().ToString());

                    if (emps.Count() > 0)
                    {
                        async.Notify(execution_id, "Employees Retrieved: " + emps.Count.ToString());
                        
                        eecodes = emps.ToArray();
                    }
                    else
                    {
                        async.Notify(execution_id, "No Employees Retrieved");

                        logger.Info("PAYCOMEmployeeImport: No Employees to retrieve - exiting");
                        GetData = false;
                    }
                }

                if (GetData == true)
                {
                    //Creating an object that stores the response of Get Employee call
                    paycomService.PCMStreamedEmployeeResult EmpResult = new paycomService.PCMStreamedEmployeeResult();

                    EmpResult = client.api_get_employee_streamed(loginresult.session_id, eecodes);

                    var resultString = System.Text.Encoding.Default.GetString(EmpResult.EmployeeResult);

                    dynamic Workers = JsonConvert.DeserializeObject(resultString);

                    DateTime LastModifiedDate = DateTime.Now;

                    int counter = 0;
                    foreach (dynamic worker in Workers)
                    {
                        Objects.EmpStaging oStage = new Objects.EmpStaging();

                        // getLog("PAYCOMEmployeeImport:", "worker.Employee_Name=" + worker.Employee_Name);

                        try { oStage.BUCode = worker.Department_Code; }
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
                        try { oStage.City = worker.City; }
                        catch { };
                        try { oStage.State = worker.State; }
                        catch { };
                        try { oStage.Country = "US"; }
                        catch { };
                        try { oStage.PostalCode = worker.Zipcode; }
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
                        try { oStage.RateType = worker.cat4desc; }
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
                        try { oStage.JobID = worker.cat3; }
                        catch { };
                        try { oStage.JobName = worker.cat3desc; }
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

                        oStage.LastModifiedDate = LastModifiedDate;

                        try
                        {
                            oStage.Save();
                        }
                        catch (Exception ex)
                        {
                            string sWorker = JsonConvert.SerializeObject(worker);
                            logger.Error("PAYCOMEmployeeImport: " + sWorker);
                        }
                        counter++;
                    }

                    async.Notify(execution_id, "Rows Imported: " + counter.ToString());
                }

                //////////////////////////////////////////////////////// - Logout

                //Creating an object that stores the Logout response
                paycomService.PCMLogoutResult logoutresult = new paycomService.PCMLogoutResult();
                logoutresult = client.api_logout(loginresult.session_id);

                //////////////////////////////////////////////////////// - Logout

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.Error("PAYCOMEmployeeImport: " + ex);
                    async.Notify(execution_id, "Failed: " + ex.InnerException.Message);
                }
                else
                {
                    logger.Error("PAYCOMEmployeeImport: " + ex);
                    async.Notify(execution_id, "Failed: " + ex.Message);
                }
                throw;
            }

            logger.Info("PAYCOMEmployeeImport: Ending");
        }

        
    }
}
