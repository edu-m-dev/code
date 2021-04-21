using System;

namespace _006_dateTime
{
    public class SimpleDateTimeDifferenceCalculator
    {
        public void CalculateIsoDateDiff()
        {
            Console.WriteLine("Enter a date in iso 8601 format");
            var parsed =
                DateTime.TryParse(Console.ReadLine(), out var date);
            Console.WriteLine(parsed ?
                $"The date you entered is {date.ToLongDateString()}"
                : "Unable to parse the date you entered");
            if (parsed)
            {
                Utilities.CalculateAndOutputDateDifference(date);
            }
        }

        public void CalculateIsoTimeDiff()
        {
            Console.WriteLine("Enter a time in iso 8601 format");
            var parsed =
                DateTime.TryParse(Console.ReadLine(), out var time);
            Console.WriteLine(parsed ?
                $"The time you entered is {time.ToLongTimeString()}"
                : "Unable to parse the time you entered");
            if (parsed)
            {
                Utilities.CalculateAndOutputTimeDifference(time);
            }
        }
    }
}