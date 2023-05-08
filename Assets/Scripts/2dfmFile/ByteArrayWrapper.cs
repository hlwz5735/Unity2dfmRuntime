using System;

namespace _2dfmFile
{
    public class ByteArrayWrapper
    {
        public readonly byte[] bytes;
        private int _offset;

        public int offset => _offset;

        public ByteArrayWrapper(byte[] bytes)
        {
            this.bytes = bytes;
            this._offset = 0;
        }

        public byte[] readBytes(int size)
        {
            var read = new byte[size];
            Array.Copy(this.bytes, _offset, read, 0, size);
            this._offset += size;
            return read;
        }

        public byte readByte()
        {
            byte val = bytes[_offset];
            _offset += 1;
            return val;
        }

        public short readShort()
        {
            short val = BitConverter.ToInt16(bytes, _offset);
            this._offset += 2;
            return val;
        }

        public int readInt()
        {
            int val = BitConverter.ToInt32(bytes, _offset);
            this._offset += 4;
            return val;
        }
        
        public void skip(int size)
        {
            this._offset += size;
        }

        public void setOffset(int val)
        {
            this._offset = val;
        }
    }
}