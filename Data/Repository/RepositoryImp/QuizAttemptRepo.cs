using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class QuizAttemptRepo : RepositoryBase<QuizAttempt>, IQuizAttemptRepo
    {
        public QuizAttemptRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
