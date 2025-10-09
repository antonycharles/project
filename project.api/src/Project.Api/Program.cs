using Project.Application.Interfaces;
using Project.Application.Services;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;
using Project.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
#if RELEASE
    builder.Configuration.AddJsonFile("secrets/appsettings.json", false, true);
#else
    builder.Configuration.AddJsonFile("appsettings.json", false, true);
#endif

builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();


builder.Services.AddScoped<IProjectService, ProjectService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

var runner = new MigrationRunner(
    connectionString: connectionString,
    migrationsFolder: Path.Combine(AppContext.BaseDirectory, "Migrations")
);

await runner.RunAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
