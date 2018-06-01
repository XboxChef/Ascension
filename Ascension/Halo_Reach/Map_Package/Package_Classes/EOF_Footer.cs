namespace Ascension.Halo_Reach.Map_Package.Package_Classes
{
    using HaloReach3d.IO;
    using System;

    public class EOF_Footer
    {
        private string _eofstring;
        private int _footersize;
        private byte[] _unknown8;

        public EOF_Footer(EndianIO IO, int position)
        {
            IO.In.BaseStream.Position = position;
            this.EOF_String = IO.In.ReadAsciiString(4);
            this.FooterSize = IO.In.ReadInt32();
            this.Unknown8 = IO.In.ReadBytes(this.FooterSize - 8);
        }

        public string EOF_String
        {
            get
            {
                return this._eofstring;
            }
            set
            {
                this._eofstring = value;
            }
        }

        public int FooterSize
        {
            get
            {
                return this._footersize;
            }
            set
            {
                this._footersize = value;
            }
        }

        public byte[] Unknown8
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

