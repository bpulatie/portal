using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestPayComDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startDate = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
            
            string[] Args = new string[] { @"C:\SPALog\",
                                            @"Data Source=172.86.160.27;Initial Catalog=SPA_Portal;Persist Security Info=True;User ID=spa_portal;Password=Kirstie92",
                                            startDate.ToString() };

            try
            {
                PAYCOMEmployeeImport.Main.OnExecute(Args);
                MessageBox.Show("Job has Completed - check the DB");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] Args = new string[] { @"C:\SPALog\",
                                            @"Data Source=172.86.160.27;Initial Catalog=SPA_Portal;Persist Security Info=True;User ID=spa_portal;Password=Kirstie92",
                                            null};

            try
            {
                PAYCOMEmployeeImport.Main.OnExecute(Args);
                MessageBox.Show("Job has Completed - check the DB");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
