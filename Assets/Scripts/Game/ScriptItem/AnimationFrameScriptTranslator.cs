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
        public AnimationFrameScript decode(byte[] bytes)
        {
            var item = new AnimationFrameScript
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

        public byte[] encode(AnimationFrameScript item)
        {
            // return new byte[0];
            throw new NotImplementedException();
        }
    }
}
