using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Scholarit.DTO;
using Scholarit.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Scholarit.Utils
{
    
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RegisterUserDTO,Users>().ReverseMap();
            CreateMap<UserUpdateDTO, Users>().ReverseMap();
            CreateMap<UserDto, Users>().ReverseMap();
            CreateMap<CategoryAddDTO, Category>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryUpdateDTO, Category>().ReverseMap();
            CreateMap<CourseAddDTO, Course>().ReverseMap();
            CreateMap<CourseDTO, Course>().ReverseMap();
            CreateMap<CourseUpdateDTO, Course>().ReverseMap();
            CreateMap<ChapterAddDTO, Chapter>().ReverseMap();
            CreateMap<ChapterDTO, Chapter>().ReverseMap();
            CreateMap<ChapterUpdateDTO, Chapter>().ReverseMap();
            CreateMap<ResourceDTO, Resource>().ReverseMap();
            CreateMap<ResourceAddDTO, Resource>().ReverseMap();
            CreateMap<ResourceUpdateDTO, Resource>().ReverseMap();
            CreateMap<QuestionAddDTO, Question>().ReverseMap();
            CreateMap<QuestionForAdminDTO, Question>().ReverseMap();
            CreateMap<QuestionUpdateDTO, Question>().ReverseMap();
            CreateMap<QuestionDTO, Question>().ReverseMap();
            CreateMap<QuizAddDTO,Quiz>().ReverseMap();
            CreateMap<QuizDTO, Quiz>().ReverseMap();
            CreateMap<QuizAttempDTO, QuizAttempt>().ReverseMap();
            CreateMap<QuizAttemptQuestionDTO, QuizAttemptQuestion>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();


            //CreateMap<QuizAttemptAddOrUpdateDTO, quizat>();

        }
    }
}
