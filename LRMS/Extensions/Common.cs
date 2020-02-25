using System.IO;

namespace LRMS.Extensions
{
    public static class Common
    {
        public static string ToLogDate(this FileInfo fileInfo)
        {
            if (fileInfo == null || fileInfo.Name == null) return null;
            return fileInfo.Name.Split(".")[0];
        }
        public static string ToLogType(this string inputStr)
        {
            if (string.IsNullOrWhiteSpace(inputStr)) return string.Empty;
            return inputStr
                .Replace(":", "")
                .ToUpper();
        }

    }
}
