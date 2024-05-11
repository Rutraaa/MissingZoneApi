namespace MissingZoneApi.Contracts.MissingPost
{
    public class GetAllMissingPostsRequest : PageData
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
