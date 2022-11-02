using Sat.Recruitment.Business.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto userDto);
    }
}
