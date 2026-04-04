using CommunityToolkit.Mvvm.ComponentModel;

using servo_hw_test_app.ViewModels;

public partial class FuseViewModel: ViewModelBase
{
    
    bool In1Active => HasFlag(0x01);

    
    bool In2Active => HasFlag(0x02);
    
    bool In3Active => HasFlag(0x04);

    
    bool In4Active => HasFlag(0x08);

    bool HasFlag(uint flag) => (FuseStatus & flag) != 0;

    uint fuseStatus;

    public uint FuseStatus
    {
        get => (uint)fuseStatus;
        set
        {
            if (SetProperty(ref fuseStatus, value))
            {
                OnPropertyChanged(nameof(In1Active));
                OnPropertyChanged(nameof(In2Active));
                OnPropertyChanged(nameof(In3Active));
                OnPropertyChanged(nameof(In4Active));
            }
        }
    }
}