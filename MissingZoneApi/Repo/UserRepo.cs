using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Dto.Admin;
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
}