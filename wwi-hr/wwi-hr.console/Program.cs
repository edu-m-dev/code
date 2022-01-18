using System;
using wwi.hr.EF;

namespace wwi.hr
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using var dbContext = new WwiContext();
            var people = dbContext.People;
            foreach (var person in people)
            {
                Console.WriteLine($"{ person.FullName}");
            }
        }
    }
}