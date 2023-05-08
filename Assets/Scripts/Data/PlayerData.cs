using System;
using System.Collections.Generic;
using Attributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        [HideInInspector]
        public byte[] signature;
        
        [CustomLabel("玩家名字")]
        public string name;
        
        [CustomLabel("脚本总数")]
        public int scriptCount;
        
        [HideInInspector]
        public List<ScriptData> scripts;

        [CustomLabel("脚本项总数")]
        public int scriptItemCount;

        [HideInInspector]
        public List<ScriptItemData> scriptItems;

        [CustomLabel("图片总数")]
        public int spriteFrameCount;
        
        [HideInInspector]
        public List<SpriteFrameData> spriteFrames;

        [CustomLabel("公共色盘列表")]
        public PaletteData[] publicPalettes;

        [CustomLabel("声音总数")]
        public int soundCount;
        
        [HideInInspector]
        public List<SoundData> sounds;

        [HideInInspector]
        public byte[] originalBytes;

        public PlayerData()
        {
            this.publicPalettes = new PaletteData[8];
            
            this.scripts = new List<ScriptData>();
            this.scriptItems = new List<ScriptItemData>();
            this.spriteFrames = new List<SpriteFrameData>();
            this.sounds = new List<SoundData>();
        }
    }
}
