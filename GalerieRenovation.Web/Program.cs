using BlazorBootstrap;
using GalerieRenovation.Web;
using GalerieRenovation.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("data", hc => hc.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
string functionsBaseAddress = $"{builder.HostEnvironment.BaseAddress}{(builder.HostEnvironment.BaseAddress.EndsWith('/') ? string.Empty : "/")}api/";
builder.Services.AddHttpClient("functions", hc => hc.BaseAddress = new Uri(functionsBaseAddress));
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<INewsletterService, NewsletterService>();
builder.Services.AddBlazorBootstrap();

await builder.Build().RunAsync();