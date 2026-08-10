using Core.Models;
using hospital_web;
using hospital_web.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using MudBlazor.Services;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Register your handler FIRST
builder.Services.AddScoped<BearerTokenHandler>();

// Then configure HttpClient
builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri("https://hospital-api-ya93.onrender.com"); // your API URL
})


.AddHttpMessageHandler<BearerTokenHandler>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
// Factory for injection
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));

builder.Services.AddScoped<hospital_web.Services.IAuthService, hospital_web.Services.AuthService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ICustomAuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<RegisterModel>();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<ReferenceDataService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<hospital_web.Services.PatientService>();
builder.Services.AddMudServices();



await builder.Build().RunAsync();

