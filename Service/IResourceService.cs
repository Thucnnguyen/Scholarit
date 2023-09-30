using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IResourceService
    {
        Task<IEnumerable<Resource>> GetByChapterId(int chapterId);
        Task<Resource> GetById(int Id);
        Task<Resource> UpdateResource(Resource resource);
        Task<bool> DeleteResource(int id);
        Task<int> AddResource(Resource resource);
    }
}
