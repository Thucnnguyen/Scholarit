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
        private readonly IChapterService _chapterService;
        public ResourceService(IResourceRepo resourceRepo, IChapterService chapterService)
        {
            _repo = resourceRepo;
            _chapterService = chapterService;
        }

        public async Task<int> AddResource(Resource resource)
        {
            var chapter = await _chapterService.GetChapterByID(resource.ChapterId);
            if (resource.ResourceParentId != null)
            {
                var parentReource = await _repo.FindOneByCondition(r => r.Id == resource.ResourceParentId && !r.IsDeleted);
                if(parentReource.ChapterId != resource.ChapterId)
                {
                    throw new ConflictException("child resource is not the same chapter with parent resouce");
                }
            }
            var newResourceId = await _repo.CreateAsync(resource);
            return newResourceId;
        }

        public async Task<bool> DeleteResource(int id)
        {
            var ExistingResource = await GetById(id);
            ExistingResource.IsDeleted = true;
            await _repo.UpdateAsync(ExistingResource);

            return true;
        }

        public async Task<IEnumerable<Resource>> GetByChapterId(int chapterId)
        {
            var resource = await _repo.GetAllByConditionAsync(r => r.ChapterId == chapterId, r => r.Id, false); ;
            if (resource == null)
            {
                throw new NotFoundException("Resource with chapterId not found: " + chapterId);
            }
            return resource;
        }

        public async Task<Resource> GetById(int Id)
        {
            var existingResource = await _repo.FindOneByCondition(rs => rs.Id == Id && !rs.IsDeleted);
            if (existingResource == null)
            {
                throw new NotFoundException("Resource with Id not found: " + Id);
            }
            return existingResource;
        }

        public async Task<Resource> UpdateResource(Resource resource)
        {
            var existingResource = await GetById(resource.Id);

            existingResource.Url = resource.Url;
            existingResource.Type = resource.Type;
            existingResource.ChapterId = resource.ChapterId;
            existingResource.ResourceParentId = resource.ResourceParentId;

            await _repo.UpdateAsync(existingResource);

            return existingResource;
        }
    }
}
