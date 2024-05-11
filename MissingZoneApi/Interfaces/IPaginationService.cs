using MissingZoneApi.Contracts;

namespace MissingZoneApi.Interfaces
{
    public interface IPaginationService<T>
    {
        Task<PayloadResponse<T>> GetPagedDataAsync(IEnumerable<T> data, PageData pageData);
    }
}
