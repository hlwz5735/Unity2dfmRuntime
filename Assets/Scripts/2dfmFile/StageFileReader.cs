using System.IO;
using Data;

namespace _2dfmFile
{
    public static class StageFileReader
    {
        public static StageData read2dfmStageFile(string path)
        {
            var buffer = File.ReadAllBytes(path);
            var wrapper = new ByteArrayWrapper(buffer);
            
            var stage = new StageData();
            
            wrapper.skip(16);
            
            var nameBytes = wrapper.readBytes(256);
            stage.name = CommonStructureUtil.getGbkStringFromBytes(nameBytes);
            
            // 脚本表数据
            stage.scripts = CommonStructureUtil.readScriptTableData(wrapper);
            // 脚本项数据
            stage.scriptItems = CommonStructureUtil.readScriptItemData(wrapper);
            // 将格子信息分配给脚本
            for (var i = 0; i < stage.scripts.Count; i++)
            {
                var script = stage.scripts[i];
                if (i < stage.scripts.Count - 1)
                {
                    script.itemCount = stage.scripts[i + 1].itemBeginIndex - script.itemBeginIndex;
                }
                else
                {
                    script.itemCount = stage.scriptItems.Count - script.itemBeginIndex;
                }

                script.items = stage.scriptItems.GetRange(script.itemBeginIndex, script.itemCount).ToArray();
            }
            // 精灵帧数据
            stage.pictures = CommonStructureUtil.readSpriteFrameData(wrapper);
            // 公共调色盘数据
            stage.commonPalettes = CommonStructureUtil.readPublicPaletteData(wrapper);
            // 声音数据
            stage.sounds = CommonStructureUtil.readSoundData(wrapper);

            wrapper.skip(4);
            
            stage.bgmId = wrapper.readInt();
            
            return stage;
        }
    }
}