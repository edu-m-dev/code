using System;
using DemoLibrary;

namespace ErrorHandlingChallenge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PaymentProcessor paymentProcessor = new PaymentProcessor();
            for (int i = 0; i <= 10; i++)
            {
                try
                {
                    var result = paymentProcessor.MakePayment($"Demo{ i }", i);
                    if (result == null)
                    {
                        Console.WriteLine($"Null value for item {i}", i);
                    }

                    Console.WriteLine(result.TransactionAmount);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Payment skipped for payment with {i} items", i);
                }
            }
            Console.ReadLine();
        }
    }
}