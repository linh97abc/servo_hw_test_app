using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace servo_hw_test_app.ViewModels;

public partial class Ad7928Model : ObservableObject
{
    [ObservableProperty]
    private float _adcValue;



    
    public string Description { get; set; } 

    

}