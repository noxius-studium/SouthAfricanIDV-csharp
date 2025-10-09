using SouthAfricanId.Models;

namespace SouthAfricanId.Extraction
{

    /// <summary>
    /// Provides logic to extract gender information from a South African ID number.
    /// </summary>
    public class GenderExtraction
    {
        /// <summary>
        /// Extracts gender from the given ID number.
        /// </summary>
        /// <param name="idNumber">The South African ID number.</param>
        /// <returns>The extracted <see cref="Gender"/> value.</returns>
        public virtual Gender Extract(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) || idNumber.Length < 10)
                return Gender.Other;
            int genderDigits = int.Parse(idNumber.Substring(6, 4));
            return genderDigits < 5000 ? Gender.Female : Gender.Male;
        }
    }
}



