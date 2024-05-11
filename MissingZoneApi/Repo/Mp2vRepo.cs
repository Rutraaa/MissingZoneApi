using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class Mp2vRepo : IMp2v
{
    public readonly mzonedbContext _mzonedbContext;

    public Mp2vRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }
}