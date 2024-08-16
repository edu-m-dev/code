using System;

namespace _006_dateTime
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var calc = new SimpleDateTimeDifferenceCalculator();
            //calc.CalculateIsoDateDiff();
            //calc.CalculateIsoTimeDiff();
            new MenuBasedDateTimeDifferenceCalculator()
                .Show();
        }
    }
}