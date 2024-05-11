﻿namespace MissingZoneApi.Contracts.AuthReg;

public class RegistrationRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Photo { get; set; }
    public string Phone { get; set; }
    public string OrganizationName { get; set; }
}