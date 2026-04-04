using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace servo_hw_test_app.Views;

public partial class SerialPortPannel : UserControl
{
    public SerialPortPannel()
    {
        InitializeComponent();
        this.DataContext = new ViewModels.SerialPortPannelViewModel();
    }
}