

using System;
using System.ComponentModel;
using System.Reflection;

namespace disk_usage
{

    public enum SortingOption
    {
        [Description("Sort by Name (A-Z)")]
        Alphabetical,
        [Description("Sort by Name (Z-A)")]
        AlphabeticalDescending,
        [Description("Sort by Free Space")]
        FreeSpace,
        [Description("Sort by Free Space (Descending)")]
        FreeSpaceDescending,
        [Description("Sort by Percentage Fill")]
        FillPercentage,
        [Description("Sort by Percentage Fill (Descending)")]
        FillPercentageDescending,
        [Description("Sort by Volume Size")]
        Capacity,
        [Description("Sort by Volume Size (Descending)")]
        CapacityDescending
    }

    public static class Enums
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            return (attributes != null && attributes.Length > 0) ? attributes[0].Description : value.ToString();

        }
    }


}