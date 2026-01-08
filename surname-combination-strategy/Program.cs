var man = new Person("man_name", new string[] { "man_surname_1", "man_surname_2" });
var woman = new Person("woman_name", new string[] { "woman_surname_1", "woman_surname_2" });

static string WomanSurnameDropped(Person man, Person woman) => man.LastNames.First();
static string CombinedSurname(Person man, Person woman) => $"{man.LastNames.First()}-{woman.LastNames.First()}";
static string MultipleSurnames(Person man, Person woman) => string.Join(" ",
    man.LastNames.Zip(
    woman.LastNames,
    (x, y) => new[] { x, y })
    .SelectMany(x => x));

// print different strategies for descendant surnames
// man-first
Print(man, woman, WomanSurnameDropped);
Print(man, woman, CombinedSurname);
Print(man, woman, MultipleSurnames);
// woman-first
Print(woman, man, WomanSurnameDropped);
Print(woman, man, CombinedSurname);
Print(woman, man, MultipleSurnames);

static void Print(Person man, Person woman, Func<Person, Person, string> surnameStrategy)
{
    Console.WriteLine(surnameStrategy(man, woman));
}
