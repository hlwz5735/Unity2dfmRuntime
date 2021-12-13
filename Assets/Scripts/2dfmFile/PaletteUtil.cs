using System;
using Data;

namespace _2dfmFile
{
    public static class PaletteUtil
    {
        public static PaletteData ReadPalette(byte[] buffer, bool isPrivate)
        {
            var palette = new PaletteData();

            palette.IsPrivate = isPrivate;

            for (int i = 0; i < 1024; i += 4)
            {
                palette.PushBgra(buffer, i);
            }

            if (!isPrivate)
            {
                var gapBytes = new byte[32];
                Array.Copy(buffer, 1024, gapBytes, 0, 32);
                palette.UnknownGap = gapBytes;
            }
            
            return palette;
        }
    }
}