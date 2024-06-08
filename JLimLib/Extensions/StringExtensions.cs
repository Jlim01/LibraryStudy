using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLimLib.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string str) // 첫번째 인자는 this뒤에 클래스나 타입이 와야함.
        {
            long result;
            return long.TryParse(str, out result); // long이 바뀔 수 있는지 테스트 해주는 function. 성공시 out변수인 result에 저장. 그러나 이 메서드는 TryParse 성공여부를 true/false로 반환.
        }

        public static bool IsDataTime(this string str)
        {
            if(String.IsNullOrEmpty(str)) return false; // str이 null일 시 DateTime으로 바꿀 수 없음
            else
            {
                DateTime result;
                return DateTime.TryParse(str, out result);
            }
        }
    }
}
