#region
using Serilog;
using Vonage;
using Vonage.Common.Monads;
using Vonage.Extensions;
using Vonage.Request;
using SampleVideoBlazor.Presentation.Components;
using SampleVideoBlazor.Presentation.Data;
#endregion

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddVonageClientScoped(builder.Configuration);
builder.Services.AddScoped<Credentials>(provider => provider.GetRequiredService<VonageClient>().Credentials);
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