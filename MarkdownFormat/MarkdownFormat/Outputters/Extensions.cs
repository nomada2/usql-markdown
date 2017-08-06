namespace MarkdownFormat
{
    static class Extensions
    {
        public static void WriteFormat(this System.IO.StreamWriter s, string format, params object[] p)
        {
            var str = string.Format(format,  p);
            s.Write(str);
        }
    }
}