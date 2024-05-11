using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class UserRepo : IUser
{
    public readonly mzonedbContext _mzonedbContext;

    public UserRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }

    public async Task<UserGetResponse> GetMe(string email)
    {
        try
        {
            User userMe = await _mzonedbContext.Users.FirstAsync(item => item.Email == email);
            return new UserGetResponse
            {
                Email = userMe.Email,
                FirstName = userMe.FirstName,
                LastName = userMe.LastName,
                Phone = userMe.Phone,
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
            bool isExist = _mzonedbContext.Users
                .Any(item => item.Email == userLogin.Email);
            if (!isExist)
            {
                return new LoginResponse { IsExist = isExist, Messsage = string.Empty };
            }
            isExist = _mzonedbContext.Users
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
        await _mzonedbContext.Users.AddRangeAsync(new User
        {
            Email = registration.Email,
            Password = registration.Password,
            FirstName = registration.FirstName,
            LastName = registration.LastName,
            Photo = new byte[]
            {
            },
            IsVerified = false,
            Phone = registration.Phone
        });

        await _mzonedbContext.SaveChangesAsync();
    }
}