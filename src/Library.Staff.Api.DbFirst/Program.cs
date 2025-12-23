using Library.Staff.Core.Interfaces;
using Library.Staff.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IStaffRepository, StaffRepository>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var conStr = config.GetConnectionString("StaffsDb") ?? throw new InvalidOperationException("Connection string 'StaffsDb' not found.");
    return new StaffRepository(conStr!);
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
