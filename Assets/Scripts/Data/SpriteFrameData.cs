namespace Data
{
    public class SpriteFrameData
    {
        public int offset { get; set; }
        public int index { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public PaletteData privatePalette;
        public int size { get; set; }
        
        public byte[] bytes { get; set; }

        public bool hasPrivatePalette => this.privatePalette != null;
        public bool compressed => size != 0;
        public int realSize {
            get
            {
                var s = 0;
                if (this.hasPrivatePalette)
                {
                    s += 1024;
                }

                s += width * height;
                return s;   
            }
        }

        public int unknownFlag1;
    }
}