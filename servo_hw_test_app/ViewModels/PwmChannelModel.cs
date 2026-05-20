using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace servo_hw_test_app.ViewModels;

public partial class PWMChannelModel : ObservableObject
{
    [ObservableProperty]
    private int _targetDutyModeA;

    [ObservableProperty]
    private int _targetDutyModeB;

    [ObservableProperty]
    private int _targetDutyModeC;

    [ObservableProperty]
    private string _targetDutyA;

    [ObservableProperty]
    private string _targetDutyB;

    [ObservableProperty]
    private string _targetDutyC;

    private Dictionary<string, string> _errors = new();

    public string Description { get; set; }


    public int AppliedDutyA { get; set; }
    public int AppliedDutyB { get; set; }
    public int AppliedDutyC { get; set; }

    int LevelToDuty(int level) => level switch
    {
        0 => -1,   // Disabled
        1 => 0,    // 0% duty cycle
        2 => 25,   // 25% duty cycle
        3 => 50,   // 50% duty cycle
        4 => 75,   // 75% duty cycle
        5 => 100,  // 100% duty cycle
        _ => -1    // Default to disabled for invalid values
    };

    partial void OnTargetDutyModeAChanged(int value)
    {
        AppliedDutyA = LevelToDuty(value);
        // TargetDutyA = AppliedDutyA.ToString();
    }

    partial void OnTargetDutyModeBChanged(int value)
    {
        AppliedDutyB = LevelToDuty(value);
        // TargetDutyB = AppliedDutyB.ToString();
    }

    partial void OnTargetDutyModeCChanged(int value)
    {
        AppliedDutyC = LevelToDuty(value);
        // TargetDutyC = AppliedDutyC.ToString();
    }


    [RelayCommand]
    void ApplyDutyAValue()
    {
        if (!int.TryParse(TargetDutyA, out var duty))
        {
            System.Diagnostics.Debug.WriteLine($"{Description}: Cannot apply - TargetDutyA has validation error");
        }
        else
        {
            if (duty <= 100)
            {
                AppliedDutyA = duty;
                System.Diagnostics.Debug.WriteLine($"{Description}: Applied Duty A Value: {AppliedDutyA}");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"{Description}: Invalid Duty A Value: {duty}. Valid range: 0-100 or negative to disable.");

        }
        TargetDutyA = AppliedDutyA.ToString(); // Revert to last valid value
    }

    [RelayCommand]
    void ApplyDutyBValue()
    {
        if (!int.TryParse(TargetDutyB, out var duty))
        {
            System.Diagnostics.Debug.WriteLine($"{Description}: Cannot apply - TargetDutyB has validation error");


        }
        else
        {
            if (duty <= 100)
            {
                AppliedDutyB = duty;
                System.Diagnostics.Debug.WriteLine($"{Description}: Applied Duty B Value: {AppliedDutyB}");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"{Description}: Invalid Duty B Value: {duty}. Valid range: 0-100 or negative to disable.");

        }

        TargetDutyB = AppliedDutyB.ToString(); // Revert to last valid value
    }

    [RelayCommand]
    void ApplyDutyCValue()
    {
        if (!int.TryParse(TargetDutyC, out var duty))
        {
            System.Diagnostics.Debug.WriteLine($"{Description}: Cannot apply - TargetDutyC has validation error");
        }
        else
        {
            if (duty <= 100)
            {
                AppliedDutyC = duty;
                System.Diagnostics.Debug.WriteLine($"{Description}: Applied Duty C Value: {AppliedDutyC}");
                return;
            }

            System.Diagnostics.Debug.WriteLine($"{Description}: Invalid Duty C Value: {TargetDutyC}. Valid range: 0-100 or negative to disable.");

        }
        TargetDutyC = AppliedDutyC.ToString(); // Revert to last valid value
    }

    public PWMChannelModel()
    {
        OnTargetDutyModeAChanged(0);
        OnTargetDutyModeBChanged(0);
        OnTargetDutyModeCChanged(0);
    }

}

