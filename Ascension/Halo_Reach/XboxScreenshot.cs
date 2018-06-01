// Decompiled with JetBrains decompiler
// Type: Ascension.Halo_Reach.XboxScreenshot
// Assembly: Ascension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF346614-614E-466E-A94D-08ACEFC3D738
// Assembly location: C:\Users\SilentSerenity\Desktop\Ascension\Ascension.exe

using Ascension.Settings;
using HaloDevelopmentExtender;
using HaloReach3d.Helpers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ascension.Halo_Reach
{
    public abstract class XboxScreenshot
    {
        public static Image TakeScreenshot(XboxDebugCommunicator xdc)
        {
            string str = Application.StartupPath + "\\TempScreenshot.dds";
            xdc.Screenshot(str);
            Image image = XboxScreenshot.Deswizzle(str);
            File.Delete(str);
            return image;
        }

        public static unsafe void GammaCorrect(double gamma, BitmapData imageData)
        {
            gamma = Math.Max(0.1, Math.Min(5.0, gamma));
            double y = 1.0 / gamma;
            byte[] numArray = new byte[256];
            for (int index = 0; index < 256; ++index)
                numArray[index] = (byte)Math.Min((int)byte.MaxValue, (int)(Math.Pow((double)index / (double)byte.MaxValue, y) * (double)byte.MaxValue + 0.5));
            int width = imageData.Width;
            int height = imageData.Height;
            int num1 = width * (imageData.PixelFormat == PixelFormat.Format8bppIndexed ? 1 : 3);
            int num2 = imageData.Stride - num1;
            byte* pointer = (byte*)imageData.Scan0.ToPointer();
            for (int index = 0; index < height; ++index)
            {
                int num3 = 0;
                while (num3 < num1)
                {
                    *pointer = numArray[(int)*pointer];
                    ++num3;
                    ++pointer;
                }
                pointer += num2;
            }
        }

        public static Bitmap ResizeImage(Bitmap Orig)
        {
            if (!AppSettings.Settings.ResizeScreenshots)
                return Orig;
            Bitmap bitmap = new Bitmap(AppSettings.Settings.ScreenshotWidth, AppSettings.Settings.ScreenshotHeight);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap))
                graphics.DrawImage((Image)Orig, 0, 0, AppSettings.Settings.ScreenshotWidth, AppSettings.Settings.ScreenshotHeight);
            return bitmap;
        }

        [DllImport("kernel32.dll")]
        private static extern void RtlMoveMemory(IntPtr src, byte[] temp, int cb);

        private static Image Deswizzle(string FilePath)
        {
            EndianReader endianReader = new EndianReader((Stream)new FileStream(FilePath, FileMode.Open, FileAccess.Read), EndianType.LittleEndian);
            endianReader.BaseStream.Position = 12L;
            int height = endianReader.ReadInt32();
            int width = endianReader.ReadInt32();
            endianReader.BaseStream.Position = 92L;
            string hexString = ExtraFunctions.BytesToHexString(endianReader.ReadBytes(12));
            endianReader.BaseStream.Position = 128L;
            int count = width * height * 4;
            byte[] buffer = endianReader.ReadBytes(count);
            endianReader.Close();
            if (hexString == "FF03000000FC0F000000F03F")
            {
                Image image = XboxScreenshot.DeswizzleA2R10G10B10(buffer, width, height);
                if (AppSettings.Settings.AdjustGamma)
                {
                    BitmapData bitmapData = ((Bitmap)image).LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    XboxScreenshot.GammaCorrect(AppSettings.Settings.GammaValue, bitmapData);
                    ((Bitmap)image).UnlockBits(bitmapData);
                }
                return image;
            }
            if (hexString == "0000FF0000FF0000FF000000")
                return XboxScreenshot.DeswizzleA8R8G8B8(buffer, width, height);
            return (Image)null;
        }

        private static Image DeswizzleA2R10G10B10(byte[] buffer, int width, int height)
        {
            int startIndex = 0;
            for (int index1 = 0; index1 < height; ++index1)
            {
                for (int index2 = 0; index2 < width; ++index2)
                {
                    uint uint32 = BitConverter.ToUInt32(buffer, startIndex);
                    uint num1 = (uint32 & 1072693248U) >> 22;
                    uint num2 = (uint32 & 1047552U) >> 12;
                    uint num3 = (uint32 & 1023U) >> 2;
                    byte[] numArray1 = buffer;
                    int index3 = startIndex;
                    int num4 = index3 + 1;
                    int num5 = (int)(byte)num3;
                    numArray1[index3] = (byte)num5;
                    byte[] numArray2 = buffer;
                    int index4 = num4;
                    int num6 = index4 + 1;
                    int num7 = (int)(byte)num2;
                    numArray2[index4] = (byte)num7;
                    byte[] numArray3 = buffer;
                    int index5 = num6;
                    int num8 = index5 + 1;
                    int num9 = (int)(byte)num1;
                    numArray3[index5] = (byte)num9;
                    byte[] numArray4 = buffer;
                    int index6 = num8;
                    startIndex = index6 + 1;
                    int maxValue = (int)byte.MaxValue;
                    numArray4[index6] = (byte)maxValue;
                }
            }
            Marshal.FreeHGlobal(new IntPtr());
            IntPtr num = Marshal.AllocHGlobal(buffer.Length);
            XboxScreenshot.RtlMoveMemory(num, buffer, buffer.Length);
            return (Image)new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, num);
        }

        private static Image DeswizzleA8R8G8B8(byte[] buffer, int width, int height)
        {
            int startIndex = 0;
            for (int index1 = 0; index1 < height; ++index1)
            {
                for (int index2 = 0; index2 < width; ++index2)
                {
                    BitConverter.ToUInt32(buffer, startIndex);
                    uint num1 = (uint)buffer[startIndex];
                    uint num2 = (uint)buffer[startIndex + 1];
                    uint num3 = (uint)buffer[startIndex + 2];
                    byte[] numArray1 = buffer;
                    int index3 = startIndex;
                    int num4 = index3 + 1;
                    int num5 = (int)(byte)num1;
                    numArray1[index3] = (byte)num5;
                    byte[] numArray2 = buffer;
                    int index4 = num4;
                    int num6 = index4 + 1;
                    int num7 = (int)(byte)num2;
                    numArray2[index4] = (byte)num7;
                    byte[] numArray3 = buffer;
                    int index5 = num6;
                    int num8 = index5 + 1;
                    int num9 = (int)(byte)num3;
                    numArray3[index5] = (byte)num9;
                    byte[] numArray4 = buffer;
                    int index6 = num8;
                    startIndex = index6 + 1;
                    int maxValue = (int)byte.MaxValue;
                    numArray4[index6] = (byte)maxValue;
                }
            }
            Marshal.FreeHGlobal(new IntPtr());
            IntPtr num = Marshal.AllocHGlobal(buffer.Length);
            XboxScreenshot.RtlMoveMemory(num, buffer, buffer.Length);
            return (Image)new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, num);
        }
    }
}
