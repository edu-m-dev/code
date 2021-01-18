using refactoring.bl.EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace refactoring.bl
{
    public class EFUserRepository : IUserRepository
    {
        private const string connectionString = @"Data Source=.\dev;Initial Catalog=refactoringChallenge;User ID=sa;Password=9ext945A%$";

        public void Add(UserModel user)
        {
            using (var db = new UserContext())
            {
                this.AddUsers(new List<UserModel> { user }, db);
            }
        }

        public void Add(IEnumerable<UserModel> users)
        {
            using (var db = new UserContext())
            {
                this.AddUsers(users, db);
            }
        }

        private void AddUsers(IEnumerable<UserModel> users, UserContext db)
        {
            db.SystemUsers.AddRange(users.Select(x => this.Map(x)));
            db.SaveChanges();
        }

        public void Delete(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<UserModel> entities)
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
            using (var db = new UserContext())
            {
                return db.SystemUsers
                    .ToList()
                    .ConvertAll(x => this.Map(x));
            }
        }

        public IReadOnlyCollection<UserModel> GetByFirstOrLastName(string name)
        {
            using (var db = new UserContext())
            {
                return db.SystemUsers
                    .Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name))
                    .ToList()
                    .ConvertAll(x => this.Map(x));
            }
        }

        public void Update(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<UserModel> entities)
        {
            throw new NotImplementedException();
        }

        private UserModel Map(SystemUser user)
        {
            return
                new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
        }

        private SystemUser Map(UserModel user)
        {
            return
                new SystemUser
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
        }
    }
}