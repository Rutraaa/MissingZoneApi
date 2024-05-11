using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IAdmin
{
    Task<Admin> GetMe();
}