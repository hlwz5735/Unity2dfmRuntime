using System;
using Data;
using UnityEngine;

namespace SpriteFrame
{
    public static class SpriteFrameUtil
    {
        public static Sprite GenSprite(SpriteFrameData data, PaletteData specifiedPalette = null)
        {
            var texture = new Texture2D(data.Width, data.Height);

            if (specifiedPalette == null && !data.HasPrivatePalette)
            {
                throw new Exception("没有调色盘信息！");
            }

            var palette = specifiedPalette ?? data.PrivatePalette;
            
            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    var idx = data.Bytes[y * data.Width + x];
                    texture.SetPixel(x, data.Height - y - 1, palette.Colors[idx]);
                }
            }
            texture.Apply();
            
            var sprite = Sprite.Create(
                texture, new Rect(0, 0, data.Width, data.Height), new Vector2(0.5f, 0.5f));
            sprite.name = "gen_sprite_" + data.Width + "_" + data.Height;
            return sprite;
        }
    }
}
