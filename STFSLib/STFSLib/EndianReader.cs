using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace STFSLib
{
    public class EndianReader : BinaryReader
    {
        public EndianType EndianType;

        public EndianReader(byte[] Data, EndianType EndianType)
            : this(new MemoryStream(Data), EndianType)
        {
        }

        public EndianReader(Stream Input, EndianType EndianType)
            : base(Input)
        {
            this.EndianType = EndianType;
        }

        public Image ParseImage(int Size)
        {
            using (MemoryStream stream = new MemoryStream(base.ReadBytes(Size)))
            {
                return Image.FromStream(stream);
            }
        }

        public override int Read(byte[] buffer, int index, int count)
        {
            return this.BaseStream.Read(buffer, index, count);
        }

        public string ReadAsciiString(int Length)
        {
            string str = string.Empty;
            int num = 0;
            for (int i = 0; i < Length; i++)
            {
                char ch = (char)this.ReadByte();
                num++;
                if (ch == '\0')
                {
                    break;
                }
                str = str + ch;
            }
            this.BaseStream.Seek((long)(Length - num), SeekOrigin.Current);
            return str;
        }

        public override byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            this.Read(buffer, 0, count);
            return buffer;
        }

        public byte[] ReadBytes(object count)
        {
            byte[] buffer = new byte[Convert.ToInt32(count)];
            this.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        public byte[] ReadBytes(object Count, EndianType EndianType)
        {
            byte[] array = base.ReadBytes(Convert.ToInt32(Count));
            if (EndianType == EndianType.BigEndian)
            {
                Array.Reverse(array);
            }
            return array;
        }

        public override double ReadDouble()
        {
            return this.ReadDouble(this.EndianType);
        }

        public double ReadDouble(EndianType EndianType)
        {
            return BitConverter.ToDouble(this.ReadBytes(8, EndianType), 0);
        }

        public override short ReadInt16()
        {
            return this.ReadInt16(this.EndianType);
        }

        public short ReadInt16(EndianType EndianType)
        {
            return BitConverter.ToInt16(this.ReadBytes(2, EndianType), 0);
        }

        public int ReadInt24()
        {
            return this.ReadInt24(this.EndianType);
        }

        public int ReadInt24(EndianType EndianType)
        {
            byte[] buffer = base.ReadBytes(3);
            if (EndianType == EndianType.BigEndian)
            {
                return (((buffer[0] << 0x10) | (buffer[1] << 8)) | buffer[2]);
            }
            return (((buffer[2] << 0x10) | (buffer[1] << 8)) | buffer[0]);
        }

        public override int ReadInt32()
        {
            return this.ReadInt32(this.EndianType);
        }

        public int ReadInt32(EndianType EndianType)
        {
            return BitConverter.ToInt32(this.ReadBytes(4, EndianType), 0);
        }

        public override long ReadInt64()
        {
            return this.ReadInt64(this.EndianType);
        }

        public long ReadInt64(EndianType EndianType)
        {
            return BitConverter.ToInt64(this.ReadBytes(8, EndianType), 0);
        }

        public string ReadNullTerminatedString()
        {
            char ch;
            string str = string.Empty;
            while ((ch = base.ReadChar()) != '\0')
            {
                if (ch == '\0')
                {
                    return str;
                }
                str = str + ch;
            }
            return str;
        }

        public override float ReadSingle()
        {
            return this.ReadSingle(this.EndianType);
        }

        public float ReadSingle(EndianType EndianType)
        {
            return BitConverter.ToSingle(this.ReadBytes(4, EndianType), 0);
        }

        public string ReadString(int Length)
        {
            return Encoding.ASCII.GetString(base.ReadBytes(Length)).Replace("\0", string.Empty);
        }

        public string ReadStringNullTerminated()
        {
            byte num;
            string str = string.Empty;
        Label_0015:
            num = this.ReadByte();
            if (num != 0)
            {
                str = str + ((char)num);
                goto Label_0015;
            }
            return str;
        }

        public override ushort ReadUInt16()
        {
            return this.ReadUInt16(this.EndianType);
        }

        public ushort ReadUInt16(EndianType EndianType)
        {
            return BitConverter.ToUInt16(this.ReadBytes(2, EndianType), 0);
        }

        public uint ReadUInt24()
        {
            return this.ReadUInt24(this.EndianType);
        }

        public uint ReadUInt24(EndianType EndianType)
        {
            return (uint)this.ReadInt24(EndianType);
        }

        public override uint ReadUInt32()
        {
            return this.ReadUInt32(this.EndianType);
        }

        public uint ReadUInt32(EndianType EndianType)
        {
            return BitConverter.ToUInt32(this.ReadBytes(4, EndianType), 0);
        }

        public override ulong ReadUInt64()
        {
            return this.ReadUInt64(this.EndianType);
        }

        public ulong ReadUInt64(EndianType EndianType)
        {
            return BitConverter.ToUInt64(this.ReadBytes(8, EndianType), 0);
        }

        public string ReadUnicodeNullTermString()
        {
            ushort num;
            string str = string.Empty;
            num = this.ReadUInt16(EndianType.BigEndian);
            if (num != 0)
            {
                str = str + ((char)num);
            }
            return str;
        }

        public string ReadUnicodeString(int Length)
        {
            return Encoding.BigEndianUnicode.GetString(base.ReadBytes(Length * 2)).Replace("\0", string.Empty);
        }

        public ushort SeekNReadUInt16(long Address)
        {
            base.BaseStream.Position = Address;
            return this.ReadUInt16();
        }

        public uint SeekNReadUInt32(long Address)
        {
            base.BaseStream.Position = Address;
            return this.ReadUInt32();
        }

        public void SeekTo(object Position)
        {
            this.SeekTo(Position, SeekOrigin.Begin);
        }

        public virtual void SeekTo(object offset, SeekOrigin SeekOrigin)
        {
            this.BaseStream.Seek(Convert.ToInt64(offset), SeekOrigin);
        }
    }
}
