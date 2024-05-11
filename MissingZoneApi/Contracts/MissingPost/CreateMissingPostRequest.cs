namespace MissingZoneApi.Contracts.MissingPost
{
    public class CreateMissingPostRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Photos { get; set; }
        public List<string> Contents { get; set; }
        public string ContactInfo { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string City { get; set; }
    }
}
