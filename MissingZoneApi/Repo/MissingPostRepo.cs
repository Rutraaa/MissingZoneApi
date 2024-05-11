using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Dto.Admin;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class MissingPostRepo : IMissingPost
{
    public readonly mzonedbContext _mzonedbContext;

    public MissingPostRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }
}