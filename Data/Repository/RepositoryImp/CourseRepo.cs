using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class CourseRepo : RepositoryBase<Course>, ICourseRepo
    {
        public CourseRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
