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
            var max = a.Aggregate(
              (acc, cur) => cur > acc ? cur : acc);
            Console.WriteLine(max);
        }
    }
}
