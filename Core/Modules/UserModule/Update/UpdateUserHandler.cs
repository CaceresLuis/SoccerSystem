using MediatR;
using Core.Dtos;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Shared.Helpers.Image;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System;

namespace Core.Modules.UserModule.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public UpdateUserHandler(IUserRepository userRepository, IIMageHelper iMageHelper, IImageRepository imageRepository)
        {
            _iMageHelper = iMageHelper;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userDto = request.UserDto;
            UserEntity user = await _userRepository.FindByEmailAsync(userDto.Email);
            if (userDto.Email != user.Email)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "This profile not is your",
                        Title = "Fail",
                        State = State.error,
                        IsSuccess = false
                    });

            user.Address = userDto.Address ?? user.Address;
            user.Document = userDto.Document ?? user.Document;
            user.LastName = userDto.LastName ?? user.LastName;
            user.FirstName = userDto.FirstName ?? user.FirstName;

            bool update = await _userRepository.UpdateUserAsync(user);
            if (!update)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "This profile not is your",
                        Title = "Fail",
                        State = State.error,
                        IsSuccess = false
                    });

            if (userDto.PictureFile == null)
                return update;

            Guid id = Guid.Parse(user.Id);
            ImageEntity img = await _imageRepository.GetImage(id);
            if(img != null)
            {
                _iMageHelper.DeleteImage(img.Path);
                string newImg = await _iMageHelper.UploadImageAsync(userDto.PictureFile, "Users");
                img.Path = newImg;
                var upImg = await _imageRepository.UpdateImage(img);
                return upImg;
            }

            string local = await _iMageHelper.UploadImageAsync(userDto.PictureFile, "Users");
            bool save = await _imageRepository.AddImage(local, id);

            return save;
        }
    }
}
