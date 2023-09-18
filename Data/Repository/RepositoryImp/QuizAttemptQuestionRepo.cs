﻿using AlumniProject.Data.Repostitory.RepositoryImp;
using Scholarit.Entity;

namespace Scholarit.Data.Repository.RepositoryImp
{
    public class QuizAttemptQuestionRepo : RepositoryBase<QuizAttemptQuestion>, IQuizAttemptQuestionRepo
    {
        public QuizAttemptQuestionRepo(ScholaritDbContext context) : base(context)
        {
        }
    }
}
