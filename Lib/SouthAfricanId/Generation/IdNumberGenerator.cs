
using System;
using System.Linq;
using SouthAfricanId.Models;
using SouthAfricanId.Validation;

namespace SouthAfricanId.Generation
{
    public class IdNumberGenerator
    /// <summary>
    /// Provides methods to generate valid South African ID numbers based on input parameters or randomly.
    /// </summary>
    {
        private readonly Validator _validator = new Validator();
        private static readonly Random _rand = new Random();

        public string GenerateIdNumber(DateTime birthday, Gender gender, bool isCitizen = true)
        /// <summary>
        /// Generates a valid South African ID number using the provided birthday, gender, and citizenship status.
        /// </summary>
        /// <param name="birthday">The date of birth to encode in the ID number. Must be between 1900 and today.</param>
        /// <param name="gender">The gender to encode (Male or Female).</param>
        /// <param name="isCitizen">True if the person is a South African citizen; otherwise, false.</param>
        /// <returns>A valid South African ID number as a string.</returns>
        /// <exception cref="ArgumentException">Thrown if birthday or gender is invalid.</exception>
        {
            if (birthday.Year < 1900 || birthday > DateTime.Now)
                throw new ArgumentException("Birthday must be between 1900 and today.");
            if (!Enum.IsDefined(typeof(Gender), gender))
                throw new ArgumentException("Invalid gender value.");

            const int maxAttempts = 10;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                string dateString = birthday.ToString("yyMMdd");
                string genderString = gender == Gender.Male ? "5000" : "0000";
                string citizenString = isCitizen ? "0" : "1";
                string sequence = _rand.Next(0, 10).ToString(); // Randomize sequence
                string partialId = dateString + genderString + citizenString + sequence;
                string controlDigit = GetControlDigit(partialId);
                string idNumber = partialId + controlDigit;
                if (_validator.Validate(idNumber))
                    return idNumber;
            }
            throw new InvalidOperationException("Could not generate a valid ID number after multiple attempts.");
        }

        public string GenerateIdNumber()
        /// <summary>
        /// Generates a valid South African ID number using random/default values for birthday, gender, and citizenship.
        /// </summary>
        /// <returns>A valid South African ID number as a string.</returns>
        {
            const int maxAttempts = 5;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                DateTime birthday = new DateTime(_rand.Next(1950, DateTime.Now.Year), _rand.Next(1, 13), _rand.Next(1, 28));
                Gender gender = (Gender)_rand.Next(0, 2);
                bool isCitizen = _rand.Next(0, 2) == 0;
                string id = GenerateIdNumber(birthday, gender, isCitizen);
                if (_validator.Validate(id))
                    return id;
            }
            throw new InvalidOperationException("Could not generate a valid ID number after multiple attempts.");
        }

        private static string GetControlDigit(string idWithoutControl)
        /// <summary>
        /// Calculates the control digit for a South African ID number using the Luhn algorithm.
        /// </summary>
        /// <param name="idWithoutControl">The ID number string without the control digit.</param>
        /// <returns>The control digit as a string.</returns>
        {
            int[] digits = idWithoutControl.Select(c => c - '0').ToArray();
            int checksum = 0;
            int parity = digits.Length % 2;
            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i];
                if (i % 2 == parity)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }
                checksum += digit;
            }
            int control = (10 - (checksum % 10)) % 10;
            return control.ToString();
        }


    }


}


