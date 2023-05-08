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
        [CustomLabel("玩家名字")]
        public string name;

        [CustomLabel("脚本索引项")]
        public List<ScriptData> scripts;

        [CustomLabel("脚本项")]
        public List<ScriptItemData> scriptItems;

        [CustomLabel("图片列表")]
        public List<SpriteFrameData> pictures;

        [CustomLabel("公共色盘列表")]
        public PaletteData[] publicPalettes;

        [CustomLabel("声音列表")]
        public List<SoundData> sounds;

        // TODO: 尚未解析的部分
        // public List<StoryData> stories;
        // public PlayerBaseInfo baseInfo;
        // public List<AiItem> aiItems;
        // public List<Command> commands;

        [HideInInspector]
        public byte[] originalBytes;

        public int scriptCount => scripts?.Count ?? 0;
        public int scriptItemCount => scriptItems?.Count ?? 0;
        public int pictureCount => pictures?.Count ?? 0;
        public int soundCount => sounds?.Count ?? 0;

        public PlayerData()
        {
            this.publicPalettes = new PaletteData[8];
            
            this.scripts = new List<ScriptData>();
            this.scriptItems = new List<ScriptItemData>();
            this.pictures = new List<SpriteFrameData>();
            this.sounds = new List<SoundData>();
        }
    }
}
