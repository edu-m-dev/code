using System;
using System.Linq;

namespace Company.Product.Aggregate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var a = Array.Empty<int>()
                .DefaultIfEmpty();
            int max = a.Aggregate(
              (acc, cur) => cur > acc ? cur : acc);
            Console.WriteLine(max);
        }
    }
}