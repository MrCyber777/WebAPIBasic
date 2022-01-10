
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApp.Business;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using MyApp.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<IProjectScreen,ProjectScreen>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ITicketScreen,TicketScreen>();
builder.Services.AddTransient<ITicketRepository,TicketRepository>();
builder.Services.AddSingleton<IWebApiExecuter>(options => new WebApiExecuter("https://localhost:44334", new HttpClient()));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// AddScoped=Singleton  

await builder.Build().RunAsync();
