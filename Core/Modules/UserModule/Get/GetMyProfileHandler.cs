using System;
using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Core.Modules.UserModule.Get
{
    public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public GetMyProfileHandler(IUserRepository userRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }

        public async Task<UserDto> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            UserEntity user = await _userRepository.GetUserInSesscion(request.UserName);
            Guid id = Guid.Parse(user.Id);
            string pic = "";
            ImageEntity getPic = await _imageRepository.GetImage(id);
            if(getPic != null)
                pic = getPic.Path;

            List<string> roles = await _userRepository.GetUserRolesAsync(user);
            UserDto userDto = _mapper.Map<UserDto>(user);
            userDto.Roles = roles;
            userDto.PicturePath = pic;
            return userDto;
        }
    }
}
