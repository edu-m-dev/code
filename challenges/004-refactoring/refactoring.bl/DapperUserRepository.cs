using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace refactoring.bl
{
    public class DapperUserRepository : IUserRepository
    {
        private const string connectionString = @"Data Source=.\dev;Initial Catalog=refactoringChallenge;User ID=sa;Password=9ext945A%$";

        public void Add(UserModel user)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                this.AddUsers(new List<UserModel> { user }, cnn);
            }
        }

        public void Add(IEnumerable<UserModel> users)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                this.AddUsers(users, cnn);
            }
        }

        private void AddUsers(IEnumerable<UserModel> users, IDbConnection cnn)
        {
            var userEntities = users.Select(x => this.Map(x));
            var returned = cnn.Insert(userEntities);
        }

        public void Delete(UserModel user)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<UserModel> users)
        {
            throw new NotImplementedException();
        }

        public UserModel FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserModel> Get()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<UserModel> Get(Func<UserModel, bool> where)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<UserModel> GetAll()
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                return cnn.GetAll<UserEntity>()
                    .Select(x => this.Map(x))
                    .ToList();
            }
        }

        public void Update(UserModel user)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<UserModel> users)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<UserModel> GetByFirstOrLastName(string name)
        {
            using (IDbConnection cnn = new SqlConnection(connectionString))
            {
                var encodedName = "%" + name.Replace("[", "[[]").Replace("%", "[%]") + "%";
                return
                    cnn.Query<UserEntity>(
                    @"select * from SystemUser
                    where firstName like @encodedName or lastName like @encodedName",
                    new { encodedName })
                    .Select(x => this.Map(x))
                    .ToList();
            }
        }

        private UserModel Map(UserEntity user)
        {
            return
                new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
        }

        private UserEntity Map(UserModel user)
        {
            return
                new UserEntity
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
        }
    }
}