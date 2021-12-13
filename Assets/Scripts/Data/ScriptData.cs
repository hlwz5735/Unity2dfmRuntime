using System.Collections.Generic;

namespace Data
{
    public class ScriptData
    {
        public string Name { get; set; }
        public int ItemBeginIndex { get; set; }
        public int ItemCount { get; set; }
        public List<ScriptItemData> Items { get; set; }
        public bool IsDefaultScript { get; set; }

        public ScriptData()
        {
            this.Items = new List<ScriptItemData>();
        }
        
        public byte UnknownFlag1;
        public byte[] UnknownBytes;
    }
}