using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Business.Dtos;
using Sat.Recruitment.Business.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{   

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]       
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var result = await  _userService.CreateUser(userDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
    
}
