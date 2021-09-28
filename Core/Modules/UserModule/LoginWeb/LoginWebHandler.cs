using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Modules.UserModule.LoginWeb
{
    public class LoginWebHandler : IRequestHandler<LoginWebQuery, SignInResult>
    {
        private readonly IUserRepository _userRepository;

        public LoginWebHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SignInResult> Handle(LoginWebQuery request, CancellationToken cancellationToken)
        {
            SignInResult login = await _userRepository.LoginAsync(request.UserName, request.Password, request.RememberMe);
            if (!login.Succeeded)
                throw new Exception("Error");

            return login;
        }
    }
}
