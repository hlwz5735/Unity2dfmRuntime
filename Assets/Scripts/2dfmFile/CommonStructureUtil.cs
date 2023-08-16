using System;
using System.Collections.Generic;
using Data;

namespace _2dfmFile
{
    public static class CommonStructureUtil
    {
        
        public static List<ScriptData> readScriptTableData(ByteArrayWrapper wrapper)
        {
            var scriptCount = wrapper.readInt();
            var list = new List<ScriptData>(scriptCount);
            var read = wrapper.readBytes(scriptCount * 39);
            for (int i = 0; i < scriptCount; i++)
            {
                var script = new ScriptData();
                int innerOffset = i * 39;
                var nameBytes = new byte[32];
                Array.Copy(read, innerOffset, nameBytes, 0, 32);
                script.name = getGbkStringFromBytes(nameBytes);
                script.itemBeginIndex = BitConverter.ToUInt16(read, innerOffset + 32);
                script.unknownFlag1 = read[innerOffset + 34];
                script.specialFlag = BitConverter.ToInt32(read, innerOffset + 35);
                list.Add(script);
            }

            return list;
        }

        public static List<ScriptItemData> readScriptItemData(ByteArrayWrapper wrapper)
        {
            var scriptItemCount = wrapper.readInt();
            var list = new List<ScriptItemData>(scriptItemCount);
            var read = wrapper.readBytes(scriptItemCount * 16);
            for (int i = 0; i < scriptItemCount; i++)
            {
                var scriptItem = new ScriptItemData();

                int innerOffset = i * 16;
                scriptItem.type = read[innerOffset];
                var parameters = new byte[15];
                Array.Copy(read, innerOffset + 1, parameters, 0, 15);
                scriptItem.parameters = parameters;
                
                list.Add(scriptItem);
            }

            return list;
        }

        public static List<SpriteFrameData> readSpriteFrameData(ByteArrayWrapper wrapper)
        {
            var spriteFrameCount = wrapper.readInt();
            var list = new List<SpriteFrameData>(spriteFrameCount);
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
                
                list.Add(frame);
            }
            return list;
        }

        public static List<PaletteData> readPublicPaletteData(ByteArrayWrapper wrapper)
        {
            var list = new List<PaletteData>(8);
            const int publicPaletteLength = 1024 + 32;
            for (int i = 0; i < 8; i++)
            {
                // 省略
                var buffer = wrapper.readBytes(publicPaletteLength);
                list.Add(PaletteUtil.readPalette(buffer, false));
            }

            return list;
        }

        public static List<SoundData> readSoundData(ByteArrayWrapper wrapper)
        {
            var soundCount = wrapper.readInt();
            var list = new List<SoundData>(soundCount);
            for (int i = 0; i < soundCount; i++)
            {
                var sound = new SoundData();
                sound.offset = wrapper.offset;
                var flagBytes = wrapper.readBytes(4);
                sound.unknownFlag = BitConverter.ToInt32(flagBytes, 0);
                var nameBytes = wrapper.readBytes(32);
                sound.name = getGbkStringFromBytes(nameBytes);
                var tailBytes = wrapper.readBytes(6);
                sound.size = BitConverter.ToInt32(tailBytes, 0);
                sound.unknownFlag2 = BitConverter.ToInt16(tailBytes, 4);

                if (sound.size != 0)
                {
                    sound.bytes = wrapper.readBytes(sound.size);
                }

                list.Add(sound);
            }

            return list;
        }

        public static string getGbkStringFromBytes(byte[] bytes)
        {
            int i = 0;
            for (; i < bytes.Length; i++)
            {
                if (bytes[i] == '\0')
                {
                    break;
                }
            }
            return System.Text.Encoding.GetEncoding("GBK").GetString(bytes, 0, i);
        }
    }
}
