using System.Runtime.CompilerServices;

namespace LocalDateTime
{
    /// <summary>
    /// Handles time, limited to hours and minutes.
    /// </summary>
    public sealed class LocalTime
    {
        /// <summary>
        /// Represents stored hour.
        /// </summary>
        private byte _hour;

        /// <summary>
        /// Represents stored minute.
        /// </summary>
        private byte _minute;

        /// <summary>
        /// Property of stored hour.
        /// </summary>
        public byte Hour
        {
            get => _hour;
            internal set
            {
                if (value is < 0 or > 23) 
                    throw new Exception("Hour must be between 0 and 23");

                _hour = value;
            }
        }

        /// <summary>
        /// Property of stored minute.
        /// </summary>
        public byte Minute
        {
            get => _minute;
            internal set
            {
                if (value is < 0 or > 59)
                    throw new Exception("Minute must be between 0 and 59");

                _minute = value;
            }
        }

        /// <summary>
        /// Convention constant for internal use. 
        /// </summary>
        private const byte HoursInADay = 24;

        /// <summary>
        /// Convention constant for internal use. 
        /// </summary>
        private const byte MinutesInAHour = 60;

        /// <summary>
        /// Convention constant for internal use. 
        /// </summary>
        private const int OneDayMinutes = HoursInADay * MinutesInAHour;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="hour">Hour</param>
        /// <param name="minute">_minute</param>
        public LocalTime(byte hour, byte minute)
        {
            Hour = hour;
            _minute = minute;
        }

        /// <summary>
        /// Add hours to this instance.
        /// </summary>
        /// <param name="hours">Hours to be added.</param>
        /// <returns>Elapsed days given added hours.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AddHours(long hours)
        {
            var hourSum = hours + _hour;
            _hour = (byte)(hourSum % HoursInADay);
            var elapsedDays = (int)(hourSum / HoursInADay);

            return elapsedDays;
        }

        /// <summary>
        /// Add minutes to this instance.
        /// </summary>
        /// <param name="minutes">Minutes to be added.</param>
        /// <returns>Elapsed days given added minutes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int AddMinutes(long minutes)
        {
            if (minutes < 0)
            {
                return SubtractMinutes(-minutes);
            }

            var minuteSum = minutes + _minute;
            var factor = (Math.Abs(minuteSum) % MinutesInAHour);
            _minute = (byte)(minuteSum < 0 ? (MinutesInAHour - factor) % MinutesInAHour : factor);

            return AddHours(minuteSum / MinutesInAHour);
        }

        /// <summary>
        /// Subtracts minutes.
        /// </summary>
        /// <param name="minutes">Minutes to subtract.</param>
        /// <returns>Negative elapsed days after subtraction of given numbers.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SubtractMinutes(long minutes)
        {
            var daysToSubtract = minutes / OneDayMinutes;
            minutes -= daysToSubtract * OneDayMinutes;
            var currentTotalMinutes = _hour * MinutesInAHour + _minute;

            if (currentTotalMinutes - minutes < 0)
            {
                daysToSubtract++;
            }

            currentTotalMinutes -= (int)minutes;

            if (currentTotalMinutes < 0)
            {
                currentTotalMinutes = OneDayMinutes + currentTotalMinutes;
            }

            _hour = (byte)(currentTotalMinutes / MinutesInAHour);
            _minute = (byte)(currentTotalMinutes % MinutesInAHour);

            return -(int)daysToSubtract;
        }

    }
}