using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace servo_hw_test_app.ViewModels;

public partial class PWMChannelModel : ObservableObject
{
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
}

