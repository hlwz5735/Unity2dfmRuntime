using System.Collections.Generic;

namespace Data
{
    public class PaletteData
    {
        public bool IsPrivate { get; set; }

        public List<PaletteColor> Colors { get; set; }
        
        private byte[] UnknownGap;

        public PaletteData()
        {
            this.Colors = new List<PaletteColor>(256);
        }
        
        public void PushBgra(byte[] buffer, int index = 0)
        {
            var color = new PaletteColor();
            color.RawData[0] = buffer[index + 2];
            color.RawData[1] = buffer[index + 1];
            color.RawData[2] = buffer[index];
            if (this.IsPrivate)
            {
                color.RawData[3]
                    = (byte) (color.RawData[0] + color.RawData[1] + color.RawData[2] == 0 ? 0 : 255);
            } else
            {
                color.RawData[3] = (byte) (buffer[index + 3] == 0 ? 0 : 255);
            }

            this.Colors.Add(color);
        }
    }

    public class PaletteColor
    {
        public byte[] RawData { get; set; }

        public int IntValue =>
            RawData[0] << 24 +
            RawData[1] << 16 +
            RawData[2] << 8 +
            RawData[0];

        public byte Red => RawData[0];
        public byte Green => RawData[1];
        public byte Blue => RawData[2];
        public byte Alpha => RawData[3];

        public string HexValue
        {
            get
            {
                var hexStr = this.IntValue.ToString("X");
                if (hexStr.Length < 7)
                {
                    return "0x0" + hexStr;
                }
                return "0x" + hexStr;
            }
        }

        public string rgbaValue
        {
            get
            {
                return $"rgba({RawData[0]}, {RawData[1]}, {RawData[2]}, {RawData[3] / 255.0})";
            }
        }
    }
}