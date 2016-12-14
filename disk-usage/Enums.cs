

using System;
using System.ComponentModel;

namespace disk_usage
{
    public enum OsName
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
        Os,
        Unknown
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
        [Description("Sort Alphabetically (A-Z)")]
        Alphabetical,
        [Description("Sort Alphabetically (Z-A)")]
        AlphabeticalDescending,
        [Description("Sort by Free Space (Low to High)")]
        FreeSpace,
        [Description("Sort by Free Space (High To Low)")]
        FreeSpaceDescending,
        [Description("Sort by % Fill (Low to High)")]
        FillPercentage,
        [Description("Sort by % Fill (High To Low)")]
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
            System.Diagnostics.Contracts.Contract.Requires(value != null);
            var fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes.Length > 0)
            {
                return (!string.IsNullOrWhiteSpace(attributes[0].Description)) ? attributes[0].Description : value.ToString();
            }
            return value.ToString();

        }
    }


}