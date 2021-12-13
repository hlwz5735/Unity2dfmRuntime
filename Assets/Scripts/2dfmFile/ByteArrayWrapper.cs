using System;

namespace _2dfmFile
{
    public class ByteArrayWrapper
    {
        private byte[] bytes;
        private int offset;

        public byte[] Bytes => bytes;
        public int Offset => offset;

        public ByteArrayWrapper(byte[] bytes)
        {
            this.bytes = bytes;
            this.offset = 0;
        }

        public byte[] ReadBytes(int size)
        {
            var read = new byte[size];
            Array.Copy(this.bytes, offset, read, 0, size);
            this.offset += size;
            return read;
        }

        public byte ReadByte()
        {
            byte val = bytes[offset];
            offset += 1;
            return val;
        }

        public short ReadShort()
        {
            short val = BitConverter.ToInt16(bytes, offset);
            this.offset += 2;
            return val;
        }

        public int ReadInt()
        {
            int val = BitConverter.ToInt32(bytes, offset);
            this.offset += 4;
            return val;
        }
        
        public void Skip(int size)
        {
            this.offset += size;
        }

        public void SetOffset(int val)
        {
            this.offset = val;
        }
    }
}