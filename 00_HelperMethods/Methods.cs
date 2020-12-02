using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_HelperMethods
{
    public class Methods
    {
        //=======================DATE TIME FUNCTIONS===================//

        //LEAP YEARS

        /// <summary>
        /// Enter a year as an int.
        /// </summary>
        /// <param name="year"></param>
        /// <returns>True or False</returns>
        public bool IsLeapYear(int year)
        {
            bool leap = false;

            if (year % 4 == 0) leap = true;
            if (year % 100 == 0) leap = false;
            if (year % 400 == 0) leap = true;

            return leap;
        }

        /// <summary>
        /// Enter a date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>True or False</returns>
        public bool IsLeapYear(DateTimeOffset date)
        {
            int year = date.Year;
            bool leap = false;

            if (year % 4 == 0) leap = true;
            if (year % 100 == 0) leap = false;
            if (year % 400 == 0) leap = true;

            return leap;
        }

        /// <summary>
        /// Determines if the Current Date is in a leap year
        /// </summary>
        /// <returns>True or False</returns>
        public bool IsLeapYear()
        {
            int year = DateTime.Now.Year;
            bool leap = false;

            if (year % 4 == 0) leap = true;
            if (year % 100 == 0) leap = false;
            if (year % 400 == 0) leap = true;

            return leap;
        }


        //DATETIME CONVERSIONS

        /// <summary>
        /// Converts a date to a string "yyyymmdd"
        /// </summary>
        /// <param name="date"></param>
        /// <returns>An input date as a string</returns>
        public string DateToString(DateTimeOffset date)
        {
            return date.ToString("yyyymmdd");

        }

        /// <summary>
        /// Converts the current date to a string "yyyymmdd"
        /// </summary>
        /// <returns>date as a string</returns>
        public string NowDateString()
        {
            return DateTime.Now.ToString("yyyymmdd");
        }

        /// <summary>
        /// Finds the following Monday from a given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A date that is a Monday</returns>
        public DateTimeOffset FindNextMonday(DateTimeOffset date)
        {
            int day = (int)date.DayOfWeek;

            int dayAdd = 8 - day + 1;

            return date.AddDays(dayAdd);
        }

        /// <summary>
        /// Find the follwoing Monday from the Current Date
        /// </summary>
        /// <returns>The next Monday from the Current Date</returns>

        public DateTimeOffset FindNextMonday()
        {
            int day = (int)DateTime.Now.DayOfWeek;

            int dayAdd = 8 - day + 1;

            return DateTime.Now.AddDays(dayAdd);
        }




        /// <summary>
        /// Finds the first of a month from a given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The date of the first day of the month from the date argument</returns>
        public DateTime FirstOfMonth(DateTimeOffset date)
        {
            DateTime first = new DateTime(date.Year, date.Month, 1);

            return first;
        }

        /// <summary>
        /// Finds the first of the current month
        /// </summary>
        /// <returns>The date of the first of the current date month</returns>
        public DateTime FirstOfThisMonth()
        {
            DateTime nowDate = DateTime.Now;
            DateTime first = new DateTime(nowDate.Year, nowDate.Month, 1);
            return first;
        }

        /// <summary>
        /// Finds the last day of the month
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The date of the last of the month from the date argument</returns>
        public DateTime LastOfMonth(DateTimeOffset date)
        {
            DateTime last = new DateTime(date.Year, date.Month + 1, 1).AddDays(-1);

            return last;
        }

        /// <summary>
        /// Finds the last of the current month
        /// </summary>
        /// <returns>The date of the last of the current month</returns>
        public DateTime LastOfThisMonth()
        {
            DateTime nowDate = DateTime.Now;
            DateTime first = new DateTime(nowDate.Year, nowDate.Month + 1, 1).AddDays(-1);
            return first;
        }

        public DateTimeOffset ConvertToEndOfDayTime(DateTimeOffset dateTimeOffset)
        {
            //first we determine what the time is
            string time = dateTimeOffset.ToString("t");


            //if not equal to 5pm then change it to equal that time
            if(time != "5:00 PM")
            {
                //figure out the difference in time
                TimeSpan fivePM = new TimeSpan(5, 0, 0);
                TimeSpan difference = (dateTimeOffset.TimeOfDay - fivePM).Duration();

                //resolve the difference
                if(dateTimeOffset.TimeOfDay < fivePM)
                {
                    dateTimeOffset += difference;
                }
                dateTimeOffset -= difference;
            }

            return dateTimeOffset;
        }

        public DateTimeOffset ConvertToDayOfWeek(DateTimeOffset dateTimeOffset)
        {
            //determine what day it is 
            DayOfWeek dayOfWeek = dateTimeOffset.DayOfWeek;

            if(dayOfWeek == DayOfWeek.Saturday)
            {
                dateTimeOffset.AddDays(2);
            } else if(dayOfWeek == DayOfWeek.Sunday)
            {
                dateTimeOffset.AddDays(1);
            } else
            {
                dateTimeOffset.AddDays(0);
            }

            return dateTimeOffset;
        }
    }
}
