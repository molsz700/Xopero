using XoperoRecruitmentTask.Communication;
using XoperoRecruitmentTask.Logging;
using XoperoRecruitmentTask.Server.Interfaces;
using XoperoRecruitmentTask.Server.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IVolumeService, VolumeService>();
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();
builder.Services.AddSingleton<ILogMessageService, LogMessageService>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapHub<ReportHub>("/reporthub");

app.Run();
