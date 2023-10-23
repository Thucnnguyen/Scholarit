using AlumniProject.Dto;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service
{
    public interface IQuestionService
    {
        Task<PagingResultDTO<Question>> GetAll(int pageNo, int pageSize, bool desdescending);
        Task<Question> UpdateQuestion(Question question);
        Task<bool> DeleteQuestionById(int id);
        Task<int> AddQuestion(Question Question);
        Task<bool> AddQuestion(List<Question> question);
        Task<Question> GetQuestionByID(int id);
        Task<PagingResultDTO< Question>> GetQuestionByChapterId(int pageNo, int pageSize, int chapterId);
        Task<Question> GetQuestionByChapterIdRandom( int chapterId);
        Task<QuestionAnwserCheckDTO> GetQuestionByChapterIdRandomCheck(QuestionAnwserDTO question);



    }
}
