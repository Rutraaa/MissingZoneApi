namespace MissingZoneApi.Contracts.MissingPost;

public class MissingPostInfo
{
    public int MissingPostId { get; set; }
    public string Title { get; set; }

    public string Description { get; set; }

    public string ContactInfo { get; set; }

    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FatherName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public string City { get; set; }

    public string Coordinates { get; set; }

    public List<string> Photos { get; set; }
    public string PrePhoto { get; set; }
}