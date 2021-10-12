using System;
using MediatR;
using AutoMapper;
using System.Threading;
using Core.Dtos.DtosApi;
using Core.Security.Token;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Core.Modules.UserModule.GetUserSession
{
    public class GetUserSessionHandler : IRequestHandler<GetUserSessionCommand, UserDtoApi>
    {
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public GetUserSessionHandler(IMapper mapper, IJwtGenerator jwtGenerator, IUserRepository userRepository, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }

        public async Task<UserDtoApi> Handle(GetUserSessionCommand request, CancellationToken cancellationToken)
        {
            string userSess = _userRepository.GetSessionUser();
            UserEntity user = await _userRepository.FindUserByName(userSess);
            Guid id = Guid.Parse(user.Id);
            var pic = await _imageRepository.GetImage(id);
            List<string> roles = await _userRepository.GetUserRolesAsync(user);
            UserDtoApi userDtoApi = _mapper.Map<UserDtoApi>(user);
            userDtoApi.Roles = roles;
            userDtoApi.Token = _jwtGenerator.CreateToken(user, roles);
            userDtoApi.PicturePath = pic.Path;

            return userDtoApi;
        }
    }
}
