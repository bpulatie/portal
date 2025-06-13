using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RDMConfig
{
    public partial class MainForm : Form
    {
        private AuthenticateService.authenticate_ServicesSoapClient oWebService = new AuthenticateService.authenticate_ServicesSoapClient();
        private dynamic oData;
        private string site_guid = string.Empty;
        private string site_id = string.Empty;
        private bool dirty_flag = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            txtHost.Text = config.AppSettings.Settings["hostURL"].Value;
            txtShare.Text = config.AppSettings.Settings["folderToMonitor"].Value;
            txtBackup.Text = config.AppSettings.Settings["folderForBackup"].Value;
            site_guid = config.AppSettings.Settings["siteGUID"].Value;
            site_id = config.AppSettings.Settings["siteID"].Value;
            selectedSite.Text = config.AppSettings.Settings["siteName"].Value;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                oWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress(txtHost.Text + @"/Services/authenticate_Services.asmx");
                string jsonString = oWebService.SupportSignIn(txtUser.Text, txtPassword.Text);
                dynamic d = JObject.Parse(jsonString);
                if (d.result != true)
                {
                    throw new Exception("Unable to Verify Credentials");
                }

                string value = Convert.ToString(d.data);
                oData = JObject.Parse(value);

                for (var x = 0; x < oData.sites.Count; x++)
                {
                    cbSites.Items.Add("Client: " + oData.sites[x].client + " - Site: " + oData.sites[x].site_code + " " + oData.sites[x].name);
                }

                if (site_id.Length > 0)
                {
                    btnRelink.Enabled = true;
                }
                
                btnLink.Enabled = true;
                btnSave.Enabled = true;
                txtHost.ReadOnly = true;
                cbSites.Enabled = true;
                btnSelectBackup.Enabled = true;
                btnSelectShare.Enabled = true;

                MessageBox.Show("Credentials Verified Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtShare.Text = fbd.SelectedPath;
                    dirty_flag = true;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtBackup.Text = fbd.SelectedPath;
                    dirty_flag = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            if (cbSites.SelectedIndex < 0)
            {
                MessageBox.Show("A Site must be selected before you can link");
                return;
            }

            try
            {
                int x = cbSites.SelectedIndex;
                //MessageBox.Show("Client: " + oData.sites[x].client + " - Site: " + oData.sites[x].site_code + " " + oData.sites[x].name + " || SiteID = " + oData.sites[x].site_id);
                oWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress(txtHost.Text + @"/Services/authenticate_Services.asmx");
                string jsonString = oWebService.LinkSite(txtUser.Text, txtPassword.Text, Convert.ToString(oData.sites[x].site_id));
                dynamic d = JObject.Parse(jsonString);
                if (d.result != true)
                {
                    throw new Exception("Unable to Link Site");
                }

                selectedSite.Text = Convert.ToString(oData.sites[x].site_code) + " - " + Convert.ToString(oData.sites[x].name);
                site_guid = Convert.ToString(d.data);
                site_id = Convert.ToString(oData.sites[x].site_id);
                dirty_flag = true;

                MessageBox.Show("Site Registration obtained - don't forget to Save Configuration");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["folderToMonitor"].Value = txtShare.Text;
            config.AppSettings.Settings["folderForBackup"].Value = txtBackup.Text;
            config.AppSettings.Settings["hostURL"].Value = txtHost.Text;
            config.AppSettings.Settings["siteGUID"].Value = site_guid;
            config.AppSettings.Settings["siteID"].Value = site_id;
            config.AppSettings.Settings["siteName"].Value = selectedSite.Text;
            config.Save(ConfigurationSaveMode.Minimal);
            dirty_flag = false;

            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty_flag == true)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes, Do you really want to exit?", "Confirm Cancel",  MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                oWebService.Endpoint.Address = new System.ServiceModel.EndpointAddress(txtHost.Text + @"/Services/authenticate_Services.asmx");
                string jsonString = oWebService.LinkSite(txtUser.Text, txtPassword.Text, site_id);
                dynamic d = JObject.Parse(jsonString);
                if (d.result != true)
                {
                    throw new Exception("Unable to Relink Site");
                }

                site_guid = Convert.ToString(d.data);
                dirty_flag = true;

                MessageBox.Show("Site Registration obtained - don't forget to Save Configuration");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
