using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IAdmin
{
    Task<AdminGetResponse> GetMe(string adminEmail);
    LoginResponse CheckIsExist(LoginRequest userLogin);
}