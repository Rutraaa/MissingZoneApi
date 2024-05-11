using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Dto.Admin;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Repo;

public class CommentRepo : IComment
{
    public readonly mzonedbContext _mzonedbContext;

    public CommentRepo(mzonedbContext mzonedbContext)
    {
        _mzonedbContext = mzonedbContext;
    }
}