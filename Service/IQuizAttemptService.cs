using AlumniProject.Dto;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuizAttemptService
    {
        Task<PagingResultDTO<QuizAttempt>> GetAllByUserId(int pageNo, int pageSize, bool desdescending, int userId);
        Task<QuizAttempt> UpdateQuizAttempt(QuizAttempt quizAttempt);
        Task<bool> DeleteQuizAttemptById(int id);
        Task<QuizAttempDTO> AddQuizAttempt(QuizAttemptAddOrUpdateDTO quiz,int userId);
        Task<QuizAttempt> GetQuizAttemptByID(int id);
        Task<QuizAttempDTO> GetQuizAttemptByUserIdAndQuizId(int UserId, int QuizId);

    }
}
