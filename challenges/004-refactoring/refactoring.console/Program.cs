using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactoring.bl;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string actionToTake = "";

            //IUserRepository repo = new DapperUserRepository();
            IUserRepository repo = new EFUserRepository();
            do
            {
                Console.Write("What action do you want to take (Display, Add, Find or Quit): ");
                actionToTake = Console.ReadLine();

                switch (actionToTake.ToLower())
                {
                    case "display":
                        var records = repo.GetAll();
                        Console.WriteLine();
                        foreach (var x in records)
                        {
                            Console.WriteLine($"{ x.FirstName } { x.LastName }");
                        }
                        Console.WriteLine();
                        break;

                    case "add":
                        Console.Write("What is the first name: ");
                        string firstName = Console.ReadLine();

                        Console.Write("What is the last name: ");
                        string lastName = Console.ReadLine();

                        var user = new UserModel
                        {
                            Id = default,
                            FirstName = firstName,
                            LastName = lastName
                        };
                        repo.Add(user);

                        Console.WriteLine();
                        break;

                    case "find":
                        Console.Write("What is the name filter: ");
                        string name = Console.ReadLine();

                        records = repo.GetByFirstOrLastName(name);
                        Console.WriteLine();
                        foreach (var x in records)
                        {
                            Console.WriteLine($"{ x.FirstName } { x.LastName }");
                        }
                        Console.WriteLine();
                        break;

                    default:
                        break;
                }
            } while (actionToTake.ToLower() != "quit");
        }
    }
}