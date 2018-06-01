namespace Ascension.Halo_Reach.Map_Package.Package_Classes
{
    using Ascension.Halo_Reach.Map_Package.Info;
    using HaloReach3d.IO;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class BLFImageFile
    {
        private BLF_Header _blfheader;
        private Image _blfimage;
        private EOF_Footer _eoffooter;

        public BLFImageFile()
        {
        }

        public BLFImageFile(Stream stream)
        {
            EndianIO iO = new EndianIO(stream, EndianType.BigEndian);
            iO.Open();
            this.BLFHeader = new BLF_Header(iO, 0);
            this.BLFImage = Image.FromStream(new MemoryStream(iO.In.ReadBytes(this.BLFHeader.FileContentSize)));
            this.EOFFooter = new EOF_Footer(iO, (int) iO.In.BaseStream.Position);
            iO.Close();
        }

        public void SaveBLFImage(Stream stream, BLFImageType imgType)
        {
            EndianIO nio = new EndianIO(stream, EndianType.BigEndian);
            nio.Open();
            nio.Out.WriteAsciiString("_blf", 4);
            nio.Out.Write(0x30);
            nio.Out.Write((short) 1);
            nio.Out.Write((short) 2);
            nio.Out.Write((short) (-2));
            nio.Out.Write(new byte[0x22]);
            nio.Out.WriteAsciiString("mapi", 4);
            MemoryStream stream2 = new MemoryStream();
            if ((imgType == BLFImageType.BaseImage) | (imgType == BLFImageType.BaseImage))
            {
                this.BLFImage.Save(stream2, ImageFormat.Jpeg);
            }
            else
            {
                this.BLFImage.Save(stream2, ImageFormat.Png);
            }
            byte[] buffer = stream2.ToArray();
            stream2.Close();
            nio.Out.Write((int) (buffer.Length + 20));
            nio.Out.Write((short) 1);
            nio.Out.Write((short) 1);
            nio.Out.Write((int) imgType);
            nio.Out.Write(buffer.Length);
            nio.Out.Write(buffer);
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

        public Image BLFImage
        {
            get
            {
                return this._blfimage;
            }
            set
            {
                this._blfimage = value;
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

        public enum BLFImageType
        {
            BaseImage = 0,
            Clip = 1,
            Film = 1,
            Sm = 0,
            Variant = 1
        }
    }
}

