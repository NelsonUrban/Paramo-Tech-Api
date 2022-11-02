using AutoMapper;
using Sat.Recruitment.Business.Dtos;
using Sat.Recruitment.Business.Services.Abstract;
using Sat.Recruitment.Custom.Validations;
using Sat.Recruitment.Custom.Validations.Abstract;
using Sat.Recruitment.Custom.Validations.Concrete;
using Sat.Recruitment.Custom.Validatiors;
using Sat.Recruitment.DataAccess.Repositories;
using Sat.Recruitment.DataAccess.Repositories.Abstract;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository; 
        private readonly ICustomValidation _customValidation;

        public UserService(IUserRepository userRepository, IMapper mapper, IFileRepository fileRepository, ICustomValidation customValidation)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;  
            _customValidation = customValidation;
        }

        public async Task<UserDto> CreateUser(UserDto userDto)
        {            
            var userValidator = new CreateUserValidator();
            var validResult =  await userValidator.ValidateAsync(userDto);

            if (!validResult.IsValid)
            {
                return new UserDto()
                {
                    IsSuccess = false,
                    Errors = validResult.Errors.First().ErrorMessage.ToString(),
                };

            }
            var user = _mapper.Map<User>(userDto);

            var normailizeEmail = new NormailizeEmail();              

            user.Money = _customValidation.UserMoneyValidation(user.UserType, user.Money); ;

            user.Email = normailizeEmail.EmailNormalized(user.Email);

            var duplicateResult = await DuplicateValidation(user);

            if (duplicateResult.Errors == null)
            {

                var result = _userRepository.CreateUser(user);
                var newDto = _mapper.Map<UserDto>(result);
                newDto.Errors = Message.UserMessage.Created;
                newDto.IsSuccess = true;
                return newDto;
            }
            else
            {
                return new UserDto()
                {
                    IsSuccess = false,
                    Errors = Message.UserMessage.Duplicated,
                };

            }
           
        }

        private async Task<IEnumerable<UserDto>> GetAllasync()
        {
            var result = new List<UserDto>();          

            using (var reader = _fileRepository.ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = (line.Split(',')[4].ToString()),
                        Money = Convert.ToDecimal(line.Split(',')[5]),
                    };

                    result.Add(_mapper.Map<UserDto>(user));              
               
                }
                reader.Close();          

                return result;
            }
        }
        private async Task<UserDto> DuplicateValidation(User user)
        {
            var resultUsers = await GetAllasync();

            var _users = _mapper.Map<List<User>>(resultUsers);

            var userValidators = new UserValidator(_users);

            var resultValidation = userValidators.IsDuplicated(user);

            if (resultValidation)
            {

                return new UserDto()
                {
                    IsSuccess = false,
                    Errors = Message.UserMessage.Duplicated,
                };

            }

            return _mapper.Map<UserDto>(user);

        }
    }
}
