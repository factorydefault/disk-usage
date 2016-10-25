

using System;
using System.ComponentModel;
using System.Reflection;

namespace disk_usage
{
    public enum OSVersion
    {
        Windows10,
        Windows8,
        Windows7,
        Other
    }

    public enum PathLocation
    {
        Local,
        Remote,
        OS,
    }

    public enum ProgressBarState
    {
        //1 = normal (green); 2 = error (red); 3 = warning (yellow).
        Normal = 1,
        Error = 2,
        Warning = 3
    }


    public enum SortingOption
    {
        [Description("Sort Alphabetical by Label (A-Z)")]
        Alphabetical,
        [Description("Sort Alphabetical by Label (Z-A)")]
        AlphabeticalDescending,
        [Description("Sort by Free Space (Low to High)")]
        FreeSpace,
        [Description("Sort by Free Space (High To Low)")]
        FreeSpaceDescending,
        [Description("Sort by Percentage Fill (Low to High)")]
        FillPercentage,
        [Description("Sort by Percentage Fill (High To Low)")]
        FillPercentageDescending,
        [Description("Sort by Volume Size (Low to High)")]
        Capacity,
        [Description("Sort by Volume Size (High To Low)")]
        CapacityDescending,
        [Description("Sort by Used Space (Low to High)")]
        UsedSpace,
        [Description("Sort by Used Space (High to Low)")]
        UsedSpaceDescending,
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