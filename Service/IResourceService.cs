using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IResourceService
    {
        Task<PagingResultDTO<Resource>> GetByChapterId(int chapterId, bool descending, int pageNo, int pageSize);
        Task<Resource> UpdateResource(Resource resource);
        Task<bool> DeleteResource(int id);
        Task<int> AddResource(Resource resource);
    }
}
