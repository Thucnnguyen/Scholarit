using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Scholarit.Data.Repository;
using Scholarit.Data.Repository.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepo _repo;
        public ResourceService(IResourceRepo resourceRepo)
        {
            _repo = resourceRepo;
        }

        public Task<int> AddResource(Resource resource)
        {
            throw new NotImplementedException();

        }

        public Task<bool> DeleteResource(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResultDTO<Resource>> GetByChapterId(int chapterId, bool descending, int pageNo, int pageSize)
        {
            var resource = await _repo.GetAllByConditionAsync(pageNo, pageSize, r => r.ChapterId == chapterId, r => r.Id, descending); ;
            if (resource == null)
            {
                throw new NotFoundException("Resource with chapterId not found: "+ chapterId);
            }
            return resource;
        }

        public Task<Resource> UpdateResource(Resource resource)
        {
            throw new NotImplementedException();
        }
    }
}
