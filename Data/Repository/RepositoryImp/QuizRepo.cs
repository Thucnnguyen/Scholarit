using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class QuizRepo : RepositoryBase<Quiz>, IQuizRepo
    {
        public QuizRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
