using MissingZoneApi.Contracts.MissingPost;
using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IMissingPost
{
    Task Create(MissingPost missingPost);
    Task<List<MissingPost>> GetAll();
    Task<MissingPostInfo> Get(int id);
    Task<MissingPost> Read(int id);
    Task Delete(int id);
    Task<int?> GetIdByDate(DateTime createdDate);
    Task<List<MissingPostInfo>> InsertPhotos(List<MissingPost> list);
}