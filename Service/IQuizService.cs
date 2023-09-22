using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuizService
    {
        Task<PagingResultDTO<Quiz>>GetAllByChapterId(int pageNo, int pageSize, bool desdescending, int chapterId);
        Task<Quiz> UpdateQuestion(Quiz quiz);
        Task<bool> DeleteQuizById(int id);
        Task<int> AddQuiz(Quiz quiz);
        Task<Quiz> GetQuizByID(int id);
    }
}
