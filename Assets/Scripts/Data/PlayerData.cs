using System;
using System.Collections.Generic;

namespace Data
{
    public class PlayerData
    {
        public byte[] Signature { get; set; }
        
        public string Name { get; set; }
        
        public int ScriptCount { get; set; }
        public List<ScriptData> Scripts { get; set; }
        
        public int ScriptItemCount { get; set; }
        public List<ScriptItemData> ScriptItems { get; set; }
        
        public int SpriteFrameCount { get; set; }
        public List<SpriteFrameData> SpriteFrames { get; set; } 
        
        public PaletteData[] PublicPalettes { get; set; }
        
        public int SoundCount { get; set; }
        public List<SoundData> Sounds { get; set; }
        
        public byte[] OriginalBytes { get; set; }

        public PlayerData()
        {
            this.PublicPalettes = new PaletteData[8];
            
            this.Scripts = new List<ScriptData>();
            this.ScriptItems = new List<ScriptItemData>();
            this.SpriteFrames = new List<SpriteFrameData>();
            this.Sounds = new List<SoundData>();
        }
    }
}
