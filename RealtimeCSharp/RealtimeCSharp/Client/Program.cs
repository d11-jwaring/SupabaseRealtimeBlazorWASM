using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealtimeCSharp.Client;
using RealtimeCSharp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<SupabaseService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var host = builder.Build();

string supabaseUrl = host.Configuration["SUPABASE_URL"];
string supabaseKey = host.Configuration["SUPABASE_KEY"];

var supabaseService = host.Services.GetRequiredService<SupabaseService>();
supabaseService.Initialise(supabaseUrl, supabaseKey);

await host.RunAsync();
