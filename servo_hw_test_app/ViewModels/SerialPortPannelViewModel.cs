using System;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace servo_hw_test_app.ViewModels;

public partial class SerialPortPannelViewModel : ObservableObject
{
    public Avalonia.Collections.AvaloniaList<string> AvailablePorts { get; } = new();

    [ObservableProperty]
    private string _selectedPort;

    [ObservableProperty]
    bool _isConnected;

    public void RefreshAvailablePorts()
    {
        if (IsConnected)
            return;

        var list_posts = System.IO.Ports.SerialPort.GetPortNames();


        foreach (var item in AvailablePorts)
        {
            if (!list_posts.Contains(item))
            {
                AvailablePorts.Remove(item);
            }
        }

        foreach (var item in list_posts)
        {
            if (!AvailablePorts.Contains(item))
            {
                AvailablePorts.Add(item);
            }
        }


        // AvailablePorts.Clear();
        // foreach (var port in System.IO.Ports.SerialPort.GetPortNames())
        // {
        //     AvailablePorts.Add(port);
        // }
    }

    DispatcherTimer _refreshTimer;

    public SerialPortPannelViewModel()
    {
        var list_posts = System.IO.Ports.SerialPort.GetPortNames();
        AvailablePorts.AddRange(list_posts);

        _refreshTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(2)
        };
        _refreshTimer.Tick += (sender, e) => RefreshAvailablePorts();
        _refreshTimer.Start();

    }

    [RelayCommand]
    private void Connect()
    {
        if (string.IsNullOrEmpty(SelectedPort))
            return;

        ServiceProvider.Inst.GetService<Service.ComService>().Open(SelectedPort);

        IsConnected = true;
    }

    [RelayCommand]
    private void Disconnect()
    {
        if (!IsConnected)
            return;

        ServiceProvider.Inst.GetService<Service.ComService>().Close();

        IsConnected = false;
    }

}
