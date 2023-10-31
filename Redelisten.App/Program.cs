using Redelisten.App.Interfaces;
using Redelisten.App.Repos;
using Redelisten.App.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IUserRepo, UserRepo>();
builder.Services.AddSingleton<IRedelisteRepo, RedelisteRepo>();
builder.Services.AddSingleton<IMeldungRepo, MeldungRepo>();
builder.Services.AddSingleton<IMeldungHistoryRepo, MeldungHistoryRepo>();

builder.Services.AddSignalR();
builder.Services.AddSingleton<ILivestreamSubscribeRepo, LivestreamSubscribeRepo>();

builder.Services.AddHostedService<RemoveOldHostedService>();

ServiceDescriptor liveService = ServiceDescriptor.Singleton(typeof(ILiveService<>), typeof(LiveService<>));
builder.Services.Add(liveService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<LiveHub>("/LiveInfos");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.UseCors(t=>
{
    t.AllowAnyHeader();
    t.AllowAnyMethod();
    t.AllowAnyOrigin();
    //t.AllowCredentials();
});

app.Run();
