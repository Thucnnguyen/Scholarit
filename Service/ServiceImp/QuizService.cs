using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;
using System.Linq;

namespace Scholarit.Service.ServiceImp
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepo _repo;
        private readonly IChapterService _chapterService;
        public QuizService(IQuizRepo quizRepo, IChapterService chapterService)
        {
            _repo = quizRepo;
            _chapterService = chapterService;
        }
        public async Task<int> AddQuiz(Quiz quiz)
        {
            var chapter = await _chapterService.GetChapterByID(quiz.ChapterId);

            var newId = await _repo.CreateAsync(quiz);
            return newId;
        }

        public async Task<bool> DeleteQuizById(int id)
        {
            var quiz = await GetQuizByID(id);
            quiz.IsDeleted = true;
            await _repo.UpdateAsync(quiz);
            return true;
        }

        public async Task<PagingResultDTO<Quiz>> GetAllByChapterId(int pageNo, int pageSize, bool desdescending, int chapterId)
        {
            var chapter = await _chapterService.GetChapterByID(chapterId);

            var quizList = await _repo.GetAllByConditionAsync(pageNo, pageSize, q => q.ChapterId == chapterId, q => q.Id, desdescending);
            return quizList;
        }

        public async Task<Quiz> GetQuizByID(int id)
        {
            var quiz = await _repo.FindOneByCondition(q => q.Id == id, q => q.QuizQuestions);
            return quiz != null ? quiz : throw new NotFoundException("Quiz not found with id: " + id);
        }

        public async Task<Quiz> UpdateQuestion(Quiz quiz)
        {
            var existedQuiz = await GetQuizByID(quiz.Id);

            existedQuiz.Name = quiz.Name;
            existedQuiz.Duration = quiz.Duration;
            existedQuiz.MaxScore = quiz.MaxScore;
            existedQuiz.NumberOfQuestion = quiz.NumberOfQuestion;
            existedQuiz.ChapterId = quiz.ChapterId;

            await _repo.UpdateAsync(existedQuiz);
            return existedQuiz;
        }
    }
}
