using BlazorBootstrap;
using GalerieRenovation.Web;
using GalerieRenovation.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("data", hc => hc.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
string? contactFunctionUrl = builder.Configuration["FUNCTION_API_BASEURL"];
ArgumentNullException.ThrowIfNull(contactFunctionUrl);
builder.Services.AddHttpClient("contact", hc => hc.BaseAddress = new Uri(contactFunctionUrl));
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddBlazorBootstrap();
string? emailFunctionUrl = builder.Configuration["FUNCTION_API_BASEURL"];
ArgumentNullException.ThrowIfNull(emailFunctionUrl);
builder.Services.AddHttpClient("api", hc => hc.BaseAddress = new(emailFunctionUrl));

await builder.Build().RunAsync();