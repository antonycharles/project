using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project.Web;
using Project.Web.Providers;
using Project.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped<AuthApiService>(sp =>
    new AuthApiService(new HttpClient { BaseAddress = new Uri("http://localhost:9001") })
);

builder.Services.AddScoped<ProjectService>(sp =>
    new ProjectService(new HttpClient { BaseAddress = new Uri("http://localhost:9501/v1") })
);


await builder.Build().RunAsync();
