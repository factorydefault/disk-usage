using System;
using System.Diagnostics;
using System.Windows.Forms;
using disk_usage_ui.Properties;
using Links = disk_usage.Links;

namespace disk_usage_ui.Forms
{
    public partial class AboutForm : Form
    {
               
        public AboutForm()
        {
            InitializeComponent();
            rtbInfo.Text = LinkString("Project Page", Links.UrlProject); 
            rtbInfo.Text += LinkString("Issues", Links.UrlIssues);

            rtbInfo.Text += Resources.AboutForm_Header;
            rtbInfo.Text += LinkString(Links.JsonName, Links.UrlJson);
            rtbInfo.Text += LinkString(Links.CosturaName,Links.UrlCostura);
            rtbInfo.Text += LinkString(Links.BytesizeName, Links.UrlBytesize);

            licenseLink.Text = Links.LicenseName;
        }

        static string LinkString(string name, string url) => string.Format($"{name}: {url}\n");


        void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        void rtbInfo_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        void gitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Links.UrlGitProfile);
        }

        void licenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Links.UrlLicense);
        }
    }
}
