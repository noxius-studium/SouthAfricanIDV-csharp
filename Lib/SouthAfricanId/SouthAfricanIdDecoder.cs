using SouthAfricanId.Models;
using SouthAfricanId.Extraction;
using SouthAfricanId.Validation;

namespace SouthAfricanId
{


    /// <summary>
    /// Decodes South African ID numbers and extracts information such as age and gender.
    /// </summary>
    public class SouthAfricanIdDecoder
    {
        private readonly Validator _validator;
        private readonly AgeExtraction _ageExtractor;
        private readonly GenderExtraction _genderExtractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SouthAfricanIdDecoder"/> class.
        /// </summary>
        public SouthAfricanIdDecoder()
        {
            _validator = new Validator();
            _ageExtractor = new AgeExtraction();
            _genderExtractor = new GenderExtraction();
        }

        /// <summary>
        /// Decodes the specified ID number and extracts age and gender information.
        /// </summary>
        /// <param name="idNumber">The South African ID number to decode.</param>
        /// <returns>A <see cref="DecodedIdModel"/> containing the extracted information, or null if invalid.</returns>
        public DecodedIdModel Decode(string idNumber)
        {
            if (!_validator.Validate(idNumber))
                return null;
            var ageInfo = _ageExtractor.Extract(idNumber);
            var gender = _genderExtractor.Extract(idNumber);
            return new DecodedIdModel
            {
                IdNumber = idNumber,
                AgeInfo = ageInfo,
                Gender = gender,
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
    }


}
