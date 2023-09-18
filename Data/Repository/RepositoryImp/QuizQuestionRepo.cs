using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class QuizQuestionRepo : RepositoryBase<QuizQuestion>, IQuizQuestionRepo
    {
        public QuizQuestionRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
