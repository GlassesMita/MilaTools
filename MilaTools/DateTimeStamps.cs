using System;
using System.Globalization;
namespace MilaTools
{
    /// <summary>
    /// Provides methods to get system time in various formats and convert to Unix/Linux timestamps.
    /// </summary>
    public class DateTimeStamps
    {
        public static string GetCurrentTime(string format = null)
        {
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(format))
                return now.ToString(CultureInfo.CurrentCulture);
            return now.ToString(format, CultureInfo.CurrentCulture);
        }

        public static string GetUtcTime(string format = null)
        {
            var utcNow = DateTime.UtcNow;
            if (string.IsNullOrEmpty(format))
                return utcNow.ToString(CultureInfo.CurrentCulture);
            return utcNow.ToString(format, CultureInfo.CurrentCulture);
        }

        public static string GetCurrentDate(string format = null)
        {
            var today = DateTime.Today;
            if (string.IsNullOrEmpty(format))
                return today.ToString(CultureInfo.CurrentCulture);
            return today.ToString(format, CultureInfo.CurrentCulture);
        }

        public static long ToUnixTimestamp(DateTime? dateTime = null)
        {
            var dt = dateTime ?? DateTime.Now;
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(dt.ToUniversalTime() - unixEpoch).TotalSeconds;
        }

        public static long ToUnixTimestampMilliseconds(DateTime? dateTime = null)
        {
            var dt = dateTime ?? DateTime.Now;
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(dt.ToUniversalTime() - unixEpoch).TotalMilliseconds;
        }

        /// <summary>
        /// Gets the time in a specified time zone, based on the current system time.
        /// </summary>
        /// <param name="destinationTimeZoneId">The time zone ID of the destination (e.g., "Pacific Standard Time").</param>
        /// <param name="format">Optional output format. If null, uses system default.</param>
        /// <returns>Time string in the destination time zone.</returns>
        public static string GetTimeInTimeZone(string destinationTimeZoneId, string format = null)
        {
            var now = DateTime.Now;
            var destZone = TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId);
            var destTime = TimeZoneInfo.ConvertTime(now, TimeZoneInfo.Local, destZone);
            if (string.IsNullOrEmpty(format))
                return destTime.ToString(CultureInfo.CurrentCulture);
            return destTime.ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Calculates the time offset (in hours) between the local time zone and the specified time zone.
        /// </summary>
        /// <param name="destinationTimeZoneId">The time zone ID of the destination.</param>
        /// <returns>Offset in hours (can be negative).</returns>
        public double GetTimeZoneOffsetHours(string destinationTimeZoneId)
        {
            var now = DateTimeOffset.Now;
            var destZone = TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId);
            var destOffset = destZone.GetUtcOffset(now);
            var localOffset = TimeZoneInfo.Local.GetUtcOffset(now);
            return (destOffset - localOffset).TotalHours;
        }
    }
}
