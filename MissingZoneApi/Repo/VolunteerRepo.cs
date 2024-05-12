using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Contracts.AuthReg;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;
using System.Text;

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

    public async Task<PayloadResponse<VolunteerInfo>> GetAll(PageData pageData)
    {
        List<Volunteer> volunteers = await _mzonedbContext.Volunteers.ToListAsync();

        int totalCount = volunteers.Count();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageData.PageSize);

        var filteredList = volunteers.Where(item => item.IsVerified == false)
            .Skip((pageData.PageNumber - 1) * pageData.PageSize)
            .Take(pageData.PageSize).Select(item => new VolunteerInfo
            {
                Email = item.Email,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Phone = item.Phone,
                OrganizationName = item.OrganizationName
            })
            .ToList();

        return new PayloadResponse<VolunteerInfo>
        {
            TotalCount = totalCount,
            PageNumber = pageData.PageNumber,
            PageSize = pageData.PageSize,
            TotalPages = totalPages,
            Data = filteredList
        };
    }

    public async Task Verify(string email)
    {
        try
        {
            Volunteer volunteer = await _mzonedbContext.Volunteers.FirstAsync(item => item.Email == email);
            volunteer.IsVerified = true;
            await _mzonedbContext.SaveChangesAsync();

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
            bool isExist = _mzonedbContext.Volunteers
                .Any(item => item.Email == userLogin.Email);
            if (!isExist)
            {
                return new LoginResult { IsExist = isExist, Messsage = string.Empty };
            }
            isExist = _mzonedbContext.Volunteers
                .Any(item => item.Password == userLogin.Password);
            if (!isExist)
            {
                return new LoginResult { IsExist = isExist, Messsage = "Wrong password" };
            }

            return new LoginResult { IsExist = isExist, Messsage = string.Empty }; ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Create(RegistrationRequest registration)
    {
        try
        {
            await _mzonedbContext.Volunteers.AddRangeAsync(new Volunteer
            {
                Email = registration.Email,
                Password = registration.Password,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Photo = Encoding.UTF8.GetBytes(registration.Photo),
                IsVerified = false,
                OrganizationName = registration.OrganizationName,
                Phone = registration.Phone
            });

            await _mzonedbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}