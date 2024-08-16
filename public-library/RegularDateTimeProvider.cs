using System;

namespace Library
{
    public class RegularDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;

        public DateTime Today() => DateTime.Today;
    }
}