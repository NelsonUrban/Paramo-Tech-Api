using Sat.Recruitment.Domain;

namespace Sat.Recruitment.DataAccess.Repositories
{
    public interface IUserRepository
    {
        User CreateUser(User user);

    }
}
