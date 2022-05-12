using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Challenges;

[TestClass()]
public class DatesTest
{
    [TestMethod()]
    public void TransformedDatesAreAsExpected()
    {
        Console.WriteLine(1 << 2);
        var input = new List<string> { "2010/02/20", "19/12/2016", "11-18-2012", "20130720" };
        DateTransform.TransformDateFormat(input).ForEach(Console.WriteLine);
    }
}
