using static Xunit.Assert;

namespace LocalDateTime.Tests
{
    /// <summary>
    /// Unit tests of class LocalDate.
    /// </summary>
    public class LocalDateTests
    {
        /// <summary>
        /// LocalDate Instance to use in tests.
        /// </summary>
        private readonly LocalDate _date;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int Day = 15;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int Month = 3;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int Year = 9950;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocalDateTests()
        {
            _date = new LocalDate(Day, Month, Year);
        }

        /// <summary>
        /// Tests if it possible to sum days to a date.
        /// </summary>
        [Fact(DisplayName = "Should sum days")]
        public void ShouldSumDys()
        {
            const int expectedDay = 20;
            const int addDays = 5;

            _date.AddDays(addDays);

            AssertDate(expectedDay, Month, Year);
        }

        /// <summary>
        /// Tests if it possible to subtract days from a date.
        /// </summary>
        [Fact(DisplayName = "Should subtract days")]
        public void ShouldSubtractDays()
        {
            const int expectedDay = 13;
            const int expectedMonth = 9;
            const int expectedYear = 9949;
            const int addDays = - 183;

            _date.AddDays(addDays);

            AssertDate(expectedDay, expectedMonth, expectedYear);
        }

        /// <summary>
        /// Tests if it throws when using an invalid year.
        /// </summary>
        [Fact(DisplayName = "Should throw year not supported")]
        public void ShouldThrowYearOverflow()
        {
            const int addDays = 999999999;
            const string expectedMessage = "Gregorian calendar does not support negative years or above 9999.";
            var exception = Throws<Exception>(() => _date.AddDays(addDays));

            Contains(expectedMessage, exception.Message);
        }

        /// <summary>
        /// Convention assert method.
        /// </summary>
        private void AssertDate(int expectedDay, int expectedMonth, int expectedYear)
        {
            Equal(expectedDay, _date.Day);
            Equal(expectedMonth, _date.Month);
            Equal(expectedYear, _date.Year);
        }
    }
}