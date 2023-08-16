using System;
using System.IO;
using Data;
using UnityEngine;

namespace _2dfmFile
{
    public static class PlayerFileReader
    {
        /// <summary>
        /// 按路径读取玩家信息
        ///
        /// 注意，不得改变读取的顺序，否则解析必定失败！
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static PlayerData read2dfmPlayerFile(string path)
        {
            var buffer = File.ReadAllBytes(path);
            var wrapper = new ByteArrayWrapper(buffer);

            var player = new PlayerData();

            // player.signature = wrapper.readBytes(16);
            // 跳过签名
            wrapper.skip(16);
            
            var nameBytes = wrapper.readBytes(256);
            player.name = CommonStructureUtil.getGbkStringFromBytes(nameBytes);

            // 脚本表数据
            player.scripts = CommonStructureUtil.readScriptTableData(wrapper);
            // 脚本项数据
            player.scriptItems = CommonStructureUtil.readScriptItemData(wrapper);
            // 将格子信息分配给脚本
            for (var i = 0; i < player.scriptCount; i++)
            {
                var script = player.scripts[i];
                if (i < player.scriptCount - 1)
                {
                    script.itemCount = player.scripts[i + 1].itemBeginIndex - script.itemBeginIndex;
                }
                else
                {
                    script.itemCount = player.scriptItemCount - script.itemBeginIndex;
                }

                script.items = player.scriptItems.GetRange(script.itemBeginIndex, script.itemCount).ToArray();
            }
            // 精灵帧数据
            player.pictures = CommonStructureUtil.readSpriteFrameData(wrapper);
            // 公共调色盘数据
            player.publicPalettes = CommonStructureUtil.readPublicPaletteData(wrapper).ToArray();
            // 声音数据
            player.sounds = CommonStructureUtil.readSoundData(wrapper);
            return player;
        }
    }
}