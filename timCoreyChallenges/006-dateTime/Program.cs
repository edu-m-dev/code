using System;

namespace _006_dateTime
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CalculateIsoDateDiff();
            CalculateIsoTimeDiff();
        }

        private static void CalculateIsoDateDiff()
        {
            Console.WriteLine("Enter a date in iso 8601 format");
            var parsed =
                DateTime.TryParse(Console.ReadLine(), out var date);
            Console.WriteLine(parsed ?
                $"The date you entered is {date.ToLongDateString()}"
                : "Unable to parse the date you entered");
            if (parsed)
            {
                Console.WriteLine($"That was {(int)(DateTime.Now - date).TotalDays} days ago");
            }
        }

        private static void CalculateIsoTimeDiff()
        {
            Console.WriteLine("Enter a time in iso 8601 format");
            var parsed =
                DateTime.TryParse(Console.ReadLine(), out var time);
            Console.WriteLine(parsed ?
                $"The time you entered is {time.ToLongTimeString()}"
                : "Unable to parse the time you entered");
            if (parsed)
            {
                var timeDiff = DateTime.Now.TimeOfDay - time.TimeOfDay;
                Console.WriteLine($"That was {timeDiff.Hours} hours and {timeDiff.Minutes % 60} minutes ago");
            }
        }
    }
}