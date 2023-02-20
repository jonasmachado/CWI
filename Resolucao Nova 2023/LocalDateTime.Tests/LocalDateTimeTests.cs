using static Xunit.Assert;

namespace LocalDateTime.Tests
{
    /// <summary>
    /// Unit tests of main class LocalDateTime.
    /// </summary>
    public class LocalDateTimeTests
    {
        /// <summary>
        /// Util constant to be used as reference in this test class.
        /// </summary>
        private const string FixedDate = "18/02/2023 00:00";

        /// <summary>
        /// Util constant to be used as wrong date in this test class.
        /// </summary>
        private const string FixedWrongDate = "18/x2/2023 00:00";

        /// <summary>
        /// Util constant to be used as wrong date in this test class.
        /// </summary>
        private const string FixedWrongDateHigherDay = "30/02/2023 00:00";

        /// <summary>
        /// Util constant to be used as wrong date in this test class.
        /// </summary>
        private const string FixedWrongDateHigherMonth = "12/22/2023 00:00";

        /// <summary>
        /// LocalDateTime class instance to execute tests upon.
        /// </summary>
        private readonly LocalDateTime _date;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int DaysInAYear = 365;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int HoursInADay = 24;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int MinutesInAnHour = 60;

        /// <summary>
        /// Convention constant to use in test class.
        /// </summary>
        private const int OneYearMinutes = DaysInAYear * HoursInADay * MinutesInAnHour;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LocalDateTimeTests()
        {
            _date = new LocalDateTime();
        }

        /// <summary>
        /// Tests if it is possible to sum minutes to a date.
        /// </summary>
        [Fact(DisplayName = "Should sum date")]
        public void ShouldSumDate()
        {
            var result = _date.ChangeDate(FixedDate, '+', OneYearMinutes);
            Equal("18/02/2024 00:00", result);
        }

        /// <summary>
        /// Tests if it is possible to sum a big amount of minutes to a date.
        /// </summary>
        [Fact(DisplayName = "Should sum big amount to a date")]
        public void ShouldSumBigAmountToDate()
        {
            var result = _date.ChangeDate(FixedDate, '+', OneYearMinutes * 3250 + 65);
            Equal("18/02/5273 01:05", result);
        }

        /// <summary>
        /// Tests if it is possible to subtract minutes from a date.
        /// </summary>
        [Fact(DisplayName = "Should subtract date")]
        public void ShouldSubtractDate()
        {
            var result = _date.ChangeDate(FixedDate, '-', OneYearMinutes);
            Equal("18/02/2022 00:00", result);
        }

        /// <summary>
        /// Tests if it is possible to subtract a big amount of minutes from a date.
        /// </summary>
        [Fact(DisplayName = "Should subtract big amount from a date")]
        public void ShouldSubtractBigAmountFromDate()
        {
            var result = _date.ChangeDate(FixedDate, '-',  OneYearMinutes * 1823 + 123);
            Equal("17/02/0200 21:57", result);
        }

        /// <summary>
        /// Tests if class throws error when using an invalid date.
        /// </summary>
        [Fact(DisplayName = "Should throw invalid date")]
        public void ShouldThrowInvalidDate()
        {
            var exception = Throws<ArgumentException>(() => _date.ChangeDate(FixedWrongDate, '-', MinutesInAnHour));
            IsType<ArgumentException>(exception);
            Contains("It was not possible to convert provided", exception.Message);
        }

        /// <summary>
        /// Tests if class throws error when using an invalid date.
        /// Day higher than supported
        /// </summary>
        [Fact(DisplayName = "Should throw invalid date, when day is higher than the months supports.")]
        public void ShouldThrowInvalidDateDayHigherThanExpected()
        {
            var exception = Throws<Exception>(() => _date.ChangeDate(FixedWrongDateHigherDay, '-', MinutesInAnHour));
            Equal("Month 2 day must be between 1 and 28", exception.Message);
        }

        /// <summary>
        /// Tests if class throws error when using an invalid date.
        /// Day higher than supported
        /// </summary>
        [Fact(DisplayName = "Should throw invalid date, when month is higher than the months supports.")]
        public void ShouldThrowInvalidDateMonthHigherThanExpected()
        {
            var exception = Throws<Exception>(() => _date.ChangeDate(FixedWrongDateHigherMonth, '-', MinutesInAnHour));
            Equal("Minimum month is 1 and max 12. Provided: 22", exception.Message);
        }

        /// <summary>
        /// Tests if class throws error when using an invalid operator.
        /// </summary>
        [Fact(DisplayName = "Should throw invalid operator")]
        public void ShouldThrowInvalidOperator()
        {
            var exception = Throws<ArgumentException>(() => _date.ChangeDate(FixedWrongDate, '1', MinutesInAnHour));
            IsType<ArgumentException>(exception);
        }
    }
}