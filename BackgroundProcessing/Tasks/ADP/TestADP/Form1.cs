using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestADP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string [] Args = new string[] { @"C:\SPALog\ADPEmployeeImport.txt",
                                            @"C:\Users\steve\Documents\Visual Studio 2010\Projects\AsyncProcessor\ADPEmployeeImport\Content\config\default.json",
                                            @"Data Source=RACECONTROLLER;Initial Catalog=PF_Integration;Persist Security Info=True;User ID=sa;Password=steve"};

            ADPEmployeeImport.Main.OnExecute(Args);

            MessageBox.Show("Job has Completed - check the DB");


        }
    }
}
