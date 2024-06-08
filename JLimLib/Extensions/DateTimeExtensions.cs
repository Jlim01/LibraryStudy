using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JLimLib.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDateOfMonth(this DateTime date) // 현재 데이터 타임 객체의 첫날(1일)의 데이터 타입 리턴
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDateOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month)); //DateTime.DaysInMonth는 년, 월을 인자로 주면 몇개의 '일'(그 달의 총 날짜 수)이 있는지 리턴
        }
    }
}
