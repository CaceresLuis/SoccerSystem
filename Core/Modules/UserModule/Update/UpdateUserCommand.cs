using MediatR;
using Core.Dtos;
using Shared.Helpers.Image;

namespace Core.Modules.UserModule.Update
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; set; }
        public string UserName { get; set; }
        public ImageData ImageData { get; set; }
    }
}
