using System;
using System.Collections.Generic;
using System.Linq;

namespace refactoring.bl
{
    public interface IUserRepository : IRepository<UserModel>
    {
        IReadOnlyCollection<UserModel> GetByFirstOrLastName(string name);
    }
}