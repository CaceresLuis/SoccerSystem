﻿using System;
using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
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
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not registered",
                        Message = "Paswords not match",
                        Title = "Not registered",
                        State = State.error,
                        IsSuccess = false
                    });

            UserEntity user = _mapper.Map<UserEntity>(userdto);
            user.UserName = user.Email;

            var userExist = await _userRepository.FindByEmailAsync(user.Email);
            if (userExist != null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not registered",
                        Message = $"The Email: {user.Email} is already usage",
                        Title = "Not registered",
                        State = State.error,
                        IsSuccess = false
                    });
          
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