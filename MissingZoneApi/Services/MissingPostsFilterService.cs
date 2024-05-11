using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Services
{
    public class MissingPostsFilterService : IMissingPostsFilterService
    {
        public IEnumerable<MissingPost> FilterMissingPosts(IEnumerable<MissingPost> posts, GetAllMissingPostsRequest request)
        {
            var filteredPosts = posts;

            if (request.BirthDate.HasValue)
            {
                var birthDate = request.BirthDate.Value.Date;
                filteredPosts = filteredPosts.Where(post =>
                    post.BirthDate.HasValue &&
                    post.BirthDate.Value.Date == birthDate);
            }

            if (!string.IsNullOrEmpty(request.FirstName))
                filteredPosts = filteredPosts.Where(post => post.FirstName.ToLower() == request.FirstName.ToLower());

            if (!string.IsNullOrEmpty(request.LastName))
                filteredPosts = filteredPosts.Where(post => post.LastName.ToLower() == request.LastName.ToLower());

            if (!string.IsNullOrEmpty(request.FatherName))
                filteredPosts = filteredPosts.Where(post => post.FatherName.ToLower() == request.FatherName.ToLower());

            if (!string.IsNullOrEmpty(request.City))
                filteredPosts = filteredPosts.Where(post => post.City.ToLower() == request.City.ToLower());

            return filteredPosts;
        }
    }
}
