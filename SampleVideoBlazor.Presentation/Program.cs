#region
using Blazored.Toast;
using Blazored.Toast.Configuration;
using SampleVideoBlazor.Presentation.Components;
using SampleVideoBlazor.Presentation.Data;
using Serilog;
using Vonage;
using Vonage.Extensions;
#endregion

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredToast();
builder.Services.AddVonageClientScoped(builder.Configuration);
builder.Services.AddScoped(provider => provider.GetRequiredService<VonageClient>().Credentials);
builder.Services.AddScoped<SessionGenerator>();
builder.Services.AddScoped<VideoService>();
builder.Services.AddScoped<SessionFactory>();
builder.Services.AddSingleton<StateContainer>();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();