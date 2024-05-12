using MissingZoneApi.Contracts.AuthReg;

namespace MissingZoneApi.Interfaces;

public interface IAdmin
{
    LoginResult CheckIsExist(LoginRequest userLogin);
}