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

namespace Core.Modules.UserModule.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository, IIMageHelper iMageHelper)
        {
            _iMageHelper = iMageHelper;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UserDto userDto = request.UserDto;
            ImageData imgData = request.ImageData;
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
            //user.PicturePath = request.ImageData.Url ?? user.PicturePath;

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
            return update;
        }
    }
}
