using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace disk_usage_ui
{
    public partial class AddPathDialog : Form
    {
        public string InitialPath { get; set; } = "C:\\";

        public disk_usage.PathRecord NewComputer { get; private set; }

        public AddPathDialog()
        {
            InitializeComponent();
            NewComputer = new disk_usage.PathRecord();
            updateUserInterface();
            DialogResult = DialogResult.Cancel;
            NewComputer.DiskInfoUpdated += NewComputer_DiskInfoUpdated;
        }

        private void NewComputer_DiskInfoUpdated(object sender, EventArgs e)
        {
            exampleTile.UpdateUserInterface(NewComputer);
        }

        void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dialogTextChanged(object sender, EventArgs e)
        {
            updateUserInterface();    
        }


        static bool PathHasValidForm(string path)
        {
            try
            {
                if (disk_usage.PathRecord.LocalRegex.IsMatch(path)) 
                {
                    return true;
                }
                if (disk_usage.PathRecord.UNCNamedRegex.IsMatch(path))
                {
                    return true;
                }

                //specified by ip?

            }
            catch (Exception ex)
            {
                Console.WriteLine($"PathExists error: {ex.Message}");
            }
            return false;


        }


        void updateUserInterface()
        {
            NewComputer.FriendlyName = labelTextBox.Text;
            NewComputer.Path = pathTextBox.Text;
            

            if (PathHasValidForm(pathTextBox.Text))
            {
                //exampleTile.UpdateUserInterface(); //NewComputer);
                NewComputer.RequestDiskInfo();
                acceptButton.Enabled = true;
            }
            else
            {
                exampleTile.SetAsNotFound(NewComputer.FriendlyName);
                acceptButton.Enabled = false;
            }

        }

        void acceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        void load(object sender, EventArgs e)
        {
            pathTextBox.Text = InitialPath;
        }
    }
}
