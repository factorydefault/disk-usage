using disk_usage;
using System;
using System.Windows.Forms;

namespace disk_usage_ui
{
    public static class Shortcuts
    {
        public static bool TryCreate(PathRecord record, bool notify = true)
        {
            try
            {
                //create shortcut

                string shortcutLocation = $"{Windows.Desktop}\\{record.ShortcutName} - Shortcut.lnk";

                Windows.CreateShortcut(record.Path, shortcutLocation);

                if (notify) MessageBox.Show($"A shortcut to \"{record.Path}\" was placed on your Desktop.");

                return true;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("The directory could not be found, a shortcut cannot be created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public static bool HasExisting(PathRecord record)
        {
            try
            {
                string shortcutLocation = $"{Windows.Desktop}\\{record.ShortcutName} - Shortcut.lnk";

                return System.IO.File.Exists(shortcutLocation);
            }
            catch (System.IO.FileNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

                    


    }
}
