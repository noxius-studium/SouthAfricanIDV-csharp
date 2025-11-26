using SouthAfricanId.Models;
using SouthAfricanId.Extraction;
using SouthAfricanId.Validation;

namespace SouthAfricanId
{


    /// <summary>
    /// Decodes South African ID numbers and extracts information such as age and gender.
    /// </summary>

    /// <summary>
    /// Decodes South African ID numbers and extracts all available information.
    /// </summary>
    public class SouthAfricanIdDecoder
    {
        private readonly Validator _validator;
        private readonly AgeExtraction _ageExtractor;
        private readonly GenderExtraction _genderExtractor;

        public SouthAfricanIdDecoder(
            Validator validator = null,
            AgeExtraction ageExtractor = null,
            GenderExtraction genderExtractor = null)
        {
            _validator = validator ?? new Validator();
            _ageExtractor = ageExtractor ?? new AgeExtraction();
            _genderExtractor = genderExtractor ?? new GenderExtraction();
        }

        /// <summary>
        /// Decodes the specified ID number and extracts all available information.
        /// </summary>
        /// <param name="idNumber">The South African ID number to decode.</param>
        /// <returns>A <see cref="DecodedIdModel"/> containing the extracted information, or null if invalid.</returns>
        public DecodedIdModel Decode(string idNumber)
        {
            if (!_validator.Validate(idNumber))
                return null;
            var ageInfo = _ageExtractor.Extract(idNumber);
            var gender = _genderExtractor.Extract(idNumber);
            // Extract citizenship and historical digit
            bool? isCitizen = null;
            int? citizenshipDigit = null;
            int? historicalDigit = null;
            if (!string.IsNullOrEmpty(idNumber) && idNumber.Length == 13)
            {
                citizenshipDigit = int.Parse(idNumber.Substring(10, 1));
                isCitizen = citizenshipDigit == 0;
                historicalDigit = int.Parse(idNumber.Substring(11, 1));
            }
            return new DecodedIdModel
            {
                IdNumber = idNumber,
                AgeInfo = ageInfo,
                Gender = gender,
                IsCitizen = isCitizen,
                CitizenshipDigit = citizenshipDigit,
                HistoricalDigit = historicalDigit
            };
        }
    }

    /// <summary>
    /// Represents the decoded information from a South African ID number.
    /// </summary>
    public class DecodedIdModel
    {
        /// <summary>
        /// Gets or sets the ID number.
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// Gets or sets the extracted age information.
        /// </summary>
        public AgeInformation AgeInfo { get; set; }

        /// <summary>
        /// Gets or sets the extracted gender information.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the citizenship status (true = citizen, false = permanent resident, null = unknown).
        /// </summary>
        public bool? IsCitizen { get; set; }

        /// <summary>
        /// Gets or sets the citizenship digit (0 or 1).
        /// </summary>
        public int? CitizenshipDigit { get; set; }

        /// <summary>
        /// Gets or sets the historical digit (0-9).
        /// </summary>
        public int? HistoricalDigit { get; set; }
    }


}
