using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;
using Shared.Helpers.Image;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.UserModule.Add;
using Core.Modules.UserModule.Get;
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
        private readonly IIMageHelper _iMageHelper;

        public AccountController(IMediator mediator, IIMageHelper iMageHelper)
        {
            _mediator = mediator;
            _iMageHelper = iMageHelper;
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
        public async Task<ActionResult<bool>> Register([FromForm] UserDto addUser)
        {
            bool add = await _mediator.Send(new AddUserCommand { UserDto = addUser });
            if (add && addUser.PictureFile != null)
            {
                UserDto user = await _mediator.Send(new GetUserQuery { Email = addUser.Email });
                user.PicturePath = await _iMageHelper.UploadImageAsync(addUser.PictureFile, "Users");
                await _mediator.Send(new UpdateUserCommand { UserDto = addUser });
            }
            return true;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update([FromForm] UserDto addUser)
        {
            return await _mediator.Send(new UpdateUserCommand { UserDto = addUser });
        }
    }
}
