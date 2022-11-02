using System;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business.Dtos;
using Xunit;
using Sat.Recruitment.Business.Services.Abstract;
using Sat.Recruitment.Business.Services.Concrete;
using Sat.Recruitment.DataAccess.Repositories;
using Sat.Recruitment.DataAccess.Repositories.Abstract;
using AutoMapper;
using Sat.Recruitment.Custom.Validations.Concrete;
using Sat.Recruitment.Business.Profiles;
using Sat.Recruitment.Custom.Validations.Abstract;
using Sat.Recruitment.DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTest
    {
        [Fact]
        public async void Test1()
        {
            var userDto = new UserDto
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "+349 1122354215",
                Money = Convert.ToDecimal("124")
            };

            IUserRepository IUserRepository =  new UserRepository();  
            
            var userProfile = new UserProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(userProfile));
            IMapper mapper = new Mapper(configuration);
            ICustomValidation customValidation = new CustomValidation();
            IFileRepository IFileRepository = new FileRepository();
            IUserService userService = 
                new UserService(IUserRepository, mapper, IFileRepository, customValidation); 

            var userController = new UsersController(userService);      
            var result = await userController.CreateUser(userDto);

            var valueResult = (result.Result as OkObjectResult).Value as UserDto;

            Assert.Equal(true, valueResult.IsSuccess);
            Assert.Equal("User Created", valueResult.Errors);
        }

        [Fact]
        public async void Test2()
        {
            var userDto = new UserDto
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = Convert.ToDecimal("124")
            };

            IUserRepository IUserRepository = new UserRepository();

            var userProfile = new UserProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(userProfile));
            IMapper mapper = new Mapper(configuration);
            ICustomValidation customValidation = new CustomValidation();
            IFileRepository IFileRepository = new FileRepository();
            IUserService userService =
                new UserService(IUserRepository, mapper, IFileRepository, customValidation);

            var userController = new UsersController(userService);
            var result = await userController.CreateUser(userDto);

            var valueResult = (result.Result as OkObjectResult).Value as UserDto;

            Assert.Equal(false, valueResult.IsSuccess);
            Assert.Equal("The user is duplicated", valueResult.Errors);
        }
    }
}
