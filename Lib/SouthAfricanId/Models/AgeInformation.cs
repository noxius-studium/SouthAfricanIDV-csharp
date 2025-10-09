using System;

namespace SouthAfricanId.Models
{

    /// <summary>
    /// Represents age information extracted from a South African ID number.
    /// </summary>
    public class AgeInformation
    {
        /// <summary>
        /// Gets the number of years.
        /// </summary>
        public int Years { get; }

        /// <summary>
        /// Gets the number of months.
        /// </summary>
        public int Months { get; }

        /// <summary>
        /// Gets the number of days.
        /// </summary>
        public int Days { get; }

        /// <summary>
        /// Gets the date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; }

        /// <summary>
        /// Gets the age as a formatted string.
        /// </summary>
        public string AgeString => $"{Years} years, {Months} months, {Days} days";

        /// <summary>
        /// Initializes a new instance of the <see cref="AgeInformation"/> class.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        public AgeInformation(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            var now = DateTime.Now;
            Years = now.Year - dateOfBirth.Year;
            Months = now.Month - dateOfBirth.Month;
            Days = now.Day - dateOfBirth.Day;

            if (Days < 0)
            {
                Months--;
                Days += DateTime.DaysInMonth(now.Year, (now.Month == 1 ? 12 : now.Month - 1));
            }
            if (Months < 0)
            {
                Years--;
                Months += 12;
            }
        }
    }


}

