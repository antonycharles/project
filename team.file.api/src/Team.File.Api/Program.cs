using Team.File.Api.Authorization;
using Team.File.Api.Configurations;
using Team.File.Api.Helpers;
using Team.File.Infrastructure.Data;
using Team.File.Infrastructure.interfaces;
using Team.File.Infrastructure.interfaces.External;
using Team.File.Infrastructure.Repositories;
using Team.File.Infrastructure.Repositories.External;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

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

builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IClientAuthorizationRepository, ClientAuthorizationRepository>();

builder.Services.AddAuthentication("Bearer")
    .AddScheme<JwtBearerOptions, CustomJwtBearerHandler>("Bearer", options => { });


builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Team.File.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,

                },
                new List<string>()
            }
    });
});

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
