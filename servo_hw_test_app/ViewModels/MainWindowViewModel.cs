using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using servo_hw_test_app.Service;
using ShadUI;

namespace servo_hw_test_app.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{


    [ObservableProperty]
    private DialogManager _dialogManager;

    public MainWindowViewModel()
    {
        _dialogManager = ServiceProvider.Inst.GetService<DialogManager>();

        // test
        // Ltc2992.VoltageSupply = 12.34f;
        // Ltc2992.CurrentSupply = 1.23f;
        // Ltc2992.VoltageMCU = 3.30f;
        // Ltc2992.CurrentMCU = 0.45f;

        // Tmp101.Temperature = 36;

        var comService = ServiceProvider.Inst.GetService<Service.ComService>();
        comService.OnMessageReceived += ProcessReceivedData;

        comService.OnUserCompute += ProcessTxData;

    }

    void ProcessReceivedData(ComRxData data)
    {
        // Process the received data and update the UI accordingly
        // For example, if ComRxData contains a temperature value:
        // Tmp101.Temperature = data.Temperature;

        for (int i = 0; i < 4; i++)
        {
            DrvInputChannels[i].IsHallAActive = (data.hall[i] & (1 << 2)) != 0;
            DrvInputChannels[i].IsHallBActive = (data.hall[i] & (1 << 1)) != 0;
            DrvInputChannels[i].IsHallCActive = (data.hall[i] & (1 << 0)) != 0;
            DrvInputChannels[i].IsFaultActive = (data.fault & (1 << i)) != 0;
        }

        Fuse.FuseStatus = data.pio_input;

        Ltc2992.VoltageSupply = data.bus_voltage;
        Ltc2992.CurrentSupply = data.bus_current;
        Ltc2992.VoltageMCU = data.mcu_voltage;
        Ltc2992.CurrentMCU = data.mcu_current;

        Tmp101.Temperature = data.temperature;
        for (int i = 0; i < 4; i++)
        {
            MotorPositions[i].AdcValue = data.position[i];
        }

        for (int i = 0; i < 4; i++)
        {
            MotorCurrents[i].AdcValue = data.i_motor[i];
        }
    }

    void ProcessTxData(ComTxData txData)
    {
        // Process the data to be sent based on user input
        // For example, you can set the duty cycles and enable channels based on the current state of the UI:
        // txData.dutyCycles[0] = (int)(PWMChannels[0].DutyCycle * 100); // Convert to percentage
        // txData.enableChannels[0] = DOChannels[0].IsEnabled;

        for (int i = 0; i < 4; i++)
        {
            txData.dutyA[i] = PWMChannels[i].AppliedDutyA;
            txData.dutyB[i] = PWMChannels[i].AppliedDutyB;
            txData.dutyC[i] = PWMChannels[i].AppliedDutyC;
        }

        for (int i = 0; i < 4; i++)
        {
            txData.enableChannels[i] = DOChannels[i].IsActive;
        }
    }

    public List<DIDrvModel> DrvInputChannels { get; } = new List<DIDrvModel>
    {
        new DIDrvModel() {ChannelNumber = 1},
        new DIDrvModel() {ChannelNumber = 2},
        new DIDrvModel() {ChannelNumber = 3},
        new DIDrvModel() {ChannelNumber = 4},
    };

    public Ltc2992ViewModel Ltc2992 { get; } = new Ltc2992ViewModel();

    public Tmp101ViewModel Tmp101 { get; } = new Tmp101ViewModel();

    public FuseViewModel Fuse { get; } = new FuseViewModel();

    public List<Ad7928Model> MotorCurrents { get; } = new List<Ad7928Model>
    {
        new Ad7928Model() { Description = "Dòng điện máy lái 1" },
        new Ad7928Model() { Description = "Dòng điện máy lái 2" },
        new Ad7928Model() { Description = "Dòng điện máy lái 3" },
        new Ad7928Model() { Description = "Dòng điện máy lái 4" },
    };

    public List<Ad7928Model> MotorPositions { get; } = new List<Ad7928Model>
    {
        new Ad7928Model() { Description = "Vị trí máy lái 1" },
        new Ad7928Model() { Description = "Vị trí máy lái 2" },
        new Ad7928Model() { Description = "Vị trí máy lái 3" },
        new Ad7928Model() { Description = "Vị trí máy lái 4" }
    };

    public List<DOModel> DOChannels { get; } = new List<DOModel>
    {
        new DOModel() { Description = "Enable driver Máy lái 1" },
        new DOModel() { Description = "Enable driver Máy lái 2" },
        new DOModel() { Description = "Enable driver Máy lái 3" },
        new DOModel() { Description = "Enable driver Máy lái 4" },
    };

    public List<PWMChannelModel> PWMChannels { get; } = new List<PWMChannelModel>
    {
        new PWMChannelModel() { Description = "1" },
        new PWMChannelModel() { Description = "2" },
        new PWMChannelModel() { Description = "3" },
        new PWMChannelModel() { Description = "4" },
    };
}
