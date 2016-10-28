using System;
using System.Linq;
using System.Windows.Forms;
using disk_usage;

namespace disk_usage_ui.Forms
{
    public partial class PropertiesForm : Form
    {
        public PropertiesForm()
        {
            _record = new disk_usage.PathRecord();
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void driveLabelTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void capacityBytesLabel_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void PropertiesForm_Load(object sender, EventArgs e)
        {
            EnableTab(tabPage2, false);
            ((Control)tabPage2).Enabled = false;
            
        }

        PathRecord _record;

        public void ProvideData(PathRecord record)
        {
            _record = record;
            UpdateUI();
        }

        void UpdateUI()
        {
            driveLabelTextBox.Text = _record.FriendlyName;
            locationLabel.Text = _record.Path;

            diskTypeLabel.Text = DiskTypeString;

            updatePieChart(_record.FillLevel);
        }
                
        string DiskTypeString
        {
            get
            {
                switch (_record.Location())
                {
                    case PathLocation.Local:
                        return "Local / Mapped Drive";
                    case PathLocation.Remote:
                        return "Network Drive";
                    case PathLocation.OS:
                        return "OS Drive";
                    default:
                        return "Unknown";
                }
            }
        }

        void updatePieChart(int usedPercentage)
        {
            try
            {
                pieChart.Series.FirstOrDefault().Points[0].SetValueY(usedPercentage);
                pieChart.Series.FirstOrDefault().Points[1].SetValueY(100 - usedPercentage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public string DiskLabel
        {
            get
            {
                return driveLabelTextBox.Text.Trim();
            }
            set
            {
                driveLabelTextBox.Text = value;
            }
        }


        public static void EnableTab(TabPage page, bool enable)
        {
            foreach (Control ctl in page.Controls) ctl.Enabled = enable;
        }

        void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        void explorerButton_Click(object sender, EventArgs e)
        {
            try
            {
                Windows.ShowFileProperties(_record.Path);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
