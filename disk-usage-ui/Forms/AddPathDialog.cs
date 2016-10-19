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

        public disk_usage.Computer NewComputer { get; private set; }

        public AddPathDialog()
        {
            InitializeComponent();
            NewComputer = new disk_usage.Computer();
            updateUserInterface();
            DialogResult = DialogResult.Cancel;
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

        void updateUserInterface()
        {
            NewComputer.FriendlyName = labelTextBox.Text;
            NewComputer.Path = pathTextBox.Text;
            

            if (System.IO.Directory.Exists(pathTextBox.Text))
            {
                exampleTile.VariablesFromComputer(NewComputer);
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
