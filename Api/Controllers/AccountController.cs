using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.UserModule.Add;
using Core.Modules.UserModule.Update;
using Core.Modules.UserModule.LoginApi;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.UserModule.GetUserSession;

namespace Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtoApi>> Login(LoginDto loginDto)
        {
            return await _mediator.Send(new LoginApiQuery { LoginDto = loginDto });
        }

        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<UserDtoApi>> UserSession()
        {
            return await _mediator.Send(new GetUserSessionCommand());
        }

        [HttpPost]
        public async Task<ActionResult<bool>>  Register([FromForm] UserDto addUser)
        {
            return await _mediator.Send(new AddUserCommand { UserDto = addUser });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromForm] UserDto addUser)
        {
            return await _mediator.Send(new UpdateUserCommand { UserDto = addUser });
        }
    }
}
