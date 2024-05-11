using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo
{
    public class PhotoRepo : IPhotoRepo
    {
        public readonly mzonedbContext _mzonedbContext;
        public PhotoRepo(mzonedbContext mzonedbContext)
        {
            _mzonedbContext = mzonedbContext;
        }
        public async Task Create(Photo photo)
        {
            _mzonedbContext.Photos.Add(photo);
            await _mzonedbContext.SaveChangesAsync();
        }
    }
}
