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
            SetFormTitle();
        }

        void PropertiesForm_Load(object sender, EventArgs e)
        {
            shortcutButton.Enabled = true;
        }

        PathRecord _record;

        public void ProvideData(PathRecord record)
        {
            _record = record;
            UpdateUi();
        }

        void UpdateUi()
        {
            driveLabelTextBox.Text = _record.FriendlyName;
            locationLabel.Text = _record.Path;

            diskTypeLabel.Text = DiskTypeString;

            updatePieChart(_record.FillPercentageDbl);

            usedBytesLabel.Text = formattedBytes(_record.UsedSpace);
            usedSummary.Text = _record.UsedSpace.PropertiesLabel();

            freeBytesLabel.Text = formattedBytes(_record.FreeSpace);
            freeSummary.Text = _record.FreeSpace.PropertiesLabel();

            capacityBytesLabel.Text = formattedBytes(_record.Capacity); 
            capacitySummary.Text = _record.Capacity.PropertiesLabel();

            //check boxes
            notificationsCheckBox.Checked = _record.Notifications;
            highlightCheckBox.Checked = _record.Highlight;

            SetFormTitle();

        }

        string formattedBytes(ByteSize size) => $"{size.Bytes:#,0} bytes";

        void SetFormTitle()
        {
            Text = string.IsNullOrWhiteSpace(driveLabelTextBox.Text) 
                ? $"{_record.Path.Trim().Ellipsis(22)} Properties" 
                : $"{driveLabelTextBox.Text.Trim().Ellipsis(22)} Properties";
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
                    case PathLocation.Os:
                        return "OS Drive";
                    default:
                        return "Unknown";
                }
            }
        }

        void updatePieChart(double percentageUsed)
        {
            double percentageClamped = percentageUsed.Clamp();

            try
            {
                var firstOrDefault = pieChart.Series.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    firstOrDefault.Points[0].SetValueY(percentageClamped);
                    firstOrDefault.Points[1].SetValueY(100 - percentageClamped);
                }
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

        public bool ShouldHighlight => highlightCheckBox.Checked;

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
