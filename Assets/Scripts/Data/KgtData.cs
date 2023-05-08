using System.Collections.Generic;

namespace Data {
    /// <summary>
    /// KGT项目主配置文件数据
    /// </summary>
    public class KgtData {
        public string projectName;
        public List<ScriptData> scripts;
        public List<ScriptItemData> scriptItems;
        public List<SpriteFrameData> pictures;
        public List<PaletteData> commonPalettes;
        public List<SoundData> sounds;
        
        // public List<ReactionData> reactions;
        // public FreezeTimeSetting freezeTimeSetting;
        // public DemoSetting demoSetting;
        // public GeneralSetting generalSetting;
        // public List<ThrowReactionData> throwReactions;
        // public CharacterSelectionPositionSetting characterSelectionPositionSetting;
    }
}
