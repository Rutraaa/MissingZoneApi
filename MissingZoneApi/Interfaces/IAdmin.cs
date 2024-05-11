using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IAdmin
{
    Task<AdminResponse> GetMe(string adminEmail);
}