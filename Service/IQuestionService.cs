using AlumniProject.Dto;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuestionService
    {
        Task<PagingResultDTO<Question>> GetAll(int pageNo, int pageSize, bool desdescending);
        Task<Question> UpdateQuestion(Question question);
        Task<bool> DeleteQuestionById(int id);
        Task<int> AddQuestion(Question Question);
        Task<Question> GetQuestionByID(int id);
    }
}
