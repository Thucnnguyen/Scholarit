using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class QuestionRepo : RepositoryBase<Question>, IQuestionRepo
    {
        public QuestionRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
