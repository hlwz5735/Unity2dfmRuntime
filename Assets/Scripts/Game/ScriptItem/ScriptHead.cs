using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ScriptItem
{
    class ScriptHead : BaseScriptItem
    {
        public int Level;

        public ScriptHead(int level = 0) : base((int)ScriptItemTypes.ScriptHead)
        {
            Level = level;
        }
    }
}