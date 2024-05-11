using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class VolunteerRepo : IVolunteer
{
    public readonly mzonedbContext _mzonedbContext;

    public VolunteerRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }
    public async Task<VolunteerGetResponse> GetMe(string email)
    {
        try
        {
            Volunteer volunteerMe = await _mzonedbContext.Volunteers.FirstAsync(item => item.Email == email);
            return new VolunteerGetResponse
            {
                Email = volunteerMe.Email,
                FirstName = volunteerMe.FirstName,
                LastName = volunteerMe.LastName,
                Phone = volunteerMe.Phone,
            };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public LoginResponse CheckIsExist(LoginRequest userLogin)
    {
        try
        {
            bool isExist = _mzonedbContext.Volunteers
                .Any(item => item.Email == userLogin.Email);
            if (!isExist)
            {
                return new LoginResponse { IsExist = isExist, Messsage = string.Empty };
            }
            isExist = _mzonedbContext.Volunteers
                .Any(item => item.Password == userLogin.Password);
            if (!isExist)
            {
                return new LoginResponse { IsExist = isExist, Messsage = "Wrong password" };
            }

            return new LoginResponse { IsExist = isExist, Messsage = string.Empty }; ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Create(RegistrationRequest registration)
    {
        await _mzonedbContext.Volunteers.AddRangeAsync(new Volunteer
        {
            Email = registration.Email,
            Password = registration.Password,
            FirstName = registration.FirstName,
            LastName = registration.LastName,
            Photo = new byte[]
            {
            },
            IsVerified = false,
            OrganizationName = registration.OrganizationName,
            Phone = registration.Phone
        });

        await _mzonedbContext.SaveChangesAsync();
    }
}