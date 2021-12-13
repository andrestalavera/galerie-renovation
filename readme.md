# Galerie Rénovation
Blazor wasm application for [http://www.galerierenovation.com].

# Getting started
## Requirements
- NodeJS tools (https://nodejs.org/)
- .NET 6 tools (https://dot.net/)
- Libman tools (https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/)

## Build stylesheets
- Restore bootstrap with libman 
	- using CLI https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-cli
	- using Visual Studio https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-6.0
- Restore NPM packages (In Visual Studio Solution Explorer, right-click `packages.json` file and click on `Restore packages` command)
- Build SCSS with Grunt (With Visual Studio Task Runner Explorer)

## Build webapp
### Using CLI
Use `dotnet build -c {CONFIGURATION}` command.
> Where `{CONFIGURATION}` is `Debug` or `Release`