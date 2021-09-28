using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Modules.UserModule.LoginWeb
{
    public class LoginWebQuery : IRequest<SignInResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
