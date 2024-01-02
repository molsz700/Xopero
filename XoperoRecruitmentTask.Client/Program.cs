using Microsoft.Extensions.Configuration;
using System.Text;
using XoperoRecruitmentTask.Win32API;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using XoperoRecruitmentTask.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using XoperoRecruitmentTask.Logging;
using XoperoRecruitmentTask.Logging.Enums;

using IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IVolumeService, VolumeService>();
        services.AddSingleton<ILogMessageService, LogMessageService>();
    })
    .Build();

var vs = ActivatorUtilities.CreateInstance<VolumeService>(host.Services);
var ms = ActivatorUtilities.CreateInstance<LogMessageService>(host.Services);

var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var config = configurationBuilder.Build();

var baseAddress = config.GetSection("Settings:ServerBaseAddress").Value;
var apiAddress = baseAddress + "/api/report/savedata";

var connection = new HubConnectionBuilder()
    .WithUrl(baseAddress + "/reporthub")
    .Build();

connection.StartAsync().Wait();
connection.On("report_progress", (string message) =>
{
    Console.WriteLine(message);
    ms.Log(LogType.Info, $"Message from server: {message}\n");
});

var interval = config.GetSection("Settings:RepeatIntervalMinutes").Value;
var timer = new PeriodicTimer(TimeSpan.FromMinutes(Convert.ToDouble(interval)));

while (await timer.WaitForNextTickAsync())
{

    var request = new VolumeRequest()
    {
        MachineName = Environment.MachineName,
        TimeStamp = DateTime.Now,
        VolumeInfos = vs!.GetVolumes()
    };

    var json = JsonSerializer.Serialize(request);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    using var client = new HttpClient();
    client.DefaultRequestHeaders.Add("connectionId", connection.ConnectionId);
    ms.Log(LogType.Info, "Request sent to server");
    var response = await client.PostAsync(apiAddress, content);
    ms.Log(LogType.Info, $"Server responded with code {response.StatusCode}");
}


await host.RunAsync();
