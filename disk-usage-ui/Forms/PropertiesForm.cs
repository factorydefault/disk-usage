using System;
using System.Linq;
using System.Windows.Forms;
using disk_usage;
using ByteSizeLib;

namespace disk_usage_ui.Forms
{
    public partial class PropertiesForm : Form
    {
        public PropertiesForm()
        {
            _record = new PathRecord();
            InitializeComponent();

        }


        void driveLabelTextBox_TextChanged(object sender, EventArgs e)
        {
            setFormTitle();
        }

        void PropertiesForm_Load(object sender, EventArgs e)
        {
            shortcutButton.Enabled = true;
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

            usedBytesLabel.Text = formattedBytes(_record.UsedSpace);
            usedSummary.Text = _record.UsedSpace.PropertiesLabel();

            freeBytesLabel.Text = formattedBytes(_record.FreeSpace);
            freeSummary.Text = _record.FreeSpace.PropertiesLabel();

            capacityBytesLabel.Text = formattedBytes(_record.Capacity); 
            capacitySummary.Text = _record.Capacity.PropertiesLabel();

            notificationsCheckBox.Checked = _record.Notifications;

            setFormTitle();

        }

        string formattedBytes(ByteSize size) => $"{size.Bytes:#,0} bytes";

        void setFormTitle()
        {
            if (string.IsNullOrWhiteSpace(driveLabelTextBox.Text))
            {
                Text = $"{_record.Path.Trim().Ellipsis(22)} Properties";
            }
            else
            {
                Text = $"{driveLabelTextBox.Text.Trim().Ellipsis(22)} Properties";
            }
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

        public bool ShouldUseNotifications => notificationsCheckBox.Checked;       

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
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        void shortcutButton_Click(object sender, EventArgs e)
        {
            _record.FriendlyName = DiskLabel;
            Shortcuts.TryCreate(_record);
        }

        void explorePathButton_Click(object sender, EventArgs e)
        {
            Tools.OpenDirectory(_record);
        }


    }
}
