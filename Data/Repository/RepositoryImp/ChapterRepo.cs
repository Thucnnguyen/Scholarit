using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class ChapterRepo : RepositoryBase<Chapter>, IChapterRepo
    {
        public ChapterRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
