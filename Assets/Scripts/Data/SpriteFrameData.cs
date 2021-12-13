namespace Data
{
    public class SpriteFrameData
    {
        public int Offset { get; set; }
        public int Index { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public PaletteData PrivatePalette;
        public int Size { get; set; }
        
        public byte[] Bytes { get; set; }

        public bool HasPrivatePalette => this.PrivatePalette != null;
        public bool Compressed => Size != 0;
        public int RealSize {
            get
            {
                var size = 0;
                if (this.HasPrivatePalette)
                {
                    size += 1024;
                }

                size += Width * Height;
                return size;   
            }
        }

        public int UnknownFlag1;
    }
}