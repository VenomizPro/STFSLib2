using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace STFSLib
{
    public class EndianIO : IDisposable
    {

        private bool bool_0;

        private EndianReader endianReader_0;
        private EndianType endianType_0;

        private EndianWriter endianWriter_0;
        internal FileAccess fileAccess_0;
        internal FileMode fileMode_0;
        internal FileShare fileShare_0;

        private Stream stream_0;

        private string string_0;

        public EndianIO()
        {
        }

        public EndianIO(string FileName, EndianType EndianType)
            : this(FileName, EndianType, false)
        {
        }

        public EndianIO(byte[] ByteArray, EndianType EndianType)
            : this(ByteArray, EndianType, false)
        {
        }

        public EndianIO(Stream Stream, EndianType EndianType)
            : this(Stream, EndianType, false)
        {
        }

        public EndianIO(Stream Stream, EndianType EndianType, bool Open)
        {
            this.EndianType = EndianType;
            this.Stream = Stream;
            if (Open)
            {
                this.Open();
            }
        }

        public EndianIO(string FileName, EndianType EndianType, bool Open)
        {
            this.fileMode_0 = FileMode.OpenOrCreate;
            this.fileAccess_0 = FileAccess.ReadWrite;
            this.fileShare_0 = FileShare.Read;
            this.EndianType = EndianType;
            this.FileName = FileName;
            if (Open)
            {
                this.Open();
            }
        }

        public EndianIO(byte[] ByteArray, EndianType EndianType, bool Open)
        {
            this.EndianType = EndianType;
            this.Stream = new MemoryStream(ByteArray);
            if (Open)
            {
                this.Open();
            }
        }

        public EndianIO(string FileName, EndianType EndianType, FileMode FileMode, FileAccess FileAccess, FileShare FileShare)
        {
            this.fileMode_0 = FileMode;
            this.fileAccess_0 = FileAccess;
            this.fileShare_0 = FileShare;
            this.EndianType = EndianType;
            this.FileName = FileName;
        }

        public virtual void Close()
        {
            if (this.Opened)
            {
                if (this.Stream != null)
                {
                    this.Stream.Dispose();
                }
                if (this.In != null)
                {
                    this.In.Close();
                }
                if (this.Out != null)
                {
                    this.Out.Close();
                }
                this.Opened = false;
            }
        }

        public void Dispose()
        {
            this.Close();
        }

        ~EndianIO()
        {
            try
            {
                this.Close();
            }
            catch
            {
            }
        }

        public virtual void Open()
        {
            if (this.Opened)
            {
                this.Close();
            }
            if (this.Stream != null)
            {
                if (this.Stream.CanRead)
                {
                    this.In = new EndianReader(this.Stream, this.EndianType);
                }
                if (this.Stream.CanWrite)
                {
                    this.Out = new EndianWriter(this.Stream, this.EndianType);
                }
                this.Opened = true;
            }
            if (this.FileName != null)
            {
                this.Stream = new FileStream(this.FileName, this.fileMode_0, this.fileAccess_0, this.fileShare_0);
                if ((this.fileAccess_0 == FileAccess.Read) || (this.fileAccess_0 == FileAccess.ReadWrite))
                {
                    this.In = new EndianReader(this.Stream, this.EndianType);
                }
                if ((this.fileAccess_0 == FileAccess.Write) || (this.fileAccess_0 == FileAccess.ReadWrite))
                {
                    this.Out = new EndianWriter(this.Stream, this.EndianType);
                }
                this.Opened = true;
            }
        }

        public virtual void SeekTo(object Position)
        {
            this.SeekTo(Convert.ToInt64(Position), SeekOrigin.Begin);
        }

        public virtual void SeekTo(long Position, SeekOrigin Origin)
        {
            this.Stream.Seek(Position, Origin);
        }

        public byte[] ToArray()
        {
            if (this.Stream.GetType() == typeof(STFSFileStream))
            {
                return ((STFSFileStream)this.Stream).ToArray();
            }
            if (this.Stream.GetType() == typeof(FileStream))
            {
                this.Stream.Position = 0L;
                byte[] buffer = new byte[this.Stream.Length];
                this.Stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
            return ((MemoryStream)this.Stream).ToArray();
        }

        public override string ToString()
        {
            return Path.GetFileName(this.FileName);
        }

        public EndianType EndianType
        {
            get
            {
                return this.endianType_0;
            }
            set
            {
                this.endianType_0 = value;
                if (this.In != null)
                {
                    this.In.EndianType = value;
                }
                if (this.Out != null)
                {
                    this.Out.EndianType = value;
                }
            }
        }

        public virtual string FileName
        {

            get
            {
                return this.string_0;
            }

            protected internal set
            {
                this.string_0 = value;
            }
        }

        public EndianReader In
        {

            get
            {
                return this.endianReader_0;
            }

            protected internal set
            {
                this.endianReader_0 = value;
            }
        }

        public virtual long Length
        {
            get
            {
                return this.Stream.Length;
            }
        }

        public bool Opened
        {

            get
            {
                return this.bool_0;
            }

            protected internal set
            {
                this.bool_0 = value;
            }
        }

        public EndianWriter Out
        {

            get
            {
                return this.endianWriter_0;
            }

            protected internal set
            {
                this.endianWriter_0 = value;
            }
        }

        public virtual long Position
        {
            get
            {
                return this.Stream.Position;
            }
            set
            {
                this.Stream.Position = value;
            }
        }

        public virtual Stream Stream
        {

            get
            {
                return this.stream_0;
            }

            protected internal set
            {
                this.stream_0 = value;
            }
        }
    }
}
