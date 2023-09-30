using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuizQuestionService
    {
        Task<IEnumerable<QuizQuestion>> GetAllBy(int quizQuestionId);
        Task<QuizQuestion> UpdateQuizQuestion(QuizQuestion quizQuestion);
        Task<bool> DeleteQuizQuestionById(int id);
        Task<int> AddQuizQuestion(QuizQuestion quizQuestion);
        Task<QuizQuestion> GetQuizQuestionByID(int id);
        //Task<List<QuizQuestion>> GetQuizQuestionBy(int id);
    }
}
