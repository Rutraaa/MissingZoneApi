using MissingZoneApi.Entities;

namespace MissingZoneApi.Interfaces;

public interface IMissingPost
{
    Task Create(MissingPost missingPost);
    Task<List<MissingPost>> GetAll();
    Task<MissingPost> Read(int id);
    Task Delete(int id);
}