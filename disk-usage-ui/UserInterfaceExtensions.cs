using System;
using System.Windows.Forms;
using disk_usage;

namespace disk_usage_ui
{
    public static class UserInterfaceExtensions
    {
        public static void AddEnumDescriptionItems(this ComboBox combo, Enum enumerable, int selectedIndex = 0, bool clearExistingItems = true)
        {
            try
            {
                if (clearExistingItems) combo.Items.Clear();

                foreach (Enum option in Enum.GetValues(enumerable.GetType()))
                {
                    combo.Items.Add(option.GetDescription());
                }

                combo.SelectedIndex = selectedIndex;
            }
            catch (Exception)
            {
                if (combo.Items.Count > 0) combo.SelectedIndex = 0;
            }
        }

        public static void AddEnumDescriptionItems(this ToolStripComboBox combo, Enum enumerable, int selectedIndex = 0, bool clearExistingItems = true)
        {
            combo.ComboBox.AddEnumDescriptionItems(enumerable, selectedIndex, clearExistingItems); //shortcut for ToolStripComboBox
        }

    }



    public class NoFocusCueButton : Button
    {
        public NoFocusCueButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override bool ShowFocusCues => false;
    }

}
