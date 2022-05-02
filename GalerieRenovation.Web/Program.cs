using GalerieRenovation.Web;
using GalerieRenovation.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("data", hc => hc.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
string? contactFunctionUrl = builder.Configuration["CONTACTFORM_FUNCTION"];
if (string.IsNullOrEmpty(contactFunctionUrl))
{
    ArgumentNullException.ThrowIfNull(nameof(contactFunctionUrl));
}
else
{
    builder.Services.AddHttpClient("contact", hc => hc.BaseAddress = new Uri(contactFunctionUrl));
}
builder.Services.AddScoped<IContactService, ContactService>();


await builder.Build().RunAsync();