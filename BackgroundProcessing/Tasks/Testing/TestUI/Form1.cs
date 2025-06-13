using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TestUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) 
            {
                Assembly a = Assembly.LoadFile(openFileDialog1.FileName);
                var onlyFileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                Type t = a.GetType(onlyFileName + ".Main");
                MethodInfo m = t.GetMethod("OnExecute");

                string[] Args = new string[] { "", textBox2.Text };
                var res = m.Invoke(null, new object[] { Args });
            }
        }
    }
}
