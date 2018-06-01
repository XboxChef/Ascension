namespace Ascension.Halo_Reach.Map_Package.Info
{
    using HaloReach3d.Helpers;
    using HaloReach3d.IO;
    using System;

    public class BLF_Header
    {
        private string _blfstring;
        private int _filecontentsize;
        private string _filedescription;
        private int _filesize;
        private int _headersize;
        private int _mapid;
        private short _unknown10;
        private short _unknown12;
        private byte[] _unknown52;
        private short _unknown8;

        public BLF_Header()
        {
        }

        public BLF_Header(EndianIO IO, int position)
        {
            IO.In.BaseStream.Position = position;
            this.BLF_String = IO.In.ReadAsciiString(4);
            this.HeaderSize = IO.In.ReadInt32();
            this.Unknown8 = IO.In.ReadInt16();
            this.Unknown10 = IO.In.ReadInt16();
            this.Unknown12 = IO.In.ReadInt16();
            this.FileDescription = ExtraFunctions.RemoveWhiteSpacingFromString(IO.In.ReadAsciiString(0x22));
            this.FileSize = IO.In.ReadInt32();
            this.Unknown52 = IO.In.ReadBytes(8);
            this.Map_ID = IO.In.ReadInt32();
            this.FileContentSize = IO.In.ReadInt32();
        }

        public string BLF_String
        {
            get
            {
                return this._blfstring;
            }
            set
            {
                this._blfstring = value;
            }
        }

        public int FileContentSize
        {
            get
            {
                return this._filecontentsize;
            }
            set
            {
                this._filecontentsize = value;
            }
        }

        public string FileDescription
        {
            get
            {
                return this._filedescription;
            }
            set
            {
                this._filedescription = value;
            }
        }

        public int FileSize
        {
            get
            {
                return this._filesize;
            }
            set
            {
                this._filesize = value;
            }
        }

        public int HeaderSize
        {
            get
            {
                return this._headersize;
            }
            set
            {
                this._headersize = value;
            }
        }

        public int Map_ID
        {
            get
            {
                return this._mapid;
            }
            set
            {
                this._mapid = value;
            }
        }

        public short Unknown10
        {
            get
            {
                return this._unknown10;
            }
            set
            {
                this._unknown10 = value;
            }
        }

        public short Unknown12
        {
            get
            {
                return this._unknown12;
            }
            set
            {
                this._unknown12 = value;
            }
        }

        public byte[] Unknown52
        {
            get
            {
                return this._unknown52;
            }
            set
            {
                this._unknown52 = value;
            }
        }

        public short Unknown8
        {
            get
            {
                return this._unknown8;
            }
            set
            {
                this._unknown8 = value;
            }
        }
    }
}

