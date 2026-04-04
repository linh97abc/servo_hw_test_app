using CommunityToolkit.Mvvm.ComponentModel;

using servo_hw_test_app.ViewModels;

public partial class Ltc2992ViewModel: ViewModelBase
{
    [ObservableProperty]
    float _voltageSupply;
    
    [ObservableProperty]
    float _currentSupply;


    [ObservableProperty]
    float _voltageMCU;

    [ObservableProperty]
    float _currentMCU;
}