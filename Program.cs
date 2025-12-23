using Library.Books.Core.Interfaces.Repositories;
using Library.Books.Infrastructure.Data;
using Library.Books.Infrastructure.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<BooksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BooksDb")));
builder.Services.AddScoped<IBookRepository, BookRepository>();

//Add rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedWindowPolicy", config =>
    {
        config.PermitLimit = 5;  // Max 5 requests
        config.Window = TimeSpan.FromMinutes(1);  //Per 1 minute
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit = 0;  // No queuing
    });
    options.RejectionStatusCode = 429; // Too Many Requests
});

var app = builder.Build();
app.UseRouting();
app.UseCors();
app.UseRateLimiter();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.Run();
