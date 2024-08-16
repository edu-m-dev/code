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
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Skipped invalid record");
                    WriteInnerException(e);
                }
                catch (FormatException e) when (i != 5)
                {
                    Console.WriteLine("Formatting Issue");
                    WriteInnerException(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Payment skipped for payment with {i} items", i);
                    WriteInnerException(e);
                }
            }
            Console.ReadLine();
        }

        private static void WriteInnerException(Exception e)
        {
            if (!string.IsNullOrWhiteSpace(e?.InnerException?.Message))
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}