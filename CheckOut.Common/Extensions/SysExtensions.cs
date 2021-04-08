using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckOut.Common
{
    public static class SysExtensions
    {
        private static Random random = new Random();

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
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}