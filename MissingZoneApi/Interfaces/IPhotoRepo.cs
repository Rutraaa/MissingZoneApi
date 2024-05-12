using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IPhotoRepo
{
    Task Create(Photo photo);
}