using Infrastructure.Models;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IImageRepository
    {
        Task<bool> AddImage(string path, Guid reference);
        Task<bool> DeleteImage(ImageEntity imageEntity);
        Task<ImageEntity> GetImage(Guid reference);
    }
}
