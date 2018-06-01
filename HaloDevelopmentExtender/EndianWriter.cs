namespace HaloDevelopmentExtender
{
    using System;
    using System.IO;

    public class EndianWriter : BinaryWriter
    {
        private readonly EndianType endianstyle;

        public EndianWriter(Stream stream, EndianType endianstyle) : base(stream)
        {
            this.endianstyle = endianstyle;
        }

        public void SeekTo(int offset)
        {
            this.SeekTo(offset, SeekOrigin.Begin);
        }

        public void SeekTo(long offset)
        {
            this.SeekTo((int) offset, SeekOrigin.Begin);
        }

        public void SeekTo(uint offset)
        {
            this.SeekTo((int) offset, SeekOrigin.Begin);
        }

        public void SeekTo(int offset, SeekOrigin SeekOrigin)
        {
            this.BaseStream.Seek((long) offset, SeekOrigin);
        }

        public override void Write(double value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(short value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(int value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(long value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(float value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(ushort value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(uint value)
        {
            this.Write(value, this.endianstyle);
        }

        public override void Write(ulong value)
        {
            this.Write(value, this.endianstyle);
        }

        public void Write(double value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(short value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(int value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(long value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(float value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(ushort value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(uint value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(ulong value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void WriteAsciiString(string String, int Length)
        {
            this.WriteAsciiString(String, Length, this.endianstyle);
        }

        public void WriteAsciiString(string String, int Length, EndianType EndianType)
        {
            int length = String.Length;
            for (int i = 0; i < length; i++)
            {
                if (i > Length)
                {
                    break;
                }
                byte num3 = (byte) String[i];
                this.Write(num3);
            }
            int num4 = Length - length;
            if (num4 > 0)
            {
                this.Write(new byte[num4]);
            }
        }

        public void WriteUnicodeString(string String, int Length)
        {
            this.WriteUnicodeString(String, Length, this.endianstyle);
        }

        public void WriteUnicodeString(string String, int Length, EndianType EndianType)
        {
            int length = String.Length;
            for (int i = 0; i < length; i++)
            {
                if (i > Length)
                {
                    break;
                }
                ushort num3 = String[i];
                this.Write(num3, EndianType);
            }
            int num4 = (Length - length) * 2;
            if (num4 > 0)
            {
                this.Write(new byte[num4]);
            }
        }
    }
}

