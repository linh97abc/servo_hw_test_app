using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace servo_hw_test_app.Controls;

public class DeviceInfoRowItem : ContentControl
{
    public static readonly StyledProperty<string> TitleProperty = AvaloniaProperty.Register<DeviceInfoRowItem, string>(
        nameof(Title));

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}