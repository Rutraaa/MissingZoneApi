using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces
{
    public interface IMissingPostsFilterService
    {
        IEnumerable<MissingPost> FilterMissingPosts(IEnumerable<MissingPost> posts, GetAllMissingPostsRequest request);
    }
}
