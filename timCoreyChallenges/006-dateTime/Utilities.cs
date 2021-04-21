using System;

namespace _006_dateTime
{
    public static class Utilities
    {
        public static void CalculateAndOutputDateDifference(DateTime date)
        {
            Console.WriteLine($"That was {(int)(DateTime.Now - date).TotalDays} days ago");
        }

        public static void CalculateAndOutputTimeDifference(DateTime time)
        {
            var timeDiff = DateTime.Now.TimeOfDay - time.TimeOfDay;
            Console.WriteLine($"That was {timeDiff.Hours} hours and {timeDiff.Minutes % 60} minutes ago");
        }
    }
}