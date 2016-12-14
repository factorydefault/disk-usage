using ByteSizeLib;

namespace disk_usage
{
    public static class Formatting
    {
        static double Epsilon => 1e-9;

        public static string Ellipsis(this string str, int length = 10)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";
            return (str.Length > System.Math.Abs(length)) ? $"{str.Substring(0, System.Math.Abs(length))}..." : str;
        }

        public static string PropertiesLabel(this ByteSize size) => (size.GigaBytes >= 1.0) ? ExplorerLabel(size) : "";

        public static string ExplorerLabel(this ByteSize size)
        {
            if (size.Bytes < 1) return string.Empty;

            return size.TeraBytes >= 1.0 ? size.ToString("#.##") : size.ToString(size.GigaBytes >= 1.0 ? "#.#" : "#");
        }

        public static double Clamp(this double value, double min = 0, double max = 100)
        {
            //edge case
            if (System.Math.Abs(min - max) < Epsilon) return min;

            if (min > max) //flip
            {
                var temp = max;
                max = min;
                min = temp;
            }

            if (value > max) return max;
            return value < min ? min : value;
        }
    }
}
