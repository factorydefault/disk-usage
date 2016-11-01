using System;
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

        void NewComputer_DiskInfoUpdated(object sender, EventArgs e)
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
                    Console.WriteLine("Valid Local");
                    return true;
                }
                if (disk_usage.PathRecord.UNCNamedRegex.IsMatch(path))
                {
                    Console.WriteLine("Valid UNC");
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
            var inputText = pathTextBox.Text;

            NewComputer.Path = inputText;

            var withBackslash = $"{inputText}\\";

            if (PathHasValidForm(inputText) ) 
            {
                System.Diagnostics.Debug.Print("Path valid.");
                
                NewComputer.RequestDiskInfo();
                acceptButton.Enabled = true;
            }
            else if(PathHasValidForm(withBackslash)) //lenient to missing backslash
            {
                System.Diagnostics.Debug.Print("Missing backslash added.");
                NewComputer.Path = withBackslash;
                NewComputer.RequestDiskInfo();
                acceptButton.Enabled = true;
            }
            else
            {
                exampleTile.SetAsNotFound();
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
            exampleTile.Interactive = false;
        }

        void pathTextBox_DoubleClick(object sender, EventArgs e)
        {
            var dr = folderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if(!folderBrowserDialog.SelectedPath.EndsWith("\\", StringComparison.Ordinal))
                {
                    pathTextBox.Text = $"{folderBrowserDialog.SelectedPath}\\";
                }
                else
                {
                    pathTextBox.Text = folderBrowserDialog.SelectedPath;
                }
                
            }


        }
    }
}
