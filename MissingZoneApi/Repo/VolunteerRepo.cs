using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Dto.Admin;
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
}