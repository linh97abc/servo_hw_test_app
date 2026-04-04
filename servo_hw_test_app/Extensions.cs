using servo_hw_test_app.ViewModels;
using servo_hw_test_app.Views;
using ShadUI;

namespace servo_hw_test_app;

public static class Extensions
{
    public static ServiceProvider RegisterDialogs(this ServiceProvider service)
    {
        // var dialogService = service.GetService<DialogManager>();
        
        // dialogService.Register<AboutView, AboutViewModel>()
        //             .Register<InstallFirmwareView, InstallFirmwareViewModel>()
        //             .Register<LogView, LogViewModel>()
        //             .Register<FaultFlagView, FaultFlagViewModel>()
        //             .Register<ConfigDeviceIdView, ConfigDeviceIdViewModel>()
        //             .Register<WriteEepromView, WriteEepromViewModel>();

        return service;
    }
}