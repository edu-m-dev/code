using equality.bl;

IEnumerable<Vehicle> vehicles = LoadVehicles();

ReportVehicles(vehicles);

CreateClassHashset();
CreateRecordHashset();
CreateRecordWithNoPublicPropertiesHashset();

void CreateClassHashset()
{
    HashSet<Vehicle> hashSet = new();
    var vehicles = new List<Vehicle>()
    {
                new Vehicle("Fiat", "Bravo", 1995),
                new Vehicle("Fiat", "Bravo", 1996),
                new Vehicle("Fiat", "Brava", 1995),
                new Vehicle("Fiat", "Brava", 1996),
    };
    foreach (var vehicle in vehicles)
    {
        hashSet.Add(vehicle);
    }
    Console.WriteLine($"Class HashSet count is {hashSet.Count}");
}

void CreateRecordHashset()
{
    HashSet<SimpleVehicleRecord> hashSet = new();
    var vehicles = new List<SimpleVehicleRecord>()
    {
                new SimpleVehicleRecord("Fiat", "Bravo", 1995),
                new SimpleVehicleRecord("Fiat", "Bravo", 1996),
                new SimpleVehicleRecord("Fiat", "Brava", 1995),
                new SimpleVehicleRecord("Fiat", "Brava", 1996),
    };
    foreach (var vehicle in vehicles)
    {
        hashSet.Add(vehicle);
    }
    Console.WriteLine($"Record HashSet count is {hashSet.Count}");
}

void CreateRecordWithNoPublicPropertiesHashset()
{
    HashSet<VehicleRecord> hashSet = new();
    var vehicles = new List<VehicleRecord>()
    {
                new VehicleRecord("Fiat", "Bravo", 1995),
                new VehicleRecord("Fiat", "Bravo", 1996),
                new VehicleRecord("Fiat", "Brava", 1995),
                new VehicleRecord("Fiat", "Brava", 1996),
    };
    foreach (var vehicle in vehicles)
    {
        hashSet.Add(vehicle);
    }
    Console.WriteLine($"Record (no public properties) HashSet count is {hashSet.Count}");
}


Console.ReadLine();

static IEnumerable<Vehicle> LoadVehicles()
{
    return new List<Vehicle>()
            {
                new Vehicle("Fiat", "Bravo",1995),
                new Vehicle("Opel", "Meriva",2004),
                new Vehicle("Renault", "Logan",2006),
                new Vehicle("Opel", "Speedster",2002),
                new Vehicle("Renault", "Clio",1996),
                new Vehicle("Fiat", "Bravo",1997),
                new Vehicle("Opel", "Meriva",2007),
                new Vehicle("Renault", "Fluence",2011),
                new Vehicle("Fiat", "Seicento",1998),
                new Vehicle("Renault", "Clio",2002)
            };
}

static void ReportVehicles(IEnumerable<Vehicle> vehicles)
{

    var query =
        (from v in vehicles
         group v by v into g
         select new { Vehicle = g.Key.GetLabel(), Count = g.Count() });

    foreach (var record in query)
        Console.WriteLine("{0,-17} x {1}", record.Vehicle, record.Count);
}
