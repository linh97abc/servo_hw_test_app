using System;
using System.IO;

namespace Helper;

public static class ConfigFileProvider
{
    public static string UserConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            servo_hw_test_app.AppConstants.ConfigFolderName,
            "configs");

    public static string UserLogPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                servo_hw_test_app.AppConstants.ConfigFolderName,
                "logs");

    public static string AppConfigPath = Path.Combine(
        AppContext.BaseDirectory,
        "configs");

    public static string GetFileConfig(string file_name)
    {
        var user_file = Path.Combine(UserConfigPath, file_name);

        if (File.Exists(user_file))
        {
            return user_file;
        }

        var app_file = Path.Combine(AppConfigPath, file_name);

        if (File.Exists(app_file))
        {
            return app_file;
        }

        return string.Empty;
    }
}