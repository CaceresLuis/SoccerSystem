using Core.Dtos.DtosApi;
using Core.Security.Token;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Modules.UserModule.LoginApi
{
    public class LoginApiHandler : IRequestHandler<LoginApiQuery, UserDtoApi>
    {
        private readonly IJwtGenerator _jwtGenerador;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public LoginApiHandler(IJwtGenerator jwtGenerador, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _jwtGenerador = jwtGenerador;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<UserDtoApi> Handle(LoginApiQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
