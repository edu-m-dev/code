var man = new Person("Eduardo", new string[] { "Martinez de Salinas", "Vazquez" });
var woman = new Person("Laetitia", new string[] { "Ponsar", "Treca" });

static string WomanSurnameDropped(Person man, Person woman) => man.LastNames.First();
static string CombinedSurname(Person man, Person woman) => $"{man.LastNames.First()}-{woman.LastNames.First()}";
static string MultipleSurnames(Person man, Person woman) => string.Join(" ",
    man.LastNames.Zip(
    woman.LastNames,
    (x, y) => new[] { x, y })
    .SelectMany(x=>x));

// print different strategies for descendant surnames
Print(man, woman, WomanSurnameDropped);
Print(man, woman, CombinedSurname);
Print(man, woman, MultipleSurnames);

static void Print(Person man, Person woman, Func<Person, Person, string> surnameStrategy)
{
    Console.WriteLine(surnameStrategy(man, woman));
}