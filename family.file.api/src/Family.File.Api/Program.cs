using Family.File.Api.Configurations;
using Family.File.Api.Helpers;
using Family.File.Infrastructure.Data;
using Family.File.Infrastructure.interfaces;
using Family.File.Infrastructure.Repositories;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.AddConfigurationRoot();
var settings = builder.GetSettings();
builder.Services.AddScoped<IUploadHelper,UploadHelper>();

var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
Directory.CreateDirectory(uploadDir);


var dbInitializer = new DatabaseInitializer(settings.DatabaseHost, settings.DatabasePort, settings.DatabaseUser, settings.DatabasePassword);
dbInitializer.Initialize();
builder.Services.AddSingleton(new Npgsql.NpgsqlConnection(dbInitializer.GetConnectionString()));

builder.Services.AddScoped<IFileDocumentRepository, FileDocumentRepository>();


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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadDir),
    RequestPath = "/files"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
