using System.Collections.Generic;

class ComTxData
{
    public int[] dutyA = new int[4]; // Assuming 4 servo channels
    public int[] dutyB = new int[4]; // Assuming 4 servo channels
    public int[] dutyC = new int[4]; // Assuming 4 servo channels
    public bool[] enableChannels = new bool[4]; // Enable/disable each channel

    public byte[] ToByteArray()
    {
        List<byte> data = new List<byte>();

        for (int i = 0; i < 4; i++)
        {
            data.Add((byte)dutyA[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            data.Add((byte)dutyB[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            data.Add((byte)dutyC[i]);
        }


        for (int i = 0; i < 4; i++)
        {
            var hiZ = ((dutyA[i] < 0) ? 1 : 0) |
                ((dutyB[i] < 0) ? 2 : 0) |
                ((dutyC[i] < 0) ? 4 : 0);

            data.Add((byte)hiZ);
        }

        byte enableByte = 0;
        for (int i = 0; i < 4; i++)
        {
            enableByte |= (byte)((enableChannels[i] ? 1 : 0) << i);
        }

        data.Add(enableByte);
        data.Add(0);

        var crc = ComCrc.CaculateCrc(data);
        data.Add((byte)(crc & 0xFF));
        data.Add((byte)((crc >> 8) & 0xFF));


        return data.ToArray();
    }
}