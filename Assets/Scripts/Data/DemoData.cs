using System.Collections.Generic;

namespace Data {
    public class DemoData {
        public List<ScriptData> scripts;
        public List<ScriptItemData> scriptItems;
        public List<SpriteFrameData> pictures;
        public List<PaletteData> commonPalettes;
        public List<SoundData> sounds;
        
        /// <summary>
        /// 背景音ID
        /// </summary>
        public uint bgmSoundId;
        
        /// <summary>
        /// 是否按任意键跳过
        /// </summary>
        public bool isSkipByPressing;
        
        /// <summary>
        /// 播放时间
        /// </summary>
        public uint timeConsumed;
    }
}
