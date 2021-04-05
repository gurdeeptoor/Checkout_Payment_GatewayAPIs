using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOut.Common
{
    public static class SysExtensions
    {
        public static string ToMaskedCardNumber(this string CardNumber)
        {
            if (!string.IsNullOrEmpty(CardNumber))
                return $"XXXXXXXXXXXX{CardNumber.Substring(CardNumber.Length - 4, 4)}";
            return CardNumber;
        }

        public static int To2DigitYear(this int YearVal)
        {
            return YearVal % 100;
        }
    }
}