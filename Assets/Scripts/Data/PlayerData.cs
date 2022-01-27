using System;
using System.Collections.Generic;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        [HideInInspector]
        public byte[] Signature;
        
        [CustomLabel("玩家名字")]
        public string Name;
        
        [CustomLabel("脚本总数")]
        public int ScriptCount;
        public List<ScriptData> Scripts;

        [CustomLabel("脚本项总数")]
        public int ScriptItemCount;
        public List<ScriptItemData> ScriptItems;

        [CustomLabel("精灵帧总数")]
        public int SpriteFrameCount;
        public List<SpriteFrameData> SpriteFrames;

        public PaletteData[] PublicPalettes;

        [CustomLabel("声音总数")]
        public int SoundCount;
        public List<SoundData> Sounds;

        public byte[] OriginalBytes;

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
