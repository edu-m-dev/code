using System;

namespace Library
{
    public class FixedDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _fixedDateTime;

        public FixedDateTimeProvider(DateTime fixedDateTime)
        {
            _fixedDateTime = fixedDateTime;
        }

        public DateTime Now() => _fixedDateTime;

        public DateTime Today() => _fixedDateTime.Date;
    }
}