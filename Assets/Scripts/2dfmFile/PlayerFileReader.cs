using System;
using System.IO;
using Data;

namespace _2dfmFile
{
    public class PlayerFileReader
    {
        public static PlayerData Read2dfmPlayerFile(string path)
        {
            var buffer = File.ReadAllBytes(path);
            var wrapper = new ByteArrayWrapper(buffer);

            var player = new PlayerData();

            player.Signature = wrapper.ReadBytes(16);
            
            var nameBytes = wrapper.ReadBytes(256);
            player.Name = System.Text.Encoding.GetEncoding("GBK").GetString(nameBytes);

            player.ScriptCount = wrapper.ReadInt();

            var read = wrapper.ReadBytes(player.ScriptCount * 39);
            
            // 跳过内部内容读取
            
            player.ScriptItemCount = wrapper.ReadInt();

            read = wrapper.ReadBytes(player.ScriptItemCount * 16);
            
            // 跳过内部内容读取

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
                frame.HasPrivatePalette = BitConverter.ToInt32(headBytes, 12) != 0;
                frame.Size = BitConverter.ToInt32(headBytes, 16);

                if (frame.Size != 0)
                {
                    wrapper.Skip(frame.Size);
                }
                else
                {
                    var frameSize = frame.Width * frame.Height;
                    if (frameSize != 0)
                    {
                        wrapper.Skip(frameSize);
                        if (frame.HasPrivatePalette)
                        {
                            wrapper.Skip(1024);
                        }
                    }
                }
                
                player.SpriteFrames.Add(frame);
            }

            const int publicPaletteLength = 1024 + 32;
            var paletteBuffer = wrapper.ReadBytes(publicPaletteLength * 8);
            for (int i = 0; i < 8; i++)
            {
                // 省略
            }

            player.SoundCount = wrapper.ReadInt();
            for (int i = 0; i < player.ScriptCount; i++)
            {
                // 省略
            }
            
            return player;
        }
    }
}