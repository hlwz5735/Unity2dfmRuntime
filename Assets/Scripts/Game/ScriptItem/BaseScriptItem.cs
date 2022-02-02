using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ScriptItem
{
    abstract class BaseScriptItem
    {
        public int Type;

        protected BaseScriptItem(int type)
        {
            this.Type = type;
        }
    }
}
