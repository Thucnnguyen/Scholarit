using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scholarit.Data;
using Scholarit.Data.Repository;
using Scholarit.Data.Repository.RepositoryImp;
using Scholarit.ExceptionHandler;
using Scholarit.Service;
using Scholarit.Service.ServiceImp;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ScholaritDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("userDb"))
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<ICategoryRepo, CategoryRepo>();
builder.Services.AddTransient<IChapterRepo, ChapterRepo>();
builder.Services.AddTransient<ICourseRepo, CourseRepo>();
builder.Services.AddTransient<IEnrollRepo, EnrollRepo>();
builder.Services.AddTransient<IOrderDetailRepo, OrderDetailRepo>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<IQuestionRepo, QuestionRepo>();
builder.Services.AddTransient<IQuizAttemptQuestionRepo, QuizAttemptQuestionRepo>();
builder.Services.AddTransient<IQuizQuestionRepo, QuizQuestionRepo>();
builder.Services.AddTransient<IQuizAttemptRepo, QuizAttemptRepo>();
builder.Services.AddTransient<IQuizRepo, QuizRepo>();
builder.Services.AddTransient<IResourceRepo, ResourceRepo>();
builder.Services.AddTransient<IRoleRepo, RoleRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IChapterService, ChapterService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IEnrollService, EnrollService>();
builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IQuizAttemptQuestionService, QuizAttempQuestionService>();
builder.Services.AddTransient<IQuizQuestionService, QuizQuestionService>();
builder.Services.AddTransient<IQuizAttemptService, QuizAttempService>();
builder.Services.AddTransient<IQuizService, QuizService>();
builder.Services.AddTransient<IResourceService, ResourceService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOrderService, OrderService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
             .GetBytes(builder.Configuration.GetSection("Token:secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,

        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware(typeof(ExceptionMiddlewareExtensions));

app.UseAuthorization();

app.MapControllers();

app.Run();
