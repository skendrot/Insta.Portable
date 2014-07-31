using System;

namespace Insta.Portable.Extensions
{
    internal static class DateTimeExtensions
    {
        internal static double ToEpoch(this DateTime dateTime)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixTimestamp = Convert.ToInt64((dateTime.Date.AddDays(1) - date).TotalSeconds);

            return unixTimestamp;
        }

        internal static DateTime FromEpoch(this float epochTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(epochTime).ToLocalTime();
        }
    }
}
