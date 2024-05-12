using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.Admin;

public class VolunteerGetResponse
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}