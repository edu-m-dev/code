using System.Collections.Generic;

namespace TextFileChallenge
{
    public interface IUsersService
    {
        IReadOnlyCollection<UserModel> GetAllFromFile(string filePath);

        void SaveToFile(IEnumerable<UserModel> users, string filePath);
    }
}