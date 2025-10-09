using System;
using System.Linq;

namespace SouthAfricanId.Validation
{


    /// <summary>
    /// Provides validation logic for South African ID numbers.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Validates a South African ID number for correct format, date, and control digit.
        /// </summary>
        /// <param name="idNumber">The ID number to validate.</param>
        /// <returns>True if the ID number is valid; otherwise, false.</returns>
        public virtual bool ValidateIdNumber(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) || !idNumber.All(char.IsDigit))
                return false;

            int[] digits = idNumber.Select(c => c - '0').ToArray();
            int checksum = 0;
            int parity = digits.Length % 2;

            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i];
                if (i % 2 == parity)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }
                checksum += digit;
            }
            return checksum % 10 == 0;
        }


        public virtual bool IsValidIdNumberDate(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) || !idNumber.All(char.IsDigit))
                return false;
            if (idNumber.Length < 6)
                return false;
            string datePart = idNumber.Substring(0, 6);
            if (DateTime.TryParseExact(datePart, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date <= DateTime.Now;
            }
            return false;
        }

        public virtual bool Validate(string idNumber)
        {
            return ValidateIdNumber(idNumber) && IsValidIdNumberDate(idNumber);
        }
    }

}