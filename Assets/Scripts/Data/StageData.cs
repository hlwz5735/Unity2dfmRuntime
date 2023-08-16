using System.Collections.Generic;

namespace Data {
    public class StageData
    {
        public string name;
        public int width;
        public int height;
        
        public List<ScriptData> scripts;
        public List<ScriptItemData> scriptItems;
        public List<SpriteFrameData> pictures;
        public List<PaletteData> commonPalettes;
        public List<SoundData> sounds;

        public int bgmId;
    }
}
