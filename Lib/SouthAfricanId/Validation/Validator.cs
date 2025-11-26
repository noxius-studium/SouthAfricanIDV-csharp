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

        /// <summary>
        /// Checks if the ID number is 13 digits and numeric.
        /// </summary>
        public bool IsFormatValid(string idNumber)
        {
            return !string.IsNullOrEmpty(idNumber) && idNumber.Length == 13 && idNumber.All(char.IsDigit);
        }

        /// <summary>
        /// Validates the Luhn checksum for the ID number.
        /// </summary>
        public bool IsLuhnValid(string idNumber)
        {
            if (!IsFormatValid(idNumber))
                return false;
            int[] digits = idNumber.Select(c => c - '0').ToArray();
            int total = 0;
            for (int i = 0; i < 12; i++)
            {
                int d = digits[i];
                if (i % 2 == 1) // every second digit from the left
                {
                    d *= 2;
                    if (d > 9) d -= 9;
                }
                total += d;
            }
            int checkDigit = (10 - (total % 10)) % 10;
            return checkDigit == digits[12];
        }



        /// <summary>
        /// Validates the birthdate encoded in the ID number (YYMMDD, not in the future).
        /// </summary>
        public bool IsBirthdateValid(string idNumber)
        {
            if (!IsFormatValid(idNumber))
                return false;
            string datePart = idNumber.Substring(0, 6);
            int year = int.Parse(datePart.Substring(0, 2));
            int month = int.Parse(datePart.Substring(2, 2));
            int day = int.Parse(datePart.Substring(4, 2));
            int currentYear2 = DateTime.Now.Year % 100;
            int century = (year > currentYear2) ? 1900 : 2000;
            year += century;
            try
            {
                var dob = new DateTime(year, month, day);
                return dob <= DateTime.Now;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the ID number for format, date, and Luhn checksum.
        /// </summary>
        public virtual bool Validate(string idNumber)
        {
            return IsFormatValid(idNumber) && IsBirthdateValid(idNumber) && IsLuhnValid(idNumber);
        }
    }

}