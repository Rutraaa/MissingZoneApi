namespace MissingZoneApi.Contracts;

public class PageData
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? City { get; set; }
}