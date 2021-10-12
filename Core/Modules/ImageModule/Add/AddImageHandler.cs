using MediatR;
using System.Threading;
using Shared.Helpers.Image;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.ImageModule.Add
{
    public class AddImageHandler : IRequestHandler<AddImageCommad, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly IImageRepository _imageRepository;

        public AddImageHandler(IIMageHelper iMageHelper, IImageRepository imageRepository)
        {
            _iMageHelper = iMageHelper;
            _imageRepository = imageRepository;
        }

        public async Task<bool> Handle(AddImageCommad request, CancellationToken cancellationToken)
        {
            var data = request.ImageData;
            string local = await _iMageHelper.UploadImageAsync(data.File, data.Folder);
            bool save =await _imageRepository.AddImage(local, data.Reference);
            return save;
        }
    }
}
