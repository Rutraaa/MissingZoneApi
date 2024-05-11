namespace MissingZoneApi.Contracts.MissingPost
{
    public class GetAllMissingPostsRequest : PageData
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? City { get; set; }
    }
}
