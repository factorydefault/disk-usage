using System;
using System.Linq;
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

        void dialogTextChanged(object sender, EventArgs e)
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

        string sanitisePath(string input)
        {
            var result = System.IO.Path.GetInvalidPathChars().Aggregate(input, (current, c) => current.Replace(c.ToString(), string.Empty)).Trim();
            return result.Replace('/', '\\');
        }


        void updateUserInterface()
        {
            NewComputer.FriendlyName = labelTextBox.Text;

            var typedPath = sanitisePath(pathTextBox.Text);

            NewComputer.Path = typedPath;

            var typedPathWithBackslash = $"{typedPath}\\";

            if (PathHasValidForm(typedPath) ) 
            {
                System.Diagnostics.Debug.Print("Path valid.");
                
                NewComputer.RequestDiskInfo();
                acceptButton.Enabled = true;
            }
            else if(PathHasValidForm(typedPathWithBackslash)) //lenient to missing backslash
            {
                System.Diagnostics.Debug.Print("Missing backslash added.");
                NewComputer.Path = typedPathWithBackslash;
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

        void DragEnterEvent(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }


        }

        void ProcessDragDrop(string[] FileList)
        {
            try
            {
                if (FileList.Count() > 0)
                {
                    string path = FileList.FirstOrDefault();

                    Console.WriteLine(path);

                    if (disk_usage.PathRecord.LocalRegex.IsMatch(path))
                    {
                        pathTextBox.Text = path;
                        return;
                    }

                    System.IO.FileAttributes attr = System.IO.File.GetAttributes(path);

                    if (attr.HasFlag(System.IO.FileAttributes.Directory))
                    {
                        pathTextBox.Text = sanitisePath(path);
                    }
                    else
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(FileList.FirstOrDefault());
                        pathTextBox.Text = sanitisePath(fi.Directory.FullName);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        void DragDropEvent(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            ProcessDragDrop(FileList);
        }

        void notificationsCheck_CheckedChanged(object sender, EventArgs e)
        {
            NewComputer.Notifications = notificationsCheck.Checked;
        }
    }
}
