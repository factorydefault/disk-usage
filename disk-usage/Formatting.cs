using ByteSizeLib;

namespace disk_usage
{
    public static class Formatting
    {

        public static string Ellipsis(this string str, int length = 10) => (str.Length > length) ? $"{str.Substring(0, length)}..." : str;

        public static string PropertiesLabel(this ByteSize size) => (size.GigaBytes >= 1.0) ? ExplorerLabel(size) : "";

        public static string ExplorerLabel(this ByteSize size)
        {
            if (size.TeraBytes >= 1.0)
            {
                return size.ToString("#.##"); //2dp
            }
            if (size.GigaBytes >= 1.0)
            {
                return size.ToString("#.#"); //1dp
            }
            return size.ToString("#"); //0dp
        }

    }
}
