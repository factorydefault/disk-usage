using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace disk_usage_ui.Forms
{
    public partial class AddPathDialog : Form
    {
        public string InitialPath { get; set; } = "C:\\";

        public disk_usage.PathRecord NewComputer { get; }

        public AddPathDialog()
        {
            InitializeComponent();
            NewComputer = new disk_usage.PathRecord();
            notificationsCheck.Checked = NewComputer.Notifications;
            UpdateUserInterface();
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

        void DialogTextChanged(object sender, EventArgs e)
        {
            UpdateUserInterface();    
        }


        static bool PathHasValidForm(string path)
        {
            try
            {
                if (disk_usage.PathRecord.LocalRegex.IsMatch(path)) 
                {
                    Console.WriteLine(@"Valid Local");
                    return true;
                }
                if (disk_usage.PathRecord.UncNamedRegex.IsMatch(path))
                {
                    Console.WriteLine(@"Valid UNC");
                    return true;
                }

                //specified by ip?

            }
            catch (Exception ex)
            {
                Console.WriteLine($@"PathExists error: {ex.Message}");
            }
            return false;


        }

        static string SanitisePath(string input)
        {
            var result = Path.GetInvalidPathChars().Aggregate(input, (current, c) => current.Replace(c.ToString(), string.Empty)).Trim();
            return result.Replace('/', '\\');
        }


        void UpdateUserInterface()
        {
            NewComputer.FriendlyName = labelTextBox.Text;

            var typedPath = SanitisePath(pathTextBox.Text);

            NewComputer.Path = typedPath;

            var typedPathWithBackslash = $"{typedPath}\\";

            if (PathHasValidForm(typedPath) ) 
            {
                System.Diagnostics.Debug.Print("Path valid.");
                
                NewComputer.RequestDiskInfoAsync();
                acceptButton.Enabled = true;
            }
            else if(PathHasValidForm(typedPathWithBackslash)) //lenient to missing backslash
            {
                System.Diagnostics.Debug.Print("Missing backslash added.");
                NewComputer.Path = typedPathWithBackslash;
                NewComputer.RequestDiskInfoAsync();
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

        void OnLoad(object sender, EventArgs e)
        {
            pathTextBox.Text = InitialPath;
            exampleTile.Interactive = false;
        }

        void pathTextBox_DoubleClick(object sender, EventArgs e)
        {
            var dr = folderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                pathTextBox.Text = !folderBrowserDialog.SelectedPath.EndsWith("\\", StringComparison.Ordinal)
                    ? $"{folderBrowserDialog.SelectedPath}\\" 
                    : folderBrowserDialog.SelectedPath;
            }


        }

        void DragEnterEvent(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) 
                ? DragDropEffects.Copy 
                : DragDropEffects.None;
        }

        void ProcessDragDrop(IReadOnlyCollection<string> fileList)
        {
            try
            {
                if (fileList.Count < 1) return;

                string path = fileList.FirstOrDefault();

                if (path == null) return;

                Console.WriteLine(path);

                if (disk_usage.PathRecord.LocalRegex.IsMatch(path))
                {
                    pathTextBox.Text = path;
                    return;
                }
                    
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                {
                    pathTextBox.Text = SanitisePath(path);
                }
                else
                {
                    var fi = new FileInfo(fileList.First());
                    if (fi.Directory != null) pathTextBox.Text = SanitisePath(fi.Directory.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        void DragDropEvent(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            ProcessDragDrop(fileList);
        }

        void notificationsCheck_CheckedChanged(object sender, EventArgs e)
        {
            NewComputer.Notifications = notificationsCheck.Checked;
        }
    }
}
