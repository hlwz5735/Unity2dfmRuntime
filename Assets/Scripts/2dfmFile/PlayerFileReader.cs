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
        public static PlayerData Read2dfmPlayerFile(string path)
        {
            var buffer = File.ReadAllBytes(path);
            var wrapper = new ByteArrayWrapper(buffer);

            var player = new PlayerData();

            player.Signature = wrapper.ReadBytes(16);
            
            var nameBytes = wrapper.ReadBytes(256);
            player.Name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);

            // 脚本表数据
            ReadScriptTableData(wrapper, player);
            // 脚本项数据
            ReadScriptItemData(wrapper, player);
            // 将格子信息分配给脚本
            for (var i = 0; i < player.ScriptCount; i++)
            {
                var script = player.Scripts[i];
                if (i < player.ScriptCount - 1)
                {
                    script.ItemCount = player.Scripts[i + 1].ItemBeginIndex - script.ItemBeginIndex;
                }
                else
                {
                    script.ItemCount = player.ScriptItemCount - script.ItemBeginIndex;
                }

                script.Items = player.ScriptItems.GetRange(script.ItemBeginIndex, script.ItemCount);
            }
            // 精灵帧数据
            ReadSpriteFrameData(wrapper, player);
            // 公共调色盘数据
            ReadPublicPaletteData(wrapper, player);
            // 声音数据
            ReadSoundData(wrapper, player);
            return player;
        }

        private static void ReadScriptTableData(ByteArrayWrapper wrapper, PlayerData player)
        {
            player.ScriptCount = wrapper.ReadInt();
            var read = wrapper.ReadBytes(player.ScriptCount * 39);
            for (int i = 0; i < player.ScriptCount; i++)
            {
                var script = new ScriptData();
                int innerOffset = i * 39;
                var nameBytes = new byte[32];
                Array.Copy(read, innerOffset, nameBytes, 0, 32);
                script.Name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);
                script.ItemBeginIndex = BitConverter.ToUInt16(read, innerOffset + 32);
                script.UnknownFlag1 = read[innerOffset + 34];
                script.IsDefaultScript = read[innerOffset + 35] == 1;
                var unknownBytes = new byte[3];
                Array.Copy(read, innerOffset + 36, unknownBytes, 0, 3);
                script.UnknownBytes = unknownBytes;
                
                player.Scripts.Add(script);
            }
        }

        private static void ReadScriptItemData(ByteArrayWrapper wrapper, PlayerData player)
        {
            player.ScriptItemCount = wrapper.ReadInt();
            var read = wrapper.ReadBytes(player.ScriptItemCount * 16);
            for (int i = 0; i < player.ScriptItemCount; i++)
            {
                var scriptItem = new ScriptItemData();

                int innerOffset = i * 16;
                scriptItem.Type = read[innerOffset];
                var parameters = new byte[15];
                Array.Copy(read, innerOffset + 1, parameters, 0, 15);
                scriptItem.Parameters = parameters;
                
                player.ScriptItems.Add(scriptItem);
            }
        }

        private static void ReadSpriteFrameData(ByteArrayWrapper wrapper, PlayerData player)
        {
            player.SpriteFrameCount = wrapper.ReadInt();
            for (int i = 0; i < player.SpriteFrameCount; i++)
            {
                var frame = new SpriteFrameData();
                frame.Offset = wrapper.Offset;
                frame.Index = i;

                var headBytes = wrapper.ReadBytes(20);

                frame.UnknownFlag1 = BitConverter.ToInt32(headBytes, 0);
                frame.Width = BitConverter.ToInt32(headBytes, 4);
                frame.Height = BitConverter.ToInt32(headBytes, 8);
                
                var hasPrivatePalette = BitConverter.ToInt32(headBytes, 12) != 0;
                var realSize = frame.Width * frame.Height + (hasPrivatePalette ? 1024 : 0);
                
                frame.Size = BitConverter.ToInt32(headBytes, 16);

                // Size != 0 -> 数据已被压缩
                if (frame.Size != 0)
                {
                    var decompressed = DecompressUtil.Decompress(wrapper.ReadBytes(frame.Size), realSize); 
                    if (!hasPrivatePalette)
                    {
                        frame.Bytes = decompressed;
                    }
                    else
                    {
                        var paletteBytes = new byte[1024];
                        Array.Copy(decompressed, 0, paletteBytes, 0, 1024);
                        frame.PrivatePalette = PaletteUtil.ReadPalette(paletteBytes, true);
                        var frameBytes = new byte[decompressed.Length - 1024];
                        Array.Copy(decompressed, 1024, frameBytes, 0, decompressed.Length - 1024);
                        frame.Bytes = frameBytes;
                    }
                }
                // 数据未被压缩
                else
                {
                    var frameSize = frame.Width * frame.Height;
                    if (frameSize != 0)
                    {
                        if (hasPrivatePalette)
                        {
                            frame.PrivatePalette = PaletteUtil.ReadPalette(wrapper.ReadBytes(1024), true);
                        }
                        frame.Bytes = wrapper.ReadBytes(frameSize);
                    }
                }
                
                player.SpriteFrames.Add(frame);
            }
        }

        private static void ReadPublicPaletteData(ByteArrayWrapper wrapper, PlayerData player)
        {
            const int publicPaletteLength = 1024 + 32;
            for (int i = 0; i < 8; i++)
            {
                // 省略
                var buffer = wrapper.ReadBytes(publicPaletteLength);
                player.PublicPalettes[i] = PaletteUtil.ReadPalette(buffer, false);
            }
        }

        private static void ReadSoundData(ByteArrayWrapper wrapper, PlayerData player)
        {
            player.SoundCount = wrapper.ReadInt();
            for (int i = 0; i < player.SoundCount; i++)
            {
                var sound = new SoundData();
                sound.Offset = wrapper.Offset;
                var flagBytes = wrapper.ReadBytes(4);
                sound.UnknownFlag = BitConverter.ToInt32(flagBytes, 0);
                var nameBytes = wrapper.ReadBytes(32);
                sound.Name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);
                var tailBytes = wrapper.ReadBytes(6);
                sound.Size = BitConverter.ToInt32(tailBytes, 0);
                sound.UnknownFlag2 = BitConverter.ToInt16(tailBytes, 4);

                if (sound.Size != 0)
                {
                    sound.Bytes = wrapper.ReadBytes(sound.Size);
                }

                player.Sounds.Add(sound);
            }
        }
    }
}