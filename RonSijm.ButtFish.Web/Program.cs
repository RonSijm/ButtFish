using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RonSijm.ButtFish.Web;
using RonSijm.ButtFish.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register ButtFish services
builder.Services.AddScoped<ButtplugService>();
builder.Services.AddScoped<ChessService>();
builder.Services.AddScoped<StockfishService>();
builder.Services.AddScoped<ICharacterEncoder, MorseEncoder>();

await builder.Build().RunAsync();
