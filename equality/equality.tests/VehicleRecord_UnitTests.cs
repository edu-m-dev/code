using equality.bl;

namespace equality.tests;

[TestClass]
public class VehicleRecord_UnitTests
{
    [TestMethod]
    public void Equality_SameManufacturerAndModelAndYear()
    {
        VehicleRecord vehicle1 = new VehicleRecord("Renault", "Fluence", 2011);
        VehicleRecord vehicle2 = new VehicleRecord("Renault", "Fluence", 2011);
        EqualityTests.TestEqualObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_SameManufacturerAndModelDistinctYear()
    {
        VehicleRecord vehicle1 = new VehicleRecord("Renault", "Fluence", 2011);
        VehicleRecord vehicle2 = new VehicleRecord("Renault", "Fluence", 2012);
        EqualityTests.TestUnequalObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_SameManufacturerDistinctModel()
    {
        VehicleRecord vehicle1 = new VehicleRecord("Renault", "Fluence", 2011);
        VehicleRecord vehicle2 = new VehicleRecord("Renault", "Clio", 2011);
        EqualityTests.TestUnequalObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_DistinctManufacturerSameModel()
    {
        VehicleRecord vehicle1 = new VehicleRecord("Opel", "Fluence", 2011);
        VehicleRecord vehicle2 = new VehicleRecord("Renault", "Fluence", 2011);
        EqualityTests.TestUnequalObjects(vehicle1, vehicle2);
    }

    [TestMethod]
    public void Inequality_ComparingWithNull()
    {
        VehicleRecord vehicle = new VehicleRecord("Renault", "Fluence", 2011);
        EqualityTests.TestAgainstNull(vehicle);
    }
}
