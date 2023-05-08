using System;
using Data;
using UnityEngine;

namespace SpriteFrame
{
    public static class SpriteFrameUtil
    {
        private const int PADDING = 5;

        public static Sprite genSprite(SpriteFrameData data, PaletteData specifiedPalette = null)
        {
            // 必须添加合适的空隙，以便在边界不出现接缝
            var texture = new Texture2D(data.width + PADDING * 2, data.height + PADDING * 2);
            var transparentColor = new Color(0, 0, 0, 0);
            var initArr = new Color[texture.width * texture.height];
            for (int i = 0; i < initArr.Length; i++)
            {
                initArr[i] = transparentColor;
            }
            texture.SetPixelData(initArr, 0);

            // texture.SetPixels(0, 0, texture.width, texture.height, initArr);

            if (specifiedPalette == null && !data.hasPrivatePalette)
            {
                throw new Exception("没有调色盘信息！");
            }

            var palette = specifiedPalette ?? data.privatePalette;
            
            for (int y = 0; y < data.height; y++)
            {
                for (int x = 0; x < data.width; x++)
                {
                    var idx = data.bytes[y * data.width + x];
                    texture.SetPixel(x + PADDING, data.height - y - 1 + PADDING, palette.colors[idx]);
                }
            }
            texture.Apply();
            
            var sprite = Sprite.Create(
                texture, new Rect(PADDING, PADDING, data.width, data.height), new Vector2(0.5f, 0), 100);
            return sprite;
        }
    }
}
