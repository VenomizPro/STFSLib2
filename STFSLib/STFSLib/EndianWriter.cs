using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace STFSLib
{
    public class EndianWriter : BinaryWriter
    {
        public EndianType EndianType;

        public EndianWriter(byte[] Buffer, EndianType EndianType)
            : this(new MemoryStream(Buffer), EndianType)
        {
        }

        public EndianWriter(Stream Input, EndianType EndianType)
            : base(Input)
        {
            this.EndianType = EndianType;
        }

        public void SeekNWrite(long position, short Value)
        {
            base.BaseStream.Position = position;
            this.Write(Value);
        }

        public void SeekNWrite(long position, int Value)
        {
            base.BaseStream.Position = position;
            this.Write(Value);
        }

        public virtual void SeekTo(object Position)
        {
            base.BaseStream.Seek(Convert.ToInt64(Position), SeekOrigin.Begin);
        }

        public virtual void SeekTo(object offset, SeekOrigin SeekOrigin)
        {
            this.Seek(Convert.ToInt32(offset), SeekOrigin);
        }

        public override void Write(char[] Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(double Value)
        {
            this.Write(Value, this.EndianType);
        }

        public void Write(Image Image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Image.Save(stream, Image.RawFormat);
                base.Write(stream.ToArray());
            }
        }

        public override void Write(short Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(int Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(long Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(float Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(string Value)
        {
            base.Write(Encoding.ASCII.GetBytes(Value));
        }

        public override void Write(ushort Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(uint Value)
        {
            this.Write(Value, this.EndianType);
        }

        public override void Write(ulong Value)
        {
            this.Write(Value, this.EndianType);
        }

        public void Write(double Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public virtual void Write(byte[] Data, EndianType EndianType)
        {
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(Data);
            }
            base.Write(Data);
        }

        public void Write(char[] Value, EndianType EndianType)
        {
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(Value);
            }
            base.Write(Value);
        }

        public void Write(short Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public void Write(int Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public void Write(long Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public void Write(float Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public void Write(ushort Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public void Write(uint Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public void Write(ulong Value, EndianType EndianType)
        {
            this.Write(BitConverter.GetBytes(Value), EndianType);
        }

        public virtual void Write(byte[] Buffer, object BufferLength)
        {
            base.Write(Buffer, 0, Convert.ToInt32(BufferLength));
        }

        public virtual void Write(byte[] Buffer, object offset, object BufferLength)
        {
            base.Write(Buffer, Convert.ToInt32(offset), Convert.ToInt32(BufferLength));
        }

        public void WriteAsciiString(string String, int Length)
        {
            this.WriteAsciiString(String, Length, this.EndianType);
        }

        public void WriteAsciiString(string String, int Length, EndianType EndianType)
        {
            if (String.Length > Length)
            {
                String = String.Substring(0, Length);
            }
            this.Write(Encoding.ASCII.GetBytes(String));
            if (String.Length < Length)
            {
                this.Write(new byte[Length - String.Length]);
            }
        }

        public void WriteByte(object value)
        {
            long num = Convert.ToInt64(value);
            base.Write((byte)(num & 0xffL));
        }

        public void WriteInt24(int Value)
        {
            this.WriteInt24(Value, this.EndianType);
        }

        public void WriteInt24(int Value, EndianType EndianType)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Resize<byte>(ref bytes, 3);
            this.Write(bytes, EndianType);
        }

        public void WriteUInt24(uint value)
        {
            this.WriteUInt24(value, this.EndianType);
        }

        public void WriteUInt24(uint value, EndianType endianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Resize<byte>(ref bytes, 3);
            if (endianType == EndianType.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void WriteUnicodeNullTermString(string String)
        {
            int length = String.Length;
            for (int i = 0; i < length; i++)
            {
                ushort num3 = String[i];
                this.Write(num3, this.EndianType);
            }
            this.Write(new byte[2]);
        }

        public void WriteUnicodeString(string Value)
        {
            this.WriteUnicodeString(Value, Value.Length);
        }

        public void WriteUnicodeString(string Value, int Length)
        {
            int length = Value.Length;
            for (int i = 0; i < length; i++)
            {
                if (i > Length)
                {
                    break;
                }
                ushort num3 = Value[i];
                this.Write(num3, this.EndianType);
            }
            int num4 = (Length - length) * 2;
            if (num4 > 0)
            {
                this.Write(new byte[num4]);
            }
        }
    }
}
