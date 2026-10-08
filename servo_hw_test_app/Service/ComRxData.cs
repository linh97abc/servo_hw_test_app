using System;
using System.Linq;

namespace servo_hw_test_app.Service;

class ComRxData
{
    public uint[] hall = new uint[4];
    public uint fault;



    public float[] position = new float[4];
    public float[] i_motor = new float[4];

    public uint ltc_status;
    public uint tmp101_status;

    public float temperature;

    public float bus_voltage;
    public float bus_current;

    public float mcu_voltage;
    public float mcu_current;

    public float[] adc_pwr = new float[8];

    public bool is_crc_valid;

    public uint seq;

    public ComRxData(uint sequence, byte[] data)
    {
        this.seq = sequence;

        // Parse the byte array to populate the properties
        // This is just a placeholder. You need to implement the actual parsing logic based on your data format.

        var crc_expected = ComCrc.CaculateCrc(data.Take(data.Length - 2));
        var crc_actual = BitConverter.ToUInt16(data, data.Length - 2);
        is_crc_valid = crc_expected == crc_actual;

        if (!is_crc_valid) return;

        using (var ms = new System.IO.MemoryStream(data))
        using (var br = new System.IO.BinaryReader(ms))
        {
            for (int i = 0; i < 4; i++)
            {
                hall[i] = br.ReadByte();
            }
            fault = br.ReadByte();
            _ = br.ReadByte();
            for (int i = 0; i < 4; i++)
            {
                position[i] = br.ReadUInt16();
            }
            for (int i = 0; i < 4; i++)
            {
                i_motor[i] = br.ReadUInt16() * 0.001221f;
            }
            ltc_status = br.ReadByte();
            tmp101_status = br.ReadByte();
            temperature = (br.ReadInt16()) * (1.0f / 256);

            const float LTC2992_VOLTAGE_LSB = 25e-3f;
            const float LTC2992_CURRENT_LSB = 12.5e-6f;
            const float RSHUNT_1 = 0.001f;
            const float RSHUNT_2 = 0.01f;
            bus_voltage = br.ReadUInt16() * LTC2992_VOLTAGE_LSB;
            mcu_voltage = br.ReadUInt16() * LTC2992_VOLTAGE_LSB;
            bus_current = br.ReadUInt16() * (LTC2992_CURRENT_LSB / RSHUNT_1);
            mcu_current = br.ReadUInt16() * (LTC2992_CURRENT_LSB / RSHUNT_2);

            for (int i = 0; i < 8; i++)
            {
                adc_pwr[i] = br.ReadUInt16();
            }
        }
    }


}