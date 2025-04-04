using BrainsToDo.Data;
using BrainsToDo.Mapper;
using BrainsToDo.Repositories;
using BrainsToDo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<ICrudRepository<User>, UserRepository>();
builder.Services.AddScoped<ICrudRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<ICrudRepository<Job>, JobRepository>();
builder.Services.AddScoped<ICrudRepository<Contact>, ContactRepository>();
builder.Services.AddScoped<ICrudRepository<Person>, PersonRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<EducationRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


//builder.Services.AddDbContext<WorkerContext>(options => options.UseInMemoryDatabase("WorkersDatabase"));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
