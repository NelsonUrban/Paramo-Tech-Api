using Sat.Recruitment.Domain;
using System.Collections.Generic;

namespace Sat.Recruitment.DataAccess.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public UserRepository()
        {

        }

        public User CreateUser(User user)
        {
            _users.Add(user);
            return user;
        }
    }
}
