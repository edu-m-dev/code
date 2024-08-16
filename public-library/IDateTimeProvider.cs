using System;

namespace Library
{
    public interface IDateTimeProvider
    {
        DateTime Now();

        DateTime Today();
    }
}