using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuizAttempService
    {
        Task<PagingResultDTO<QuizAttempt>> GetAllByUserId(int pageNo, int pageSize, bool desdescending, int userId);
        Task<QuizAttempt> UpdateQuizAttempt(QuizAttempt quizAttempt);
        Task<bool> DeleteQuizAttemptById(int id);
        Task<int> AddQuizAttempt(QuizAttempt quiz);
        Task<QuizAttempt> GetQuizAttemptByID(int id);
        Task<QuizAttempt> GetQuizAttemptByUserIdAndQuizId(int UserId, int QuizId);

    }
}
