using MissingZoneApi.Contracts;
using MissingZoneApi.Interfaces;

namespace MissingZoneApi.Services;

public class PaginationService<T> : IPaginationService<T>
{
    public async Task<PayloadResponse<T>> GetPagedDataAsync(IEnumerable<T> data, PageData pageData)
    {
        try
        {
            var totalCount = data.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageData.PageSize);

            var pagedData = data
                .Skip((pageData.PageNumber - 1) * pageData.PageSize)
                .Take(pageData.PageSize)
                .ToList();

            return new PayloadResponse<T>
            {
                TotalCount = totalCount,
                PageNumber = pageData.PageNumber,
                PageSize = pageData.PageSize,
                TotalPages = totalPages,
                Data = pagedData
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to paginate data: {ex.Message}", ex);
        }
    }
}