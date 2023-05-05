using equality.bl;

IEnumerable<Vehicle> vehicles = LoadVehicles();

ReportVehicles(vehicles);

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
