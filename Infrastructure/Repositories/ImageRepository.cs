using System;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly DataContext _dataContext;

        public ImageRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> AddImage(string path, Guid reference)
        {
            _dataContext.Images.Add(new ImageEntity { Path = path, Reference = reference });
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<ImageEntity> GetImage(Guid reference)
        {
            return await _dataContext.Images.FirstOrDefaultAsync(r => r.Reference == reference);
        }

        public async Task<bool> DeleteImage(ImageEntity imageEntity)
        {
            _dataContext.Remove(imageEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        
        public async Task<bool> UpdateImage(ImageEntity imageEntity)
        {
            _dataContext.Update(imageEntity);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
