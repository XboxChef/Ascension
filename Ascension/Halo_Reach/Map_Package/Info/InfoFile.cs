namespace Ascension.Halo_Reach.Map_Package.Info
{
    using Ascension.Halo_Reach.Map_Package.Package_Classes;
    using HaloReach3d.IO;
    using System;
    using System.IO;

    public class InfoFile
    {
        private BLF_Header _blfheader;
        private string _chinesedescription;
        private string _chinesename;
        private string _englishdescription;
        private string _englishname;
        private EOF_Footer _eoffooter;
        private string _frenchdescription;
        private string _frenchname;
        private string _germandescription;
        private string _germanname;
        private string _italiandescription;
        private string _italianname;
        private string _japanesedescription;
        private string _japanesename;
        private string _koreandescription;
        private string _koreanname;
        private string _latinamericanricaspanishdescription;
        private string _latinamericaspanishname;
        private string _mapfilename;
        private string _mapimagefilename;
        private string _portuguesedescription;
        private string _portuguesename;
        private string _spanishdescription;
        private string _spanishname;

        public InfoFile()
        {
        }

        public InfoFile(Stream stream)
        {
            EndianIO iO = new EndianIO(stream, EndianType.BigEndian);
            iO.Open();
            this.BLFHeader = new BLF_Header(iO, 0);
            iO.In.BaseStream.Position = 0x44L;
            this.EnglishName = iO.In.ReadUnicodeString(0x20);
            this.JapaneseName = iO.In.ReadUnicodeString(0x20);
            this.GermanName = iO.In.ReadUnicodeString(0x20);
            this.FrenchName = iO.In.ReadUnicodeString(0x20);
            this.SpanishName = iO.In.ReadUnicodeString(0x20);
            this.LatinAmericaSpanishName = iO.In.ReadUnicodeString(0x20);
            this.ItalianName = iO.In.ReadUnicodeString(0x20);
            this.KoreanName = iO.In.ReadUnicodeString(0x20);
            this.ChineseName = iO.In.ReadUnicodeString(0x20);
            iO.In.ReadBytes(0x40);
            this.PortugueseName = iO.In.ReadUnicodeString(0x20);
            iO.In.ReadBytes(0x40);
            this.EnglishDescription = iO.In.ReadUnicodeString(0x80);
            this.JapaneseDescription = iO.In.ReadUnicodeString(0x80);
            this.GermanDescription = iO.In.ReadUnicodeString(0x80);
            this.FrenchDescription = iO.In.ReadUnicodeString(0x80);
            this.SpanishDescription = iO.In.ReadUnicodeString(0x80);
            this.LatinAmericanSpanishDescription = iO.In.ReadUnicodeString(0x80);
            this.ItalianDescription = iO.In.ReadUnicodeString(0x80);
            this.KoreanDescription = iO.In.ReadUnicodeString(0x80);
            this.ChineseDescription = iO.In.ReadUnicodeString(0x80);
            iO.In.ReadBytes(0x100);
            this.PortugueseDescription = iO.In.ReadUnicodeString(0x80);
            iO.In.ReadBytes(0x100);
            this.MapImageFileName = iO.In.ReadAsciiString(0x100);
            this.MapFileName = iO.In.ReadAsciiString(0x100);
            iO.In.ReadBytes(0xbb74);
            this.EOFFooter = new EOF_Footer(iO, (int) iO.In.BaseStream.Position);
            iO.Close();
        }

        public void SaveInfoFile(Stream stream)
        {
            EndianIO nio = new EndianIO(stream, EndianType.BigEndian);
            nio.Open();
            nio.Out.WriteAsciiString("_blf", 4);
            nio.Out.Write(0x30);
            nio.Out.Write((short) 1);
            nio.Out.Write((short) 2);
            nio.Out.Write((short) (-2));
            nio.Out.Write(new byte[0x22]);
            nio.Out.WriteAsciiString("levl", 4);
            nio.Out.Write((short) 0);
            nio.Out.Write(new byte[] { 0x4d, 80, 0, 3 });
            nio.Out.Write((short) 1);
            nio.Out.Write(this.BLFHeader.Map_ID);
            nio.Out.Write(0x4c);
            nio.Out.BaseStream.Position = 0x44L;
            nio.Out.WriteUnicodeString(this.EnglishName, 0x20);
            nio.Out.WriteUnicodeString(this.JapaneseName, 0x20);
            nio.Out.WriteUnicodeString(this.GermanName, 0x20);
            nio.Out.WriteUnicodeString(this.FrenchName, 0x20);
            nio.Out.WriteUnicodeString(this.SpanishName, 0x20);
            nio.Out.WriteUnicodeString(this.LatinAmericaSpanishName, 0x20);
            nio.Out.WriteUnicodeString(this.ItalianName, 0x20);
            nio.Out.WriteUnicodeString(this.KoreanName, 0x20);
            nio.Out.WriteUnicodeString(this.ChineseName, 0x20);
            nio.Out.Write(new byte[0x40]);
            nio.Out.WriteUnicodeString(this.PortugueseName, 0x20);
            nio.Out.Write(new byte[0x40]);
            nio.Out.WriteUnicodeString(this.EnglishDescription, 0x80);
            nio.Out.WriteUnicodeString(this.JapaneseDescription, 0x80);
            nio.Out.WriteUnicodeString(this.GermanDescription, 0x80);
            nio.Out.WriteUnicodeString(this.FrenchDescription, 0x80);
            nio.Out.WriteUnicodeString(this.SpanishDescription, 0x80);
            nio.Out.WriteUnicodeString(this.LatinAmericanSpanishDescription, 0x80);
            nio.Out.WriteUnicodeString(this.ItalianDescription, 0x80);
            nio.Out.WriteUnicodeString(this.KoreanDescription, 0x80);
            nio.Out.WriteUnicodeString(this.ChineseDescription, 0x80);
            nio.Out.Write(new byte[0x100]);
            nio.Out.WriteUnicodeString(this.PortugueseDescription, 0x80);
            nio.Out.Write(new byte[0x100]);
            nio.Out.WriteAsciiString(this.MapImageFileName, 0x100);
            nio.Out.WriteAsciiString(this.MapFileName, 0x100);
            nio.Out.Write(new byte[0x3c3c]);
            int position = (int) nio.Stream.Position;
            nio.Out.WriteAsciiString("_eof", 4);
            nio.Out.Write(0x111);
            nio.Out.Write(0x10001);
            nio.Out.Write(position);
            nio.Out.Write((byte) 3);
            nio.Out.Write(new byte[0x100]);
            nio.Close();
        }

        public BLF_Header BLFHeader
        {
            get
            {
                return this._blfheader;
            }
            set
            {
                this._blfheader = value;
            }
        }

        public string ChineseDescription
        {
            get
            {
                return this._chinesedescription;
            }
            set
            {
                this._chinesedescription = value;
            }
        }

        public string ChineseName
        {
            get
            {
                return this._chinesename;
            }
            set
            {
                this._chinesename = value;
            }
        }

        public string EnglishDescription
        {
            get
            {
                return this._englishdescription;
            }
            set
            {
                this._englishdescription = value;
            }
        }

        public string EnglishName
        {
            get
            {
                return this._englishname;
            }
            set
            {
                this._englishname = value;
            }
        }

        public EOF_Footer EOFFooter
        {
            get
            {
                return this._eoffooter;
            }
            set
            {
                this._eoffooter = value;
            }
        }

        public string FrenchDescription
        {
            get
            {
                return this._frenchdescription;
            }
            set
            {
                this._frenchdescription = value;
            }
        }

        public string FrenchName
        {
            get
            {
                return this._frenchname;
            }
            set
            {
                this._frenchname = value;
            }
        }

        public string GermanDescription
        {
            get
            {
                return this._germandescription;
            }
            set
            {
                this._germandescription = value;
            }
        }

        public string GermanName
        {
            get
            {
                return this._germanname;
            }
            set
            {
                this._germanname = value;
            }
        }

        public string ItalianDescription
        {
            get
            {
                return this._italiandescription;
            }
            set
            {
                this._italiandescription = value;
            }
        }

        public string ItalianName
        {
            get
            {
                return this._italianname;
            }
            set
            {
                this._italianname = value;
            }
        }

        public string JapaneseDescription
        {
            get
            {
                return this._japanesedescription;
            }
            set
            {
                this._japanesedescription = value;
            }
        }

        public string JapaneseName
        {
            get
            {
                return this._japanesename;
            }
            set
            {
                this._japanesename = value;
            }
        }

        public string KoreanDescription
        {
            get
            {
                return this._koreandescription;
            }
            set
            {
                this._koreandescription = value;
            }
        }

        public string KoreanName
        {
            get
            {
                return this._koreanname;
            }
            set
            {
                this._koreanname = value;
            }
        }

        public string LatinAmericanSpanishDescription
        {
            get
            {
                return this._latinamericanricaspanishdescription;
            }
            set
            {
                this._latinamericanricaspanishdescription = value;
            }
        }

        public string LatinAmericaSpanishName
        {
            get
            {
                return this._latinamericaspanishname;
            }
            set
            {
                this._latinamericaspanishname = value;
            }
        }

        public string MapFileName
        {
            get
            {
                return this._mapfilename;
            }
            set
            {
                this._mapfilename = value;
            }
        }

        public string MapImageFileName
        {
            get
            {
                return this._mapimagefilename;
            }
            set
            {
                this._mapimagefilename = value;
            }
        }

        public string PortugueseDescription
        {
            get
            {
                return this._portuguesedescription;
            }
            set
            {
                this._portuguesedescription = value;
            }
        }

        public string PortugueseName
        {
            get
            {
                return this._portuguesename;
            }
            set
            {
                this._portuguesename = value;
            }
        }

        public string SpanishDescription
        {
            get
            {
                return this._spanishdescription;
            }
            set
            {
                this._spanishdescription = value;
            }
        }

        public string SpanishName
        {
            get
            {
                return this._spanishname;
            }
            set
            {
                this._spanishname = value;
            }
        }
    }
}

