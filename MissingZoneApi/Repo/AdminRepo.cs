using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
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

    public async Task<AdminResponse> GetMe(string adminEmail)
    {
        try
        {
            var adminMe = await _mzonedbContext.Admins.FirstAsync(item => item.Email == adminEmail);
            return new AdminResponse
            {
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
}