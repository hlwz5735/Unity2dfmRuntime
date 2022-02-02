using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP;
using UnityEngine;

namespace Game.ScriptItem
{
    class AnimationFrameScriptTranslator : Singleton<AnimationFrameScriptTranslator>, Translator<AnimationFrameScript>
    {
        public AnimationFrameScript Decode(byte[] bytes)
        {
            var item = new AnimationFrameScript
            {
                FreezeTime = BitConverter.ToUInt16(bytes, 0),
                PicIndex = BitConverter.ToUInt16(new byte[] { bytes[2], (byte)(bytes[3] & 0b00111111) }, 0),
                Offset = new Vector2(BitConverter.ToInt16(bytes, 4), BitConverter.ToInt16(bytes, 6)),
                FlipX = (bytes[3] & 0b01000000) != 0,
                FlipY = (bytes[3] & 0b10000000) != 0,
                FixDir = bytes[8] != 0
            };

            return item;
        }

        public byte[] Encode(AnimationFrameScript item)
        {
            // return new byte[0];
            throw new NotImplementedException();
        }
    }
}
