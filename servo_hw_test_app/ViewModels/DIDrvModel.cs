using CommunityToolkit.Mvvm.ComponentModel;

namespace servo_hw_test_app.ViewModels;

public partial class DIDrvModel : ObservableObject
{
    [ObservableProperty]
    private bool _isHallAActive;

    [ObservableProperty]
    private bool _isHallBActive;

    [ObservableProperty]
    private bool _isHallCActive;

    [ObservableProperty]
    private bool _isFaultActive;

    public int ChannelNumber { get; set; }
}