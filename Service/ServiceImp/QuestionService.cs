using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _repo;
        public QuestionService(IQuestionRepo repo)
        {
            _repo = repo;
        }

        public Task<int> AddQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteQuestionById(int id)
        {
            var question = await GetQuestionByID(id);
            question.IsDeleted = true;
            await _repo.UpdateAsync(question);
            return true;
        }

        public async Task<PagingResultDTO<Question>> GetAll(int pageNo, int pageSize, bool desdescending)
        {
            var questionList = await _repo.GetAllByConditionAsync(pageNo, pageSize, q => !q.IsDeleted, q => q.Id, desdescending);
            return questionList;
        }

        public async Task<Question> GetQuestionByID(int id)
        {
            var question = await _repo.FindOneByCondition(q =>q.Id == id && !q.IsDeleted);
            return question != null ? question : throw new NotFoundException("Qestion not found with Id: "+id);
        }

        public async Task<Question> UpdateQuestion(Question question)
        {
            var existedQuestion = await GetQuestionByID(question.Id);

            existedQuestion.Text = question.Text;
            existedQuestion.Type = question.Type;
            existedQuestion.Difficult = question.Difficult;
            existedQuestion.Option1 = question.Option1;
            existedQuestion.Option2 = question.Option2;
            existedQuestion.Option3 = question.Option3;
            existedQuestion.Option4 = question.Option4;
            existedQuestion.Option5 = question.Option5;
            existedQuestion.Answer = question.Answer;

            await _repo.UpdateAsync(existedQuestion);
            return existedQuestion;

        }
    }
}
