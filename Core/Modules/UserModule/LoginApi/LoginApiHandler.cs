using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using System.Threading;
using Shared.Exceptions;
using Core.Dtos.DtosApi;
using Core.Security.Token;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Core.Modules.UserModule.LoginApi
{
    public class LoginApiHandler : IRequestHandler<LoginApiQuery, UserDtoApi>
    {
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerador;
        private readonly IUserRepository _userRepository;

        public LoginApiHandler(IJwtGenerator jwtGenerador, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _jwtGenerador = jwtGenerador;
            _userRepository = userRepository;
        }

        public async Task<UserDtoApi> Handle(LoginApiQuery request, CancellationToken cancellationToken)
        {
            LoginDto data = request.LoginDto;
            UserEntity user = await _userRepository.FindByEmailAsync(data.Username);
            if(user == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound,
                    new Error
                    {
                        Code = "NotFound",
                        Message = $"The user: {user.Email} not exist",
                        Title = "NotFound",
                        IsSuccess = false
                    });
            UserDtoApi userDto = _mapper.Map<UserDtoApi>(user);

            if (!await _userRepository.LoginApiAsync(user, data.Password))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "BadRequest",
                        Message = "Login Failed",
                        Title = "BadRequest",
                        IsSuccess = false
                    });
            List<string> roles = await _userRepository.GetUserRolesAsync(user);
            if(roles.Count > 0)
                userDto.Roles = roles;

            string token = _jwtGenerador.CreateToken(user, roles);
            userDto.Token = token;
            return userDto;
        }
    }
}
