using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly IQuizQuestionRepo _repo;
        private readonly IQuizService _quizService;
        private readonly IQuestionService _quizQuestionService;

        public QuizQuestionService(IQuizQuestionRepo repo, IQuizService quizService, IQuestionService quizQuestionService)
        {
            _repo = repo;
            _quizService = quizService;
            _quizQuestionService = quizQuestionService;
        }

        public async Task<int> AddQuizQuestion(QuizQuestion quizQuestion)
        {
            var newQuizQuestion = await _repo.CreateAsync(quizQuestion);
            return newQuizQuestion;
        }

        public async Task<bool> DeleteQuizQuestionById(int id)
        {
            var existingQuizQuestion = await GetQuizQuestionByID(id);
            existingQuizQuestion.IsDeleted = true;
            await _repo.UpdateAsync(existingQuizQuestion);
            return true;
        }

        public async Task<bool> DeleteQuizQuestionByQuizId(int quizId)
        {
            var quizQuestionList = await _repo.GetAllByConditionAsync(qq => qq.QuizId == quizId,qq =>qq.Order, false);
            foreach(var quizQuestion in quizQuestionList)
            {
                await _repo.DeleteByIdAsync(quizQuestion.Id);
            }
            return true;
        }

        public async Task<IEnumerable<QuizQuestion>> GetAllBy(int quizQuestionId)
        {
            var quizQuestionList = await _repo.GetAllByConditionAsync(qq => qq.QuizId == quizQuestionId && !qq.IsDeleted, qq => qq.Order, false);
            return quizQuestionList;
        }

        public async Task<QuizQuestion> GetQuizQuestionByID(int id)
        {
            QuizQuestion quizQuestion = await _repo.FindOneByCondition(qq => qq.Id == id && !qq.IsDeleted);
            return quizQuestion != null ? quizQuestion : throw new NotFoundException("QuizQuestion not found with id: " + id);
        }

        public async Task<QuizQuestion> UpdateQuizQuestion(QuizQuestion quizQuestion)
        {
            var existingQuizQuestion = await GetQuizQuestionByID(quizQuestion.Id);
            existingQuizQuestion.Order = quizQuestion.Order;

            await _repo.UpdateAsync(existingQuizQuestion);
            return existingQuizQuestion;
        }
    }
}
