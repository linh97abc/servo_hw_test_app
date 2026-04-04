using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;
using Jab;
using Serilog;
using ShadUI;
namespace servo_hw_test_app;

[ServiceProvider]
[Singleton<Service.ComService>]
[Singleton<ViewModels.MainWindowViewModel>]
[Singleton<ShadUI.DialogManager>]
[Singleton<ShadUI.ToastManager>]
[Singleton(typeof(ILogger), Factory = nameof(LoggerFactory))]
[Singleton(typeof(ThemeWatcher), Factory = nameof(ThemeWatcherFactory))]
public partial class ServiceProvider
{
    public static ServiceProvider Inst { get; } = new ServiceProvider();




    public ILogger LoggerFactory()
    {
        var currentFolder = Helper.ConfigFileProvider.UserLogPath;

        Directory.CreateDirectory(currentFolder); //ensure the directory exists

        var file = Path.Combine(currentFolder, "log.txt");

        var config = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(
                file,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14,
                // outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
            .WriteTo.Debug(
                outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            // .WriteTo.Sink(new LogChannelSink(_appLogVM))
            .CreateLogger();

        Log.Logger = config; //set the global logger

        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                Log.Fatal(ex, "AppDomain crash");
        };

        TaskScheduler.UnobservedTaskException += (s, e) =>
        {
            Log.Fatal(e.Exception, "Task crash");
            e.SetObserved();
        };

        Dispatcher.UIThread.UnhandledException += (s, e) =>
        {
            Log.Fatal(e.Exception, "UI crash");
        };

        return config;
    }

    public ThemeWatcher ThemeWatcherFactory()
    {
        return new ThemeWatcher(Application.Current!);
    }
}