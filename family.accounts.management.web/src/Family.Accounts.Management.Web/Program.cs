using Family.Accounts.Management.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddConfigurationRoot();
var settings = builder.GetSettings();
builder.AddDependence(settings);


builder.Services.AddAuthentication("CookieManagement")
    .AddCookie("CookieManagement", options =>
    {
        options.Cookie.Name = "Family.Accounts.Management.Web";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddControllersWithViews();



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
