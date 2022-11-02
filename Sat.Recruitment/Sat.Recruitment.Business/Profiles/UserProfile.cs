using AutoMapper;
using Sat.Recruitment.Business.Dtos;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }

    }
}
