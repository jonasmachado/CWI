using static Xunit.Assert;

namespace LocalDateTime.Tests
{
    /// <summary>
    /// Unit tests of LocalTime.
    /// </summary>
    public class LocalTimeTests
    {
        /// <summary>
        /// Represents LocalTime instance that is going to be tested in this class.
        /// </summary>
        private readonly LocalTime _time;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int Hour = 15;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int Minute = 3;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocalTimeTests()
        {
            _time = new LocalTime(Hour, Minute);
        }

        /// <summary>
        /// Checks if it is possible to sum minutes to a LocalTime.
        /// </summary>
        [Fact(DisplayName = "Should sum minutes")]
        public void ShouldSumMinutes()
        {
            const int minutesToAdd = 183;
            const int expectedHour = 18;
            const int expectedMinutes = 6;

            _time.AddMinutes(minutesToAdd);

            Equal(expectedHour, _time.Hour);
            Equal(expectedMinutes, _time.Minute);
        }

        /// <summary>
        /// Checks if it is possible to subtract minutes in a LocalTime.
        /// </summary>
        [Fact(DisplayName = "Should subtract minutes")]
        public void ShouldSubtractDays()
        {
            const int minutesToSubtract = -600;
            const int expectedHour = 5;
            const int expectedMinutes = 3;

            _time.AddMinutes(minutesToSubtract);

            Equal(expectedHour, _time.Hour);
            Equal(expectedMinutes, _time.Minute);
        }
    }
}