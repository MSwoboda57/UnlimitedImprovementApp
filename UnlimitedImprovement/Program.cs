using UnlimitedImprovement.Repositories;
using UnlimitedImprovement.Interfaces;
using UnlimitedImprovement.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IExercise, ExerciseRepository>();
builder.Services.AddTransient<ILearning, LearningRepository>();
builder.Services.AddTransient<IMeditation, MeditationRepository>();
builder.Services.AddTransient<INewIdea, NewIdeaRepository>();
builder.Services.AddTransient<INutrition, NutritionRepository>();
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IVice, ViceRepository>();
builder.Services.AddTransient<IExerciseDayOfTheWeek, ExerciseDayOfTheWeekRepository>();
builder.Services.AddTransient<IDayOfTheWeek, DayOfTheWeekRepository>();
builder.Services.AddTransient<ILearningType, LearningTypeRepository>();
builder.Services.AddTransient<INutritionDayOfTheWeek, NutritionDayOfTheWeekRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
