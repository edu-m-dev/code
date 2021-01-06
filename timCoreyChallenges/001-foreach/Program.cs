using System;
using System.Collections.Generic;

namespace tidbits
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintNames();
            PrintPeople();
        }

        private static void PrintPeople()
        {
            var names = new List<Person> {
                new Person
                {
                    FirstName = "Sam",
                    LastName = "Smith"
                },
                new Person
                {
                    FirstName = "Mary",
                    LastName = "Berry"
                },
            };

            foreach (var name in names)
            {
                Console.WriteLine($"{name.FirstName} {name.LastName}");
            }
        }

        private static void PrintNames()
        {
            var names = new List<string> { "Sam", "Mary" };
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}