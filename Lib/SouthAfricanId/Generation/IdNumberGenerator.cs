
using System;
using System.Linq;
using SouthAfricanId.Models;
using SouthAfricanId.Validation;

namespace SouthAfricanId.Generation
{

        /// <summary>
        /// Generates valid South African ID numbers with flexible parameters.
        /// </summary>
        public class IdNumberGenerator
        {
            private readonly Validator _validator = new Validator();
            private static readonly Random _rand = new Random();

            /// <summary>
            /// Generates a valid South African ID number using the provided parameters.
            /// </summary>
            /// <param name="birthday">Date of birth (required).</param>
            /// <param name="gender">Gender (required).</param>
            /// <param name="isCitizen">True for citizen, false for permanent resident.</param>
            /// <param name="sequence">Optional sequence (0000-9999). If null, random based on gender.</param>
            /// <param name="historicalDigit">Optional historical digit (0-9). If null, defaults to 8.</param>
            /// <returns>A valid South African ID number as a string.</returns>
            public string GenerateIdNumber(DateTime birthday, Gender gender, bool isCitizen = true, int? sequence = null, int? historicalDigit = null)
            {
                if (birthday.Year < 1900 || birthday > DateTime.Now)
                    throw new ArgumentException("Birthday must be between 1900 and today.");
                if (!Enum.IsDefined(typeof(Gender), gender))
                    throw new ArgumentException("Invalid gender value.");

                const int maxAttempts = 20;
                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    string dateString = birthday.ToString("yyMMdd");
                    int seq = sequence ?? (gender == Gender.Female ? _rand.Next(0, 5000) : _rand.Next(5000, 10000));
                    string ssss = seq.ToString("D4");
                    string citizenString = isCitizen ? "0" : "1";
                    int hist = historicalDigit ?? 8;
                    string aDigit = hist.ToString();
                    string first12 = dateString + ssss + citizenString + aDigit;
                    string controlDigit = GetControlDigit(first12);
                    string idNumber = first12 + controlDigit;
                    if (_validator.Validate(idNumber))
                        return idNumber;
                }
                throw new InvalidOperationException("Could not generate a valid ID number after multiple attempts.");
            }

            /// <summary>
            /// Generates a valid South African ID number using random/default values.
            /// </summary>
            /// <returns>A valid South African ID number as a string.</returns>
            public string GenerateIdNumber()
            {
                DateTime birthday = new DateTime(_rand.Next(1950, DateTime.Now.Year), _rand.Next(1, 13), _rand.Next(1, 28));
                Gender gender = (Gender)_rand.Next(0, 2);
                bool isCitizen = _rand.Next(0, 2) == 0;
                return GenerateIdNumber(birthday, gender, isCitizen);
            }

            /// <summary>
            /// Calculates the control digit for a South African ID number using the Luhn algorithm.
            /// </summary>
            /// <param name="first12Digits">The first 12 digits of the ID number.</param>
            /// <returns>The control digit as a string.</returns>
            public static string GetControlDigit(string first12Digits)
            {
                if (first12Digits.Length != 12 || !first12Digits.All(char.IsDigit))
                    throw new ArgumentException("Input must be 12 digits.");
                int[] digits = first12Digits.Select(c => c - '0').ToArray();
                int total = 0;
                for (int i = 0; i < 12; i++)
                {
                    int d = digits[i];
                    if (i % 2 == 1)
                    {
                        d *= 2;
                        if (d > 9) d -= 9;
                    }
                    total += d;
                }
                int checkDigit = (10 - (total % 10)) % 10;
                return checkDigit.ToString();
            }
        }


}


