using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Challenges;

[TestClass()]
public class InheritanceTests
{
    [TestMethod()]
    public void TestInheritance()
    {
        //Inheritance.CreateInstances();
        Inheritance.CreateVirtualThenOverrideWithSealed();
    }
}
