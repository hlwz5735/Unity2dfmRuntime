using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ScriptItem
{
    interface Translator<T> where T : BaseScriptItem
    {
        public T Decode(byte[] bytes);
        public byte[] Encode(T item);
    }
}
