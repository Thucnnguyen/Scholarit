using AlumniProject.Dto;
using AlumniProject.ExceptionHandler;
using AutoMapper;
using Scholarit.Data.Repository;
using Scholarit.DTO;
using Scholarit.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Scholarit.Service.ServiceImp
{
    public class QuizAttempService : IQuizAttemptService
    {
        private readonly IQuizAttemptRepo _repo;
        private readonly IMapper _mapper;
        private readonly IQuizService quizService;
        private readonly IUserService userService;
        private readonly IQuizQuestionService quizQuestionService;
        private readonly IQuestionService questionService;
        private readonly IQuizAttemptQuestionService quizAttemptQuestionService;


        public QuizAttempService(IQuizAttemptRepo repo, IQuizService quizService, IUserService userService, IQuestionService questionService, IQuizAttemptQuestionService quizAttemptQuestionService, IMapper mapper)
        {
            _repo = repo;
            this.quizService = quizService;
            this.userService = userService;
            this.questionService = questionService;
            this.quizAttemptQuestionService = quizAttemptQuestionService;
            this._mapper = mapper;
        }

        public async Task<QuizAttempDTO> AddQuizAttempt(QuizAttemptAddOrUpdateDTO quizAttempt, int userId)
        {
            var quizAttempDTO = new QuizAttempDTO();
            var existingQuizAttempt = await _repo.FindOneByCondition(q => q.UserId == userId && q.QuizId == quizAttempt.QuizId && !q.IsDeleted);
            if (existingQuizAttempt != null)
            {
                quizAttempDTO.QuizId = existingQuizAttempt.QuizId;
                quizAttempDTO.UserId = userId;
                quizAttempDTO.Attempt = existingQuizAttempt.Attempt + 1;
                quizAttempDTO.LastAttempt = DateTime.Now;
                var score = 0;
                foreach (var q in quizAttempt.Questions)
                {
                    var question = await questionService.GetQuestionByID(q.QuestionId);
                    if (question != null)
                    {
                        if (question.Answer == q.Answer)
                        {
                            score += 10;
                            var quizAttempQuestion = new QuizAttemptQuestion()
                            {
                                Answer = q.Answer,
                                IsCorrect = true,
                                UserId = userId,
                                QuizAttemptId = existingQuizAttempt.Id,
                                QuestionId = q.QuestionId
                            };
                            await quizAttemptQuestionService.UpdateQuizAttempt(quizAttempQuestion);
                            quizAttempDTO.QuizAttempQuestions.Add(_mapper.Map<QuizAttemptQuestionDTO>(quizAttempQuestion));
                        }
                        else
                        {
                            var quizAttempQuestion = new QuizAttemptQuestion()
                            {
                                Answer = q.Answer,
                                IsCorrect = false,
                                UserId = userId,
                                QuizAttemptId = existingQuizAttempt.Id,
                                QuestionId = q.QuestionId
                            };
                            await quizAttemptQuestionService.UpdateQuizAttempt(quizAttempQuestion);
                            quizAttempDTO.QuizAttempQuestions.Add(_mapper.Map<QuizAttemptQuestionDTO>(quizAttempQuestion));

                        }
                    }
                }

                existingQuizAttempt.Score = score;
                existingQuizAttempt.LastAttempt = DateTime.Now;
                quizAttempDTO.Score = score;
                var updateQa = await UpdateQuizAttempt(existingQuizAttempt);
                return quizAttempDTO;
            }
            else
            {
                var newQuizAttemp = new QuizAttempt()
                {
                    QuizId = quizAttempt.QuizId,
                    UserId = userId,
                    Attempt = 1,
                    Score = 0,
                };
                var newQuizAttempId = await _repo.CreateAsync(newQuizAttemp);
                quizAttempDTO.QuizId = quizAttempt.QuizId;
                quizAttempDTO.UserId = userId;
                quizAttempDTO.Attempt = 1;
                var score = 0;
                foreach (var q in quizAttempt.Questions)
                {
                    var question = await questionService.GetQuestionByID(q.QuestionId);
                    if (question != null)
                    {
                        if (question.Answer == q.Answer)
                        {
                            score += 10;
                            var quizAttempQuestion = new QuizAttemptQuestion()
                            {
                                Answer = q.Answer,
                                IsCorrect = true,
                                UserId = userId,
                                QuizAttemptId = newQuizAttempId,
                                QuestionId = q.QuestionId
                            };
                            await quizAttemptQuestionService.AddQuizAttemptQuestion(quizAttempQuestion);
                            quizAttempDTO.QuizAttempQuestions.Add(_mapper.Map<QuizAttemptQuestionDTO>(quizAttempQuestion));
                        }
                        else
                        {
                            var quizAttempQuestion = new QuizAttemptQuestion()
                            {
                                Answer = q.Answer,
                                IsCorrect = false,
                                UserId = userId,
                                QuizAttemptId = newQuizAttempId,
                                QuestionId = q.QuestionId
                            };
                            await quizAttemptQuestionService.AddQuizAttemptQuestion(quizAttempQuestion);
                            quizAttempDTO.QuizAttempQuestions.Add(_mapper.Map<QuizAttemptQuestionDTO>(quizAttempQuestion));

                        }
                    }
                }
                newQuizAttemp.Id = newQuizAttempId;
                quizAttempDTO.Score = score;
                quizAttempDTO.LastAttempt = DateTime.Now;
                await _repo.UpdateAsync(newQuizAttemp);
                return quizAttempDTO;
            }
        }

        public async Task<bool> DeleteQuizAttemptById(int id)
        {
            var quizAttempt = GetQuizAttemptByID(id);
            await _repo.DeleteByIdAsync(id);
            return true;
        }

        public async Task<PagingResultDTO<QuizAttempt>> GetAllByUserId(int pageNo, int pageSize, bool desdescending, int userId)
        {
            var quizAtemptList = await _repo.GetAllByConditionAsync(pageNo, pageSize, qa => qa.UserId == userId && !qa.IsDeleted, qa => qa.Id, desdescending);
            return quizAtemptList;
        }

        public async Task<QuizAttempt> GetQuizAttemptByID(int id)
        {
            var quiz = await _repo.FindOneByCondition(q => q.Id == id && !q.IsDeleted);
            return quiz != null ? quiz : throw new NotFoundException("QuizAtempt Not Found Wtih id: " + id);
        }

        public async Task<QuizAttempDTO> GetQuizAttemptByUserIdAndQuizId(int UserId, int QuizId)
        {
            var quizAttempt = await _repo.FindOneByCondition(q => q.UserId == UserId && q.QuizId == QuizId && !q.IsDeleted, q => q.QuizAttempQuestions);
            if (quizAttempt != null)
            {
                var newQuizAttemptDTO = new QuizAttempDTO()
                {
                    UserId = UserId,
                    QuizId = QuizId,
                    Attempt = quizAttempt.Attempt,
                    LastAttempt = quizAttempt.LastAttempt,
                    Score = quizAttempt.Score,
                };
                foreach (var qaq in quizAttempt.QuizAttempQuestions)
                {
                    var quizAttempQuestion = new QuizAttemptQuestion()
                    {
                        Answer = qaq.Answer,
                        IsCorrect = qaq.IsCorrect,
                        UserId = qaq.UserId,
                        QuizAttemptId = qaq.QuizAttemptId,
                        QuestionId = qaq.QuestionId,
                    };
                    newQuizAttemptDTO.QuizAttempQuestions.Add(_mapper.Map<QuizAttemptQuestionDTO>(quizAttempQuestion));
                }
                return newQuizAttemptDTO;
            }
            throw new NotFoundException("QuizAtempt Not Found Wtih Userid or QuizId.");
        }
        public async Task<QuizAttempt> UpdateQuizAttempt(QuizAttempt quizAttempt)
        {
            var existingQuizAttempt = await GetQuizAttemptByID(quizAttempt.Id);

            existingQuizAttempt.Attempt = quizAttempt.Attempt;
            existingQuizAttempt.Score = quizAttempt.Score;
            existingQuizAttempt.LastAttempt = quizAttempt.LastAttempt;

            await _repo.UpdateAsync(quizAttempt);

            return existingQuizAttempt;
        }
    }
}
