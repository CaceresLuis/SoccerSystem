using System;
using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Modules.UserModule.Add
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AddUserHandler(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _mapper = mapper; 
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userdto = request.UserDto;
            if (userdto.Password != userdto.PasswordConfirm)
                throw new Exception("Error");

            UserEntity user = _mapper.Map<UserEntity>(userdto);
            user.Email = user.UserName;

            UserEntity exist = await _userRepository.UserNameExist(user.UserName);
            if (exist != null)
                throw new Exception("Error");

            bool userEmailExist = await _userRepository.EmailExist(user.Email);
            if (userEmailExist)
                throw new Exception("Error");

            await _userRepository.AddUserAsync(user, userdto.PasswordConfirm);

            if (userdto.Roles != null)
            {
                foreach (string rol in userdto.Roles)
                {
                    IdentityRole roleExist = await _roleRepository.GetRole(rol);
                    if (roleExist != null)
                        await _userRepository.AddRoleToUser(user, roleExist.Name);
                }
                return true;
            }

            await _userRepository.AddRoleToUser(user, "user");

            return true;
        }
    }
}