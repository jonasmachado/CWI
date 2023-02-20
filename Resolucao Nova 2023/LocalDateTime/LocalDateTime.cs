using System.Runtime.CompilerServices;

namespace LocalDateTime
{
    /// <summary>
    /// Represents a date and a time. It provides the feature to change a date based in a string of specific format.
    /// </summary>
    public sealed class LocalDateTime
    {
        /// <summary>
        /// LocalDate instance.
        /// </summary>
        public LocalDate LocalDate { get; }

        /// <summary>
        /// LocalTime instance.
        /// </summary>
        public LocalTime LocalTime { get; }

        /// <summary>
        /// Default constructor. Builds localtime with minimum valid value. 
        /// </summary>
        public LocalDateTime()
        {
            LocalTime = new LocalTime(hour: 0, minute: 0);
            LocalDate = new LocalDate(day: 1, month: 1, year: 0);
        }

        /// <summary>
        /// Provides the feature to sum or subtract minutes to a date.
        /// </summary>
        /// <param name="date">A date in format: 'dd/MM/yyyy hh:mm'.</param>
        /// <param name="op">Represents the operation. Supports + ou -.</param>
        /// <param name="value">The amount of minutes to be operated in given date.</param>
        /// <returns>Result of date changed string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ChangeDate(string date, char op, long value)
        {
            ValidateOperation(op);
            ParseLocalDateTime(date);

            var elapsedDays = LocalTime.AddMinutes(op is '+' ? value : value * -1);

            LocalDate.AddDays(elapsedDays);
 
            return $"{LocalDate.Day:00}/{LocalDate.Month:00}/{LocalDate.Year:0000} {LocalTime.Hour:00}:{LocalTime.Minute:00}";
        }

        /// <summary>
        /// Parses date in expected format.
        /// </summary>
        /// <param name="date"></param>
        private void ParseLocalDateTime(string date)
        {
            try
            {
                LocalDate.Year = int.Parse(date.Substring(startIndex: 6, length: 4));
                LocalDate.Month = byte.Parse(date.Substring(startIndex: 3, length: 2));
                LocalDate.Day = byte.Parse(date[..2]);
                LocalTime.Hour = byte.Parse(date.Substring(startIndex: 11, length: 2));
                LocalTime.Minute = byte.Parse(date.Substring(startIndex: 14, length: 2));
            }
            catch (FormatException ex)
            {
                throw new ArgumentException($"It was not possible to convert provided date {date}.", ex);
            }
        }

        /// <summary>
        /// Validates operation
        /// </summary>
        /// <param name="op">Operation parameter. Accepts + or -.</param>
        /// <exception cref="ArgumentException">If operation is invalid, method will throw.</exception>
        private static void ValidateOperation(char op)
        {
            if (op is '+' or '-')
                return;

            throw new ArgumentException("Operation not supported. Use + or -");
        }
    }
}