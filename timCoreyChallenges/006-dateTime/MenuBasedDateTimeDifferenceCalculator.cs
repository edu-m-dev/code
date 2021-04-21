using ConsoleTools;
using System;
using System.Globalization;

namespace _006_dateTime
{
    public class MenuBasedDateTimeDifferenceCalculator
    {
        private enum DateFormat
        {
            dayFirst,
            monthFirst,
        }

        private enum TimeFormat
        {
            am_pm,
            h24,
        }

        private readonly ConsoleMenu consoleMenu;

        public MenuBasedDateTimeDifferenceCalculator()
        {
            // TODO - dry, .AddMenu()
            var dateMenu = new ConsoleMenu()
                .Add("dd/MM/yyyy", () => this.CalculateDateDiff(DateFormat.dayFirst))
                .Add("MM/dd/yyyy", () => this.CalculateDateDiff(DateFormat.monthFirst))
                .Add("Back", () => this.consoleMenu.Show())
                .Configure(config =>
                {
                    config.EnableFilter = true;
                    config.Title = "Select date format";
                    config.EnableWriteTitle = true;
                    config.EnableBreadcrumb = true;
                });
            var timeMenu = new ConsoleMenu()
                .Add("h24", () => this.CalculateTimeDiff(TimeFormat.h24))
                .Add("am/pm", () => this.CalculateTimeDiff(TimeFormat.am_pm))
                .Add("Back", () => this.consoleMenu.Show())
                .Configure(config =>
                {
                    config.EnableFilter = true;
                    config.Title = "Select time format";
                    config.EnableWriteTitle = true;
                    config.EnableBreadcrumb = true;
                });
            this.consoleMenu = new ConsoleMenu()
                .Add("Date", () => dateMenu.Show())
                .Add("Time", () => timeMenu.Show())
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.EnableFilter = true;
                    config.Title = "Select date or time";
                    config.EnableWriteTitle = true;
                    config.EnableBreadcrumb = true;
                });
        }

        public void Show()
        {
            this.consoleMenu.Show();
        }

        // TODO - dry, include iso in enum, create enum - pattern dict
        private void CalculateDateDiff(DateFormat dateFormat)
        {
            Console.WriteLine($"Enter a date in {dateFormat} format");
            var parsed =
                DateTime.TryParseExact(Console.ReadLine(), dateFormat == DateFormat.dayFirst ? "dd/MM/yyyy" : "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            Console.WriteLine(parsed ?
                $"The date you entered is {date.ToLongDateString()}"
                : "Unable to parse the date you entered");
            if (parsed)
            {
                Utilities.CalculateAndOutputDateDifference(date);
            }
            Console.ReadLine();
        }

        private void CalculateTimeDiff(TimeFormat timeFormat)
        {
            Console.WriteLine($"Enter a time in {timeFormat} format");
            var parsed =
                DateTime.TryParseExact(Console.ReadLine(), timeFormat == TimeFormat.h24 ? "HH:mm:ss" : "h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time);
            Console.WriteLine(parsed ?
                $"The time you entered is {time.ToLongTimeString()}"
                : "Unable to parse the time you entered");
            if (parsed)
            {
                Utilities.CalculateAndOutputTimeDifference(time);
            }
            Console.ReadLine();
        }
    }
}