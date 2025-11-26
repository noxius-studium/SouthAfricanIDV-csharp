using System;
using SouthAfricanId.Models;

namespace SouthAfricanId.Extraction
{
    /// <summary>
    /// Provides logic to extract age information from a South African ID number.
    /// </summary>
    public class AgeExtraction
    {
        /// <summary>
        /// Extracts age information from the given ID number.
        /// </summary>
        /// <param name="idNumber">The South African ID number.</param>
        /// <returns>An <see cref="AgeInformation"/> object if extraction is successful; otherwise, null.</returns>
        /// <summary>
        /// Extracts age information from a valid South African ID number.
        /// </summary>
        /// <param name="idNumber">The South African ID number (must be validated first).</param>
        /// <returns>AgeInformation if extraction is successful; otherwise, null.</returns>
        public virtual AgeInformation Extract(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) || idNumber.Length < 6)
                return null;
            try
            {
                int year = int.Parse(idNumber.Substring(0, 2));
                int month = int.Parse(idNumber.Substring(2, 2));
                int day = int.Parse(idNumber.Substring(4, 2));
                int currentYear2 = DateTime.Now.Year % 100;
                int century = (year > currentYear2) ? 1900 : 2000;
                year += century;
                var dob = new DateTime(year, month, day);
                if (dob > DateTime.Now)
                    return null;
                return new AgeInformation(dob);
            }
            catch
            {
                return null;
            }
        }
    }

}



