using System.Collections.Generic;

namespace Data
{
    public class ScriptData
    {
        public string name { get; set; }
        public int itemBeginIndex { get; set; }
        public int itemCount { get; set; }
        public List<ScriptItemData> items { get; set; }
        public bool isDefaultScript { get; set; }

        public ScriptData()
        {
            this.items = new List<ScriptItemData>();
        }
        
        public byte unknownFlag1;
        public byte[] unknownBytes;
    }
}