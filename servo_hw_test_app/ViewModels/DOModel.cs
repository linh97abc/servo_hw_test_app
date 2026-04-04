using CommunityToolkit.Mvvm.ComponentModel;

namespace servo_hw_test_app.ViewModels;

public partial class DOModel : ObservableObject
{
    [ObservableProperty]
    private bool _isActive;

    
    public string Description { get; set; } 

    
}