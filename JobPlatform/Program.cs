using JobPlatformBackend.Business.src.Services.Abstractions;
using JobPlatformBackend.Business.src.Services.Implementations;
using JobPlatformBackend.Domain.src.Abstractions;
using JobPlatformBackend.Infrastructure.src.Database;
using JobPlatformBackend.Infrastructure.src.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped(typeof(BaseService<,>));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
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
