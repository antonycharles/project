using Team.Accounts.Login.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.AddConfigurationRoot();
var settings = builder.GetSettings();
builder.AddDependence();


builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "Team.Accounts.Login.Web";
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(180);
    });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = settings.RedisUrl;
    options.InstanceName = "team_accounts_login_"; // Optional prefix for cache keys
});

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(
    x =>
    {
        x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
