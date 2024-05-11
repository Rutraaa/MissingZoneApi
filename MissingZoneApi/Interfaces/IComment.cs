﻿using MissingZoneApi.Contracts.Admin;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IComment
{
    Task Verify(int commentId);
}