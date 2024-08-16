using ConsoleTools;
using System;
using System.Collections.Generic;
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
            var dateMenu = this.CreateMenuItem(
                new List<(string, Action)> {
                    ("dd/MM/yyyy", () => this.CalculateDateDiff(DateFormat.dayFirst)),
                    ("MM/dd/yyyy", () => this.CalculateDateDiff(DateFormat.monthFirst)),
                    ("Back", () => this.consoleMenu.Show()),},
                "Select date format");

            var timeMenu = this.CreateMenuItem(
                new List<(string, Action)>{
                    ("h24", () => this.CalculateTimeDiff(TimeFormat.h24)),
                    ("am/pm", () => this.CalculateTimeDiff(TimeFormat.am_pm)),
                    ("Back", () => this.consoleMenu.Show()),},
                "Select time format");

            this.consoleMenu = this.CreateMenuItem(
                new List<(string, Action)>{
                    ("Date", () => dateMenu.Show()),
                    ("Time", () => timeMenu.Show()),
                    ("Exit", () => Environment.Exit(0)),},
                "Select date or time");
        }

        private ConsoleMenu CreateMenuItem(IReadOnlyList<(string name, Action action)> options, string title)
        {
            var menu = new ConsoleMenu()
                .Configure(config =>
                {
                    config.EnableFilter = true;
                    config.Title = title;
                    config.EnableWriteTitle = true;
                    config.EnableBreadcrumb = true;
                });
            foreach (var (name, action) in options)
            {
                menu.Add(name, action);
            }
            return menu;
        }

        public void Show()
        {
            this.consoleMenu.Show();
        }

        // TODO - dry, separation of concerns, include iso in enum, create enum - pattern dict
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