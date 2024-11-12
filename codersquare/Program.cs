using codersquare.BL;
using codersquare.DAL;
using codersquare.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Default Services

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Database

builder.Services.AddDbContext<CodersquareContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#endregion

#region Repos

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPostRepo, PostRepo>();
builder.Services.AddScoped<ILikeRepo, LikeRepo>();
builder.Services.AddScoped<ICommentRepo, CommentRepo>();


#endregion

#region Managers

builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IPostManager, PostManager>();
builder.Services.AddScoped<ILikeManager, LikeManager>();
builder.Services.AddScoped<ICommentManager, CommentManager>();


#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();