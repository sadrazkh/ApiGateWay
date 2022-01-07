using DNTPersianUtils.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class DateTimeExtention
    {
        /// <summary>
        /// تبدیل تاریخ و زمان رشته‌ای شمسی به میلادی
        /// با قالب‌های پشتیبانی شده‌ی ۹۰/۸/۱۴ , 1395/11/3 17:30 , ۱۳۹۰/۸/۱۴ , ۹۰-۸-۱۴ , ۱۳۹۰-۸-۱۴
        /// در اینجا اگر رشته‌ی مدنظر قابل تبدیل نباشد، مقدار نال را دریافت خواهید کرد
        /// </summary>
        /// <param name="persianDateTime">تاریخ و زمان شمسی</param>
        /// <returns>تاریخ و زمان میلادی</returns>
        public static DateTimeOffset? ToGregorianDateTimeOffset(this string persianDateTime)
        {
            var dateTime = persianDateTime.ToGregorianDateTime();
            if (dateTime == null)
            {
                return null;
            }

            return new DateTimeOffset(dateTime.Value, DateTimeUtils.IranStandardTime.BaseUtcOffset);
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// با قالبی مانند 1395/10/21 10:20
        /// </summary>
        /// <param name="dt">تاریخ و زمان</param>
        /// <param name="dateTimeOffsetPart">کدام جزء این وهله مورد استفاده قرار گیرد؟</param>
        /// <returns>تاریخ شمسی</returns>
        public static string ToShortPersianDateTimeString(this DateTimeOffset dt, DateTimeOffsetPart dateTimeOffsetPart = DateTimeOffsetPart.IranLocalDateTime)
        {
            return ToShortPersianDateTimeString(dt.GetDateTimeOffsetPart(dateTimeOffsetPart));
        }

        public static DateTime ToIranStandardTime(this DateTime dateTime)
        {
            //var iranStandardTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, "Iran Standard Time");
            var iranStandardTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, "Asia/Tehran");
            return iranStandardTime;
        }


        //public static DateTime? ToIranStandardTime(this DateTime? dateTime)
        //{
        //    if (dateTime == null)
        //    {
        //        return null;
        //    }
        //    var iranStandardTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime.Value, "Iran Standard Time");
        //    //var iranStandardTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime.Value, "Asia/Tehran");
        //    return iranStandardTime;
        //}
    }
}
