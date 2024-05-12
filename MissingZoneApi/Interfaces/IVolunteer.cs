using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;

namespace MissingZoneApi.Interfaces;

public interface IVolunteer
{
    Task<VolunteerGetResponse> GetMe(string adminEmail);

    Task<PayloadResponse<VolunteerInfo>> GetAll(PageData pageData);
    LoginResult CheckIsExist(LoginRequest userLogin);
    Task Create(RegistrationRequest registration);
    Task Verify(string email);
}