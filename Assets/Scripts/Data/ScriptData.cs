using System.Collections.Generic;

namespace Data
{
    public class ScriptData
    {
        public string name { get; set; }
        public int itemBeginIndex { get; set; }
        public int itemCount { get; set; }
        public ScriptItemData[] items { get; set; }

        public byte unknownFlag1;
        
        /// <summary>
        /// 特殊标记，在不同类型的脚本中具有不同的含义
        /// </summary>
        public int specialFlag { get; set; }

        public ScriptData()
        {
            this.items = new ScriptItemData[itemCount];
        }
    }
}
