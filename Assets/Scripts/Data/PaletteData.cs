using System.Collections.Generic;
using Attributes;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// 色盘数据信息
    /// </summary>
    public class PaletteData
    {
        /// <summary>
        /// 是否为私有色盘
        /// </summary>
        public bool isPrivate { get; set; }

        /// <summary>
        /// 色盘颜色索引表
        /// </summary>
        public List<PaletteColor> colors { get; set; }

        public byte[] unknownGap;

        public PaletteData()
        {
            this.colors = new List<PaletteColor>(256);
        }

        public void pushBgra(byte[] buffer, int index = 0)
        {
            var color = new PaletteColor();
            color.rawData[0] = buffer[index + 2];
            color.rawData[1] = buffer[index + 1];
            color.rawData[2] = buffer[index];
            if (color.rawData[0] + color.rawData[1] + color.rawData[2] == 0)
            {
                color.rawData[3] = 0;
            }
            else if (this.isPrivate)
            {
                color.rawData[3] = 255;
            }
            else
            {
                color.rawData[3] = (byte) (buffer[index + 3] == 0 ? 0 : 255);
            }

            this.colors.Add(color);
        }
    }

    /// <summary>
    /// 色盘颜色对象
    /// </summary>
    public class PaletteColor
    {
        /// <summary>
        /// 原始字节数组数据
        /// </summary>
        public byte[] rawData { get; set; }

        public PaletteColor()
        {
            this.rawData = new byte[4];
        }

        public int intValue =>
            (rawData[0] << 24)
            | (rawData[1] << 16)
            | (rawData[2] << 8)
            | rawData[0];

        public byte red => rawData[0];
        public byte green => rawData[1];
        public byte blue => rawData[2];
        public byte alpha => rawData[3];

        public string hexValue
        {
            get
            {
                var hexStr = this.intValue.ToString("X");
                if (hexStr.Length < 7)
                {
                    return "0x0" + hexStr;
                }

                return "0x" + hexStr;
            }
        }

        public string rgbaValue
        {
            get { return $"rgba({rawData[0]}, {rawData[1]}, {rawData[2]}, {rawData[3] / 255.0})"; }
        }

        public static implicit operator Color(PaletteColor pc) => new Color(
            pc.red / 255f, pc.green / 255f, pc.blue / 255f, pc.alpha / 255f
        );

        public static implicit operator int(PaletteColor pc) => pc.intValue;
    }
}