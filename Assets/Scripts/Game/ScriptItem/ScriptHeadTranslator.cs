using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP;

namespace Game.ScriptItem
{
    class ScriptHeadTranslator : Singleton<ScriptHeadTranslator>, Translator<ScriptHead>
    {
        public ScriptHead Decode(byte[] bytes)
        {
            return new ScriptHead(bytes[1]);
        }

        public byte[] Encode(ScriptHead item)
        {
            // return new byte[0];
            throw new NotImplementedException();
        }
    }
}
