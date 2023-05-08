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
            player.name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);

            // 脚本表数据
            readScriptTableData(wrapper, player);
            // 脚本项数据
            readScriptItemData(wrapper, player);
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
            readSpriteFrameData(wrapper, player);
            // 公共调色盘数据
            readPublicPaletteData(wrapper, player);
            // 声音数据
            readSoundData(wrapper, player);
            return player;
        }

        private static void readScriptTableData(ByteArrayWrapper wrapper, PlayerData player)
        {
            var scriptCount = wrapper.readInt();
            var read = wrapper.readBytes(scriptCount * 39);
            for (int i = 0; i < scriptCount; i++)
            {
                var script = new ScriptData();
                int innerOffset = i * 39;
                var nameBytes = new byte[32];
                Array.Copy(read, innerOffset, nameBytes, 0, 32);
                script.name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);
                script.itemBeginIndex = BitConverter.ToUInt16(read, innerOffset + 32);
                script.unknownFlag1 = read[innerOffset + 34];
                script.specialFlag = BitConverter.ToInt32(read, innerOffset + 35);
                player.scripts.Add(script);
            }
        }

        private static void readScriptItemData(ByteArrayWrapper wrapper, PlayerData player)
        {
            var scriptItemCount = wrapper.readInt();
            var read = wrapper.readBytes(scriptItemCount * 16);
            for (int i = 0; i < scriptItemCount; i++)
            {
                var scriptItem = new ScriptItemData();

                int innerOffset = i * 16;
                scriptItem.type = read[innerOffset];
                var parameters = new byte[15];
                Array.Copy(read, innerOffset + 1, parameters, 0, 15);
                scriptItem.parameters = parameters;
                
                player.scriptItems.Add(scriptItem);
            }
        }

        private static void readSpriteFrameData(ByteArrayWrapper wrapper, PlayerData player)
        {
            var spriteFrameCount = wrapper.readInt();
            for (int i = 0; i < spriteFrameCount; i++)
            {
                var frame = new SpriteFrameData();
                frame.offset = wrapper.offset;
                frame.index = i;

                var headBytes = wrapper.readBytes(20);

                frame.unknownFlag1 = BitConverter.ToInt32(headBytes, 0);
                frame.width = BitConverter.ToInt32(headBytes, 4);
                frame.height = BitConverter.ToInt32(headBytes, 8);
                
                var hasPrivatePalette = BitConverter.ToInt32(headBytes, 12) != 0;
                var realSize = frame.width * frame.height + (hasPrivatePalette ? 1024 : 0);
                
                frame.size = BitConverter.ToInt32(headBytes, 16);

                // Size != 0 -> 数据已被压缩
                if (frame.size != 0)
                {
                    var decompressed = DecompressUtil.decompress(wrapper.readBytes(frame.size), realSize); 
                    if (!hasPrivatePalette)
                    {
                        frame.bytes = decompressed;
                    }
                    else
                    {
                        var paletteBytes = new byte[1024];
                        Array.Copy(decompressed, 0, paletteBytes, 0, 1024);
                        frame.privatePalette = PaletteUtil.readPalette(paletteBytes, true);
                        var frameBytes = new byte[decompressed.Length - 1024];
                        Array.Copy(decompressed, 1024, frameBytes, 0, decompressed.Length - 1024);
                        frame.bytes = frameBytes;
                    }
                }
                // 数据未被压缩
                else
                {
                    var frameSize = frame.width * frame.height;
                    if (frameSize != 0)
                    {
                        if (hasPrivatePalette)
                        {
                            frame.privatePalette = PaletteUtil.readPalette(wrapper.readBytes(1024), true);
                        }
                        frame.bytes = wrapper.readBytes(frameSize);
                    }
                }
                
                player.pictures.Add(frame);
            }
        }

        private static void readPublicPaletteData(ByteArrayWrapper wrapper, PlayerData player)
        {
            const int publicPaletteLength = 1024 + 32;
            for (int i = 0; i < 8; i++)
            {
                // 省略
                var buffer = wrapper.readBytes(publicPaletteLength);
                player.publicPalettes[i] = PaletteUtil.readPalette(buffer, false);
            }
        }

        private static void readSoundData(ByteArrayWrapper wrapper, PlayerData player)
        {
            var soundCount = wrapper.readInt();
            for (int i = 0; i < soundCount; i++)
            {
                var sound = new SoundData();
                sound.offset = wrapper.offset;
                var flagBytes = wrapper.readBytes(4);
                sound.unknownFlag = BitConverter.ToInt32(flagBytes, 0);
                var nameBytes = wrapper.readBytes(32);
                sound.name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);
                var tailBytes = wrapper.readBytes(6);
                sound.size = BitConverter.ToInt32(tailBytes, 0);
                sound.unknownFlag2 = BitConverter.ToInt16(tailBytes, 4);

                if (sound.size != 0)
                {
                    sound.bytes = wrapper.readBytes(sound.size);
                }

                player.sounds.Add(sound);
            }
        }
    }
}