using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using Scholarit.Data.Repository;
using Scholarit.DTO;
using Scholarit.Entity;

namespace Scholarit.Service.ServiceImp
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepo _repo;
        private readonly IChapterService _chapterService;

        public QuestionService(IQuestionRepo repo, IChapterService chapter)
        {
            _repo = repo;
            _chapterService = chapter;
        }

        public async Task<int> AddQuestion(Question question)
        {
            var chapter = await _chapterService.GetChapterByID(question.ChapterId);

            var newQuestionId = await _repo.CreateAsync(question);
            return newQuestionId;
        }

        public async Task<bool> AddQuestion(List<Question> question)
        {

            await _repo.CreateRangeAsync(question);
            return true;
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

        public async Task<PagingResultDTO<Question>> GetQuestionByChapterId(int pageNo, int pageSize, int chapterId)
        {
            var questionPage = await _repo
                    .GetAllByConditionAsync(pageNo, pageSize, q => q.ChapterId == chapterId && !q.IsDeleted, q => q.DateCreated, true);
            return questionPage;
        }

        public async Task<Question> GetQuestionByChapterIdRandom(int chapterId)
        {
            var questions = await _repo
                    .GetAllByConditionAsync(q => q.ChapterId == chapterId && !q.IsDeleted, q => q.DateCreated, true);
            if (questions != null && questions.Any())
            {
                var random = new Random();
                var shuffledQuestions = questions.OrderBy(q => random.Next()).ToList();

                // Take the first question from the shuffled list
                var randomQuestion = shuffledQuestions.First();

                return randomQuestion;
            }
            else
            {
                throw new NotFoundException("Chapter not has any question");
            }
           
            return null;
        }

        public async Task<QuestionAnwserCheckDTO> GetQuestionByChapterIdRandomCheck(QuestionAnwserDTO questionDTO)
        {
            var question = await GetQuestionByID(questionDTO.QuestionId);
            return new QuestionAnwserCheckDTO()
            {
                QuestionId = questionDTO.QuestionId,
                Ansewer = question.Answer,
                UserAnswer = questionDTO.Answer,
                IsAnswer = question.Answer == questionDTO.Answer
            };
        }

        public async Task<Question> GetQuestionByID(int id)
        {
            var question = await _repo.FindOneByCondition(q => q.Id == id && !q.IsDeleted);
            return question != null ? question : throw new NotFoundException("Question not found with Id: " + id);
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
