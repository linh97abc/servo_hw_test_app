using CommunityToolkit.Mvvm.ComponentModel;

using servo_hw_test_app.ViewModels;

public partial class Tmp101ViewModel: ViewModelBase
{
    [ObservableProperty]
    float _temperature;
    
}