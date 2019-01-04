using System;
using System.Globalization;
using System.IO;

namespace TeamHGSTalentContest.Services
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string str)
        {
            var ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(str);
        }

        public static string GetUniqueName(this string str)
        {
            str = Path.GetFileName(str);
            return Path.GetFileNameWithoutExtension(str)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(str);
        }
    }
}
