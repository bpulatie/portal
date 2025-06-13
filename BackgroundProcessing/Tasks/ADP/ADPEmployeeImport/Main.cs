using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Diagnostics;
using ADPClient.ADPException;
using ADPClient;
using System.IO;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Appender;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ADPEmployeeImport
{
    public class Main
    {
        public static void OnExecute(string [] Args)
        {
            String logFile = Args[0];
            String configFileName = Args[1];
            String connectionString = Args[2];

            SetLogger(logFile, "%date [%thread] %-5level %logger - %message%newline");
            getLog("ADPEmployeeImport:", "Starting");

            StreamReader sr = new StreamReader(configFileName);
            string clientconfig = sr.ReadToEnd();

            ADPAccessToken token = null;

            if (String.IsNullOrEmpty(clientconfig))
            {
                getLog("ADPEmployeeImport:", "Settings file or default options not available.");
            }
            else
            {
                ClientCredentialConfiguration connectionCfg = JSONUtil.Deserialize<ClientCredentialConfiguration>(clientconfig);
                ClientCredentialConnection connection = (ClientCredentialConnection)ADPApiConnectionFactory.createConnection(connectionCfg);

                try
                {
                    connection.connect();
                    if (connection.isConnectedIndicator())
                    {
                        token = connection.accessToken;

                        getLog("ADPEmployeeImport:", " Connected to API end point");
                        getLog("ADPEmployeeImport:", " Token:");
                        getLog("ADPEmployeeImport:", "         AccessToken: " + token.AccessToken);
                        getLog("ADPEmployeeImport:", "         TokenType: " + token.TokenType);
                        getLog("ADPEmployeeImport:", "         ExpiresIn: " + token.ExpiresIn);
                        getLog("ADPEmployeeImport:", "         Scope: " + token.Scope);
                        getLog("ADPEmployeeImport:", "         ExpiresOn: " + token.ExpiresOn);

                        // var eventsUrl = "/events/core/v1/consumer-application-subscription-credentials.read";
                        // var eventsBody = "{\"events\": [{}]}";
                        // var eventsResults = connection.postADPEvent(eventsUrl, eventsBody);
                        // Console.WriteLine("\r\nEvents Data: {0} ", eventsResults);

                        String str = connection.getADPData("/hr/v2/workers?limit=5");
                        getLog("ADPEmployeeImport:", " Employee Data - " + str);

                        var converter = new ExpandoObjectConverter();
                        dynamic d = JsonConvert.DeserializeObject<ExpandoObject>(str, converter); 

                        foreach(dynamic worker in d.workers)
                        {
                            Objects.EmpStaging oStage = new Objects.EmpStaging(connectionString, logFile);

                            oStage.EmpID = worker.workerID.idValue;
                            oStage.FirstName = worker.person.legalName.givenName;
                            oStage.LastName = worker.person.legalName.familyName1;
                            oStage.AddressLine1 = worker.person.legalAddress.lineOne; 
                            oStage.Email = worker.businessCommunication.emails[0].emailUri; 
                            oStage.City = worker.person.legalAddress.cityName; 
                            oStage.State = worker.person.legalAddress.countrySubdivisionLevel1.codeValue; 
                            oStage.Country = worker.person.legalAddress.countryCode; 
                            oStage.PostalCode = worker.person.legalAddress.postalCode; 
                            oStage.HomePhone = worker.person.communication.landlines[0].formattedNumber; 
                            oStage.CellPhone = worker.person.communication.mobiles[0].formattedNumber; 
                            oStage.Phone = worker.businessCommunication.landlines[0].formattedNumber; 
                            oStage.NationalID = worker.person.governmentIDs[0].idValue; 
                            oStage.BirthDate = worker.person.birthDate; 
                            oStage.HireDate = worker.workAssignments[0].hireDate; 
                            oStage.OriginalHireDate = worker.workerDates.originalHireDate; 
                            oStage.PayrollSystemNumber = worker.workAssignments[0].payrollFileNumber; 
                            oStage.JobID = worker.workAssignments[0].jobCode.codeValue; 
                            oStage.JobName = worker.workAssignments[0].jobTitle; 
                            oStage.JobTitle = worker.workAssignments[0].jobTitle;

                            oStage.Save();
                        }

                    }
                }
                catch (ADPConnectionException e)
                {
                    getLog("ADPEmployeeImport:", " Exception - " + e.Message);
                }
                catch (Exception e)
                {
                    getLog("ADPEmployeeImport:", " Exception - " + e.Message);
                }
                Console.Read();
            }

            getLog("ADPEmployeeImport:", " Ending");
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
