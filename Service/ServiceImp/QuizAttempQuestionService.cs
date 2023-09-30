using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class QuizAttempQuestionService : IQuizAttemptQuestionService
    {
        private readonly IQuizAttemptQuestionRepo _repo;
        private readonly IQuizAttemptService _quizAttempService;
        private readonly IQuestionService _questionService;
        public QuizAttempQuestionService(IQuizAttemptQuestionRepo quizAttemptQuestionRepo, IQuizAttemptService quizAttemptService, IQuestionService questionService)
        {
            _repo = quizAttemptQuestionRepo;
            _quizAttempService = quizAttemptService;
            _questionService = questionService;
        }
        public async Task<int> AddQuizAttemptQuestion(QuizAttemptQuestion quizAttemptQuestion)
        {
            var newQuizAttemptQuestion = await _repo.CreateAsync(quizAttemptQuestion);
            return newQuizAttemptQuestion;
        }

        public async Task<bool> DeleteQuizAttemptQuestionById(int id)
        {
            var existingQuizAtemptQuestion = await GetQuizAttemptQuestionByID(id);
            existingQuizAtemptQuestion.IsDeleted = true;
            await _repo.UpdateAsync(existingQuizAtemptQuestion);
            return true;
        }

        public async Task<IEnumerable<QuizAttemptQuestion>> GetAllBy(int quizAttemptId)
        {
            var quizAttemptQuestionList = await _repo.GetAllByConditionAsync(qa => qa.QuizAttemptId == quizAttemptId && !qa.IsDeleted, qa => qa.Id, false);
            return quizAttemptQuestionList;

        }

        public async Task<QuizAttemptQuestion> GetQuizAttemptQuestionByID(int id)
        {
            var quizAtemptQuestion = await _repo.FindOneByCondition(
                qaq => qaq.Id == id && !qaq.IsDeleted);
            return quizAtemptQuestion != null ? quizAtemptQuestion : throw new NotFoundException("QuizAttemptQuestion not found with id: " + id);
        }

        public async Task<QuizAttemptQuestion> UpdateQuizAttempt(QuizAttemptQuestion quizAttemptQuestion)
        {
            var existingQuizAttempt = await _repo.FindOneByCondition(qaq => qaq.QuizAttemptId == quizAttemptQuestion.QuizAttemptId && qaq.QuestionId == quizAttemptQuestion.QuestionId && !quizAttemptQuestion.IsDeleted);

            existingQuizAttempt.Answer = quizAttemptQuestion.Answer;
            var question = await _questionService.GetQuestionByID(quizAttemptQuestion.QuestionId);

            if (existingQuizAttempt.Answer == question.Answer)
            {
                existingQuizAttempt.IsCorrect = true;
            }
            else
            {
                existingQuizAttempt.IsCorrect = true;
            }

            await _repo.UpdateAsync(existingQuizAttempt);

            return existingQuizAttempt;
        }
    }
}
