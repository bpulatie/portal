using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.Security.Permissions;

namespace ProcessMonitor
{
    public partial class MainForm : Form
    {
        private List<myProcess> myProcessList = new List<myProcess>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            myTimer.Enabled = true;
            myTimer_Tick(sender, e);
            myTimer.Start();
        }

        private void GetProcessList()
        {
        }
        
        [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
        private void myTimer_Tick(object sender, EventArgs e)
        {
            myTimer.Stop();

            ServiceStatus.Text = GetServiceStatus();

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("ProcessName");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("TotalProcessorTime");
            dt.Columns.Add("PagedMemorySize64");
            dt.Columns.Add("MainWindow");

            Process[] processlist = Process.GetProcesses();
            foreach (Process p in processlist)
            {
                try
                {
  //                  if (p.ProcessName.StartsWith("AsyncExecution"))
                    {
                        myProcess myP = myProcessList.Find(XmlReadMode => XmlReadMode.process_id == p.Id);

                        if (myP != null)
                        {
                            dt.Rows.Add(p.Id,
                                        myP.process_name,
                                        p.StartTime.ToString(),
                                        p.TotalProcessorTime.ToString(),
                                        p.PagedMemorySize64.ToString(),
                                        p.MainModule.ModuleMemorySize.ToString()
                                        );
                        }
                        else
                        {
                            myProcess xP = new myProcess();

                            xP.process_id = p.Id;
                            xP.process_name = p.ProcessName; //"Unknown";

                            myProcessList.Add(xP);
                        }
                    }
                }
                catch
                {}
            }

            myGrid.DataSource = dt;

            myGrid.Columns[0].HeaderText = "Id";
            myGrid.Columns[1].HeaderText = "ProcessName";
            myGrid.Columns[2].HeaderText = "StartTime";
            myGrid.Columns[3].HeaderText = "TotalProcessorTime";
            myGrid.Columns[4].HeaderText = "PagedMemorySize64";
            myGrid.Columns[5].HeaderText = "MemorySize";

            myGrid.Columns[0].Width = 100;
            myGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            myGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            myGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            myGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            myGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            myGrid.Sort(myGrid.Columns[2], ListSortDirection.Ascending);

            myTimer.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string GetServiceStatus()
        {
            ServiceController sc = new ServiceController("AsyncProcessor");

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    btnStatus.Text = "Stop AsyncProcessor";
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    btnStatus.Text = "Start AsyncProcessor";
                    return "Stopped";
                case ServiceControllerStatus.Paused:
                    btnStatus.Text = "Please Wait..";
                    return "Paused";
                case ServiceControllerStatus.StopPending:
                    btnStatus.Text = "Please Wait..";
                    return "Stopping";
                case ServiceControllerStatus.StartPending:
                    btnStatus.Text = "Please Wait..";
                    return "Starting";
                default:
                    btnStatus.Text = "Please Wait..";
                    return "Status Changing";
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("AsyncProcessor");

            if (btnStatus.Text == "Start AsyncProcessor")
                sc.Start();

            if (btnStatus.Text == "Stop AsyncProcessor")
                sc.Stop();
        }
    }
    


    public class myProcess
    {
        public int process_id;
        public string process_name;
    }
}
