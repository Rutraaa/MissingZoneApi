using Microsoft.EntityFrameworkCore;
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

    public async Task<Admin> GetMe()
    {

        try
        {
            var adminMe = await _mzonedbContext.Admins.FirstAsync();
            return adminMe;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }
}