using Microsoft.EntityFrameworkCore;
using MissingZoneApi.Entities;
using MissingZoneApi.Interfaces;
using MissingZoneApi.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<mzonedbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IAdmin, AdminRepo>();
builder.Services.AddScoped<IComment, CommentRepo>();
builder.Services.AddScoped<IMissingPost, MissingPostRepo>();
builder.Services.AddScoped<IMp2v, Mp2vRepo>();
builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IVolunteer, VolunteerRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
