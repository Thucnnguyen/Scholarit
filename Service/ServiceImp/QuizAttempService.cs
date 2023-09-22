using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class QuizAttempService : IQuizAttempService
    {
        private readonly IQuizAttemptRepo _repo;
        private readonly IQuizService quizService;
        private readonly IUserService userService;


        public QuizAttempService(IQuizAttemptRepo repo, IQuizService quizService, IUserService userService)
        {
            _repo = repo;
            this.quizService = quizService;
            this.userService = userService;
        }

        public async Task<int> AddQuizAttempt(QuizAttempt quizAttempt)
        {
            var existingQuiz = await _repo.FindOneByCondition(q => q.UserId == quizAttempt.UserId && q.QuizId == quizAttempt.QuizId && !q.IsDeleted);
            if(existingQuiz != null)
            {
                existingQuiz.Attempt+=1;
                existingQuiz.Score = quizAttempt.Score;
                existingQuiz.LastAttempt = DateTime.Now;

                var updateQa =  await UpdateQuizAttempt(existingQuiz);
                return updateQa.Id;
            }
            else
            {
                var newId = await _repo.CreateAsync(quizAttempt);
                return newId;
            }
        }

        public Task<bool> DeleteQuizAttemptById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResultDTO<QuizAttempt>> GetAllByUserId(int pageNo, int pageSize, bool desdescending, int userId)
        {
            var quizAtemptList = await _repo.GetAllByConditionAsync(pageNo, pageSize, qa => qa.UserId == userId && !qa.IsDeleted, qa => qa.Id, desdescending);
            return quizAtemptList;
        }

        public async Task<QuizAttempt> GetQuizAttemptByID(int id)
        {
            var quiz = await _repo.FindOneByCondition(q => q.Id == id && !q.IsDeleted);
            return quiz != null ? quiz : throw new NotFoundException("QuizAtempt Not Found Wtih id: " + id);
        }

        public async Task<QuizAttempt> GetQuizAttemptByUserIdAndQuizId(int UserId, int QuizId)
        {
            var quiz = await _repo.FindOneByCondition(q => q.UserId == UserId && q.QuizId == QuizId && !q.IsDeleted);
            return quiz != null ? quiz : throw new NotFoundException("QuizAtempt Not Found Wtih Userid or QuizId.");
        }
        public async Task<QuizAttempt> UpdateQuizAttempt(QuizAttempt quizAttempt)
        {
            var existingQuizAttempt = await GetQuizAttemptByID(quizAttempt.Id);

            existingQuizAttempt.Attempt = quizAttempt.Attempt;
            existingQuizAttempt.Score = quizAttempt.Score;
            existingQuizAttempt.LastAttempt = quizAttempt.LastAttempt;

            await _repo.UpdateAsync(quizAttempt);

            return existingQuizAttempt;
        }
    }
}
