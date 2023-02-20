using System.Runtime.CompilerServices;

namespace LocalDateTime
{
    /// <summary>
    /// Handles date operations
    /// </summary>
    public sealed class LocalDate
    {
        /// <summary>
        /// Stored day.
        /// </summary>
        private byte _day;

        /// <summary>
        /// Stored month.
        /// </summary>
        private byte _month;

        /// <summary>
        /// Stored year.
        /// </summary>
        private int _year;

        /// <summary>
        /// Property of stored day.
        /// </summary>
        public byte Day
        {
            get => _day;
            internal set
            {
                if (value < MinDay || value > (byte)_monthDays[_month -1])
                    throw new Exception($"Month {Month} day must be between 1 and {_monthDays[_month -1]}");

                _day = value;
            }
        }

        /// <summary>
        /// Represents stored Month.
        /// </summary>
        public byte Month
        {
            get => _month;
            internal set
            {
                if(value is < MinMonth or > MaxMonth)
                    throw new Exception($"Minimum month is {MinMonth} and max {MaxMonth}. Provided: {value}");

                _month = value;
            }
        }

        /// <summary>
        /// Represents stored Year.
        /// </summary>
        public int Year
        {
            get => _year;
            internal set
            {
                if (value is < MinYear or > MaxYear)
                    throw new Exception($"Gregorian calendar does not support negative years or above 9999. Current: {value}");

                _year = value;
            }
        }

        /// <summary>
        /// Convention constant for internal use.
        /// </summary>
        private const byte MinDay = 1;

        /// <summary>
        /// Convention constant for internal use.
        /// </summary>
        private const byte MaxMonth = 12;

        /// <summary>
        /// Convention constant for internal use.
        /// </summary>
        private const byte MinMonth = 1;

        /// <summary>
        /// Convention constant for internal use.
        /// </summary>
        private const int DaysInAYear = 365;

        /// <summary>
        /// Convention constant for internal use.
        /// </summary>
        private const int MaxYear = 9999;

        /// <summary>
        /// Convention constant for internal use.
        /// </summary>
        private const int MinYear = 0;

        /// <summary>
        /// Convention collection for internal use.
        /// Represents month capacity.
        /// </summary>
        private readonly int[] _monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// Convention collection for internal use.
        /// Represents accumulated days in respective (index) month.
        /// </summary>
        private readonly int[] _monthAccumulatedDays = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="month">Month</param>
        /// <param name="year">Year</param>
        public LocalDate (byte day, byte month, int year)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        /// <summary>
        /// Add days to the current date.
        /// </summary>
        /// <param name="days">The amount of days to be added.</param>
        /// <exception cref="Exception">If the amount of days results in an invalid year, then throws exception.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddDays(int days)
        {
            var totalDays = GetCurrentYearTotalDays() + DaysInAYear * (Year - 1) + days;
            var completeYears = totalDays / DaysInAYear;
            var dayModule = totalDays % DaysInAYear;
            var currentMonth = Array.FindIndex(_monthAccumulatedDays, m => m >= dayModule);
            var currentDay = dayModule - (currentMonth > 1 ? _monthAccumulatedDays[currentMonth - 1] : 0);

            Year = completeYears + 1;
            _month = (byte)currentMonth;
            _day = (byte)currentDay;
        }

        /// <summary>
        /// Checks accumulated days until current date.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int GetCurrentYearTotalDays()
        {
            return _monthAccumulatedDays[_month - 1] + _day;
        }
    }
}