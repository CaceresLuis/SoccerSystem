using MediatR;
using Shared.Helpers.Image;

namespace Core.Modules.ImageModule.Add
{
    public class AddImageCommad : IRequest<bool>
    {
        public ImageData ImageData { get; set; }
    }
}
