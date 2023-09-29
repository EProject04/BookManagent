using BEWebtoon.WebtoonDBContext;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BEWebtoon.Helpers;
using BEWebtoon.Repositories;
using BEWebtoon.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebtoonDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebtoonCS"))
);
var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperProfiles()));
IMapper mapper = autoMapper.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
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
