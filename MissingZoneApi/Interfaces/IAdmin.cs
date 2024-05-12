using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IAdmin
{
    LoginResult CheckIsExist(LoginRequest userLogin);
}