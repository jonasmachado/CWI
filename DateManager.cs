using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonasMachado
{
    public enum dateSubType
    {
        undefided = 0,
        year = 1,
        month = 2,
        day = 3,
        hour = 4,
        minute = 5
    }

    public class DateManager
    {
        public string ChangeDate(string date, char op, long value)
        {
            //   1      2     3    4      5
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            string sRet = "";
            // lets check the length and format
            if (date.Length != 16)
            {
                return "Date format error, the format expected is: 'dd/MM/yyyy HH24:mi'";
            }

            if (!op.Equals('+') && !op.Equals('-'))
            {
                return "Only + and - operators are accepted.";
            }
            if (getConvertedPartialDate(ref year, dateSubType.year, date) < 0)
            {
                return "Could not convert year, have you inserted numbers?";
            }
            else if (getConvertedPartialDate(ref month, dateSubType.month, date) < 0)
            {
                return "Could not convert month, have you inserted numbers?";
            }
            else if (getConvertedPartialDate(ref day, dateSubType.day, date) < 0)
            {
                return "Could not convert day, have you inserted numbers?";
            }
            else if (getConvertedPartialDate(ref hour, dateSubType.hour, date) < 0)
            {
                return "Could not convert hour, have you inserted numbers?";
            }
            else if (getConvertedPartialDate(ref minute, dateSubType.minute, date) < 0)
            {
                return "Could not convert minute, have you inserted numbers?";
            }

            if (!dateIsValid(day, month, year, hour, minute))
                return "Error in inserted format, max is '31/12/9999 59:59'";

            if (value < 0)
                value = value * (-1);

            if (op.Equals('+'))
            {
                //value now has total minutes 
                value += minute;

                //lets convert it to hours 
                hour += minutesToHours(ref value);

                //and now set the minutes to the rest of the operation above
                minute = (int)value;

                //Converting the rest of the data
                day += hoursToDays(ref hour);

                if (daysToMonths(ref day, ref month) < 0)
                    return "Error to convert day to months";

                year += monthToYears(ref month);

            }
            else if (op.Equals('-'))
            {
                long monthMinutes = (day * 1440) + (hour * 60) + minute;

                if (monthMinutes > value)
                {
                    monthMinutes -= value;

                    hour = minutesToHours(ref monthMinutes);

                    minute = (int)monthMinutes;

                    day = hoursToDays(ref hour);
                }
                else if (monthMinutes < value)
                {
                    int daysToSubtract;
                    int valueMinsModulo = ((int)value % 60);

                    if (minute > valueMinsModulo)
                        minute = ((int)value % 60) + minute;
                    else
                        minute = ((int)value % 60) + minute;


                    int hourModulo = ((int)value / 60) % 24;
                    if (hour > hourModulo)
                        hour = (hourModulo + hour) % 24;
                    else
                        hour = (hourModulo - hour) % 24;

                    daysToSubtract = (((int)value - minute) / 60) / 24;
                    year -= subtractDaysFromMonth(ref month, daysToSubtract, ref day);
                }

            }

            //format return
            sRet = day > 9 ? "" + day + "/" : "0" + day + "/";
            sRet = month > 9 ? sRet + month + "/" : sRet + "0" + month + "/";
            sRet = sRet + year + " ";
            sRet = hour > 9 ? sRet + "" + hour + ":" : sRet + "0" + hour + ":";
            sRet = minute > 9 ? sRet + "" + minute : sRet + "0" + minute;

            return sRet;
        }

        public bool dateIsValid(int day, int month, int year, int hour, int minute)
        {
            if (month > 12 || month < 1 || year > 9999 || year < 0 || hour > 23 || hour < 0 || minute > 59 || minute < 0)
            {
                return false;
            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day > 31)
                    return false;
            }
            else if (month == 2 && day > 28)
            {
                return false;
            }
            else if (month == 2 || month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day > 30)
                    return false;
            }

            return true;
        }

        public int subtractDaysFromMonth(ref int currentMonth, int daysToSubtract, ref int day)
        {
            int retYears = 0;

            for (int i = daysToSubtract; i > 0; i--)
            {
                day--;

                //Console.WriteLine("LAÇO = " + i + " DO DIA = " + day + " DO MES : " + currentMonth);
                if (currentMonth == 2 || currentMonth == 4 || currentMonth == 6 || currentMonth == 8 || currentMonth == 9 || currentMonth == 11)
                {
                    if (day == 1)
                    {
                        day = 32;
                        currentMonth--;
                    }
                }
                else if (currentMonth == 3)
                {
                    if (day == 1)
                    {
                        day = 29;
                        currentMonth--;
                    }
                }
                else if (currentMonth == 1)
                {
                    if (day == 1)
                    {
                        day = 32;
                        currentMonth--;
                        retYears++;
                        currentMonth = 12;
                    }
                }
                else
                {
                    if (day == 1)
                    {
                        day = 31;
                        currentMonth--;
                    }
                }
            }
            return retYears;
        }

        public int monthToYears(ref int months)
        {
            if (months < 1)
                return 0;
            int ret = months / 12;
            months = months % 12;

            return ret;
        }

        public int minutesToHours(ref long minutes)
        {
            if (minutes < 1)
                return 0;

            long ret = minutes / 60;
            minutes = minutes % 60;

            return (int)ret;
        }

        public int hoursToDays(ref int hours)
        {
            if (hours < 1)
                return 0;

            int ret = hours / 24;
            hours = hours % 24;

            return ret;
        }

        public int daysToMonths(ref int days, ref int currentMonth)
        {
            int moduloMonth;
            try
            {
                while (true)
                {
                    moduloMonth = currentMonth % 12;

                    if (moduloMonth == 1 || moduloMonth == 3 || moduloMonth == 5 || moduloMonth == 7 ||
                        moduloMonth == 8 || moduloMonth == 10 || moduloMonth == 0)
                    {
                        if (days < 32)
                        {
                            return 0;
                        }

                        days -= 31;
                        currentMonth += 1;
                    }
                    else if (moduloMonth == 2)
                    {
                        if (days < 29)
                        {
                            return 0;
                        }

                        days -= 28;
                        currentMonth += 1;
                    }
                    else
                    {
                        if (days < 31)
                        {
                            return 0;
                        }

                        days -= 30;
                        currentMonth += 1;
                    }
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }


        public int getConvertedPartialDate(ref int value, dateSubType subtype, string date)
        {
            int ret = 0;

            switch (subtype)
            {
                case dateSubType.year:
                    if (!int.TryParse(date.Substring(6, 4), out value))
                        ret = -1;
                    break;
                case dateSubType.month:
                    if (!int.TryParse(date.Substring(3, 2), out value))
                        ret = -1;
                    break;

                case dateSubType.day:
                    if (!int.TryParse(date.Substring(0, 2), out value))
                        ret = -1;
                    break;
                case dateSubType.hour:
                    if (!int.TryParse(date.Substring(11, 2), out value))
                        ret = -1;
                    break;
                case dateSubType.minute:
                    if (!int.TryParse(date.Substring(14, 2), out value))
                        ret = -1;
                    break;
            }

            return ret;
        }
    }
}
