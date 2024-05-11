using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IUser
{
    Task<UserGetResponse> GetMe(string adminEmail); 
    Task<PayloadResponse<UserInfo>> GetAll(PageData pageData);
    LoginResult CheckIsExist(LoginRequest userLogin);
    Task Create(RegistrationRequest registration);
    Task Verify(string email);
}