using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TextFileChallenge
{
    public class UsersService : IUsersService
    {
        public IReadOnlyCollection<UserModel> GetAllFromFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<UserModel>().ToList();
            }
        }

        public void SaveToFile(IEnumerable<UserModel> users, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(users);
            }
        }
    }
}