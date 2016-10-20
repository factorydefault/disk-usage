using System;
using System.Diagnostics;
using System.Windows.Forms;
using Links = disk_usage.Links;

namespace disk_usage_ui.Forms
{
    public partial class AboutForm : Form
    {
               
        public AboutForm()
        {
            InitializeComponent();
            rtbInfo.Text = LinkString("Project Page", Links.URL_PROJECT); 
            rtbInfo.Text += LinkString("Issues", Links.URL_ISSUES);

            rtbInfo.Text += "\nExternal Libraries:\n\n";
            rtbInfo.Text += LinkString(Links.JSON_NAME, Links.URL_JSON);
            rtbInfo.Text += LinkString(Links.COSTURA_NAME,Links.URL_COSTURA);

            licenseLink.Text = Links.LICENSE_NAME;
        }

        string LinkString(string name, string url) => string.Format($"{name}: {url}\n");


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
            Process.Start(Links.URL_ALEX);
        }

        void licenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Links.URL_LICENSE);
        }
    }
}
