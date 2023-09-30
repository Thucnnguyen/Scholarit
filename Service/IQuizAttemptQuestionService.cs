using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuizAttemptQuestionService
    {
        Task<IEnumerable<QuizAttemptQuestion>> GetAllBy(int quizAttemptId);
        Task<QuizAttemptQuestion> UpdateQuizAttempt(QuizAttemptQuestion quizAttemptQuestion);
        Task<bool> DeleteQuizAttemptQuestionById(int id);
        Task<int> AddQuizAttemptQuestion(QuizAttemptQuestion quizAttemptQuestion);
        Task<QuizAttemptQuestion> GetQuizAttemptQuestionByID(int id);
        //Task<QuizAttempt> GetQuizAttemptByUserIdAndQuizId(int UserId, int QuizId);
    }
}
