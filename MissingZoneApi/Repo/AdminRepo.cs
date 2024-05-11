using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class AdminRepo : IAdmin
{
    public readonly mzonedbContext _mzonedbContext;

    public AdminRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }

    public async Task<AdminGetResponse> GetMe(string email)
    {
        try
        {
            Admin adminMe = await _mzonedbContext.Admins.FirstAsync(item => item.Email == email);
            return new AdminGetResponse
            {
                Email = adminMe.Email,
                FirstName = adminMe.FirstName,
                LastName = adminMe.LastName,
                OrganizationName = adminMe.OrganizationName,
            };

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }

    public LoginResult CheckIsExist(LoginRequest userLogin)
    {
        try
        {
            bool isExist = _mzonedbContext.Admins
                .Any(item => item.Email == userLogin.Email);
            if (!isExist)
            {
                return new LoginResult { IsExist = isExist, Messsage = string.Empty };
            }
            isExist = _mzonedbContext.Admins
                .Any(item => item.Password == userLogin.Password);
            if (!isExist)
            {
                return new LoginResult { IsExist = isExist, Messsage = "Wrong password" };
            }

            return new LoginResult { IsExist = isExist, Messsage = string.Empty}; ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}