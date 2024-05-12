using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts;
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

    public async Task<PayloadResponse<UserInfo>> GetAll(PageData pageData)
    {
        try
        {
            List<User> users = await _mzonedbContext.Users.ToListAsync();

            int totalCount = users.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageData.PageSize);

            var filteredList = users.Where(item => item.IsVerified == false)
                .Skip((pageData.PageNumber - 1) * pageData.PageSize)
                .Take(pageData.PageSize).Select(item => new UserInfo
                {
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Phone = item.Phone
                })
                .ToList();

            return new PayloadResponse<UserInfo>
            {
                TotalCount = totalCount,
                PageNumber = pageData.PageNumber,
                PageSize = pageData.PageSize,
                TotalPages = totalPages,
                Data = filteredList
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Verify(string email)
    {
        try
        {
            User user = await _mzonedbContext.Users.FirstAsync(item => item.Email == email);
            user.IsVerified = false;
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
            bool isExist = _mzonedbContext.Users
                .Any(item => item.Email == userLogin.Email);
            if (!isExist)
            {
                return new LoginResult { IsExist = isExist, Messsage = string.Empty };
            }
            isExist = _mzonedbContext.Users
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
            await _mzonedbContext.Users.AddRangeAsync(new User
            {
                Email = registration.Email,
                Password = registration.Password,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Photo = Encoding.UTF8.GetBytes(registration.Photo),
                IsVerified = false,
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