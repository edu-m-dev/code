using equality.bl;

namespace equality.tests;

[TestClass]
public class Vehicle_UnitTests
{
    [TestMethod]
    public void Equality_SameManufacturerAndModelDistinctYear()
    {
        Vehicle vehicle1 = new Vehicle("Renault", "Fluence", 2011);
        Vehicle vehicle2 = new Vehicle("Renault", "Fluence", 2012);
        EqualityTests.TestEqualObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_SameManufacturerDistinctModel()
    {
        Vehicle vehicle1 = new Vehicle("Renault", "Fluence", 2011);
        Vehicle vehicle2 = new Vehicle("Renault", "Clio", 2011);
        EqualityTests.TestUnequalObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_DistinctManufacturerSameModel()
    {
        Vehicle vehicle1 = new Vehicle("Opel", "Fluence", 2011);
        Vehicle vehicle2 = new Vehicle("Renault", "Fluence", 2011);
        EqualityTests.TestUnequalObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_ComparingWithNull()
    {
        Vehicle vehicle = new Vehicle("Renault", "Fluence", 2011);
        EqualityTests.TestAgainstNull(vehicle);
    }
}
