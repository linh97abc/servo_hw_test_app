using System;
using System.Collections.Generic;
using System.IO.Ports;
using Avalonia.Threading;

// using FFT.COBS;
using COBS.NET;

namespace servo_hw_test_app.Service;

class ComService
{

    private SerialPort _port;
    private List<byte> _buffer = new List<byte>();
    DispatcherTimer _timer;

    ComTxData _txData = new ComTxData();

    public ComService()
    {
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(100);
        _timer.Tick += OnTimerTick;
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        OnUserCompute?.Invoke(_txData);
        var buff = COBS.NET.COBS.Encode(_txData.ToByteArray()); // Test COBS encoding
        if (_port != null && _port.IsOpen)
        {
            _port.Write(buff, 0, buff.Length);
            _port.Write(new byte[] { 0x00 }, 0, 1); // Frame delimiter
        }
    }

    public void Open(string port)
    {
        _port = new SerialPort(port, 921600);
        _port.DataReceived += OnDataReceived;
        _port.Open();
        _timer.Start();
    }

    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        byte[] temp = new byte[_port.BytesToRead];
        _port.Read(temp, 0, temp.Length);

        lock (_buffer)
        {
            _buffer.AddRange(temp);
            ProcessBuffer();
        }
    }

    uint _seq = 0;
    private void ProcessBuffer()
    {
        while (true)
        {
            int idx = _buffer.IndexOf(0x00);
            if (idx < 0) return;

            byte[] frame = _buffer.GetRange(0, idx).ToArray();
            _buffer.RemoveRange(0, idx + 1);

            if (frame.Length == 0) continue;

            try
            {

                byte[] decoded = COBS.NET.COBS.Decode(frame);
                // HandleRxPacket(decoded);
                var data = new ComRxData(_seq++, decoded);
                if (data.is_crc_valid)
                {
                    OnMessageReceived?.Invoke(data);

                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Decode lỗi: " + ex.Message);
            }
        }
    }


    public void Close()
    {
        _timer.Stop();
        if (_port != null && _port.IsOpen)
        {
            _port.Close();

        }
    }

    public event Action<ComTxData> OnUserCompute;
    public event Action<ComRxData> OnMessageReceived;
}
