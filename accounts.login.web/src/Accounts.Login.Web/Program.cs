using Accounts.Login.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.AddConfigurationRoot();
var settings = builder.GetSettings();
builder.AddDependence();


builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "Accounts.Login.Web";
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(180);
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = settings.RedisUrl;
    options.InstanceName = "accounts_login_"; // Optional prefix for cache keys
});

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(
    x =>
    {
        x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:5062", "https://localhost:5001", "http://localhost:9003"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowBlazor"); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
