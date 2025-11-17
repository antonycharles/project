using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Project.Web;
using Project.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthApiService>(sp =>
    new AuthApiService(new HttpClient { BaseAddress = new Uri("https://localhost:5001") })
);

builder.Services.AddScoped<ProjectService>(sp =>
    new ProjectService(new HttpClient { BaseAddress = new Uri("https://localhost:5001") })
);

await builder.Build().RunAsync();
