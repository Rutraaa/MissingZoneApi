using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IVolunteer
{
    Task<VolunteerGetResponse> GetMe(string adminEmail);
    LoginResponse CheckIsExist(LoginRequest userLogin);
    Task Create(RegistrationRequest registration);
}