using System;
using Data.ScriptItem;
using UnityEngine;

namespace _2dfmFile
{
    public static class ScriptItemReader
    {
        public static PlayerScriptHead readPlayerScriptHead(byte[] bytes) {
            return new PlayerScriptHead(bytes[1]);
        }

        public static PlayerShowPicture readPlayerShowPicture(byte[] bytes) {
            var item = new PlayerShowPicture
            {
                freezeTime = BitConverter.ToUInt16(bytes, 0),
                picIndex = BitConverter.ToUInt16(new byte[] { bytes[2], (byte)(bytes[3] & 0b00111111) }, 0),
                offset = new Vector2(BitConverter.ToInt16(bytes, 4), BitConverter.ToInt16(bytes, 6)),
                flipX = (bytes[3] & 0b01000000) != 0,
                flipY = (bytes[3] & 0b10000000) != 0,
                fixDir = bytes[8] != 0
            };

            return item;
        }
    }
}
