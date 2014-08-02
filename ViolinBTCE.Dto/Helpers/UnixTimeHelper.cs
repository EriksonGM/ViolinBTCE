using System;

namespace ViolinBtce.Dto.Helpers
{
    public static class UnixTimeHelper
    {
        static DateTime _unixEpoch;
        static UnixTimeHelper()
        {
            _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public static UInt32 Now { get { return GetFromDateTime(DateTime.UtcNow); } }

        public static UInt32 GetFromDateTime(DateTime d) { return (UInt32)(d - _unixEpoch).TotalSeconds; }

        public static DateTime ConvertToDateTime(UInt32 unixtime) { return _unixEpoch.AddSeconds(unixtime); }
    }
}