// Decompiled with JetBrains decompiler
// Type: Ascension.Patches.File_Patches.AscendedPatch
// Assembly: Ascension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF346614-614E-466E-A94D-08ACEFC3D738
// Assembly location: C:\Users\SilentSerenity\Desktop\Ascension\Ascension.exe

using Ascension.Settings;
using HaloDevelopmentExtender;
using HaloReach3d.Map;
using System;
using System.IO;

namespace Ascension.Patches.File_Patches
{
    public abstract class AscendedPatch
    {
        public static void ApplyPatchData(string originalMap, string patchPath)
        {
            if (patchPath == "" | originalMap == "")
                return;
            BinaryReader binaryReader = new BinaryReader((Stream)new FileStream(patchPath, FileMode.Open, FileAccess.Read));
            BinaryWriter binaryWriter = new BinaryWriter((Stream)new FileStream(originalMap, FileMode.Open, FileAccess.Write));
            while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
            {
                int num = binaryReader.ReadInt32();
                int count = binaryReader.ReadInt32();
                binaryWriter.BaseStream.Position = (long)num;
                binaryWriter.Write(binaryReader.ReadBytes(count));
            }
            binaryReader.Close();
            binaryWriter.Close();
        }


        public static unsafe void CreatePatch(string Orig, string ModdedMap, string PatchLocation)
        {
            HaloReach3d.IO.EndianIO endianIo1 = new HaloReach3d.IO.EndianIO(PatchLocation, HaloReach3d.IO.EndianType.LittleEndian);
            HaloReach3d.IO.EndianIO endianIo2 = new HaloReach3d.IO.EndianIO(ModdedMap, HaloReach3d.IO.EndianType.LittleEndian);
            HaloReach3d.IO.EndianIO endianIo3 = new HaloReach3d.IO.EndianIO(Orig, HaloReach3d.IO.EndianType.LittleEndian);
            endianIo3.Open();
            endianIo1.Open();
            endianIo2.Open();
            int val1 = 4096;
            int length = (int)endianIo2.Stream.Length;
            byte[] buffer1 = new byte[val1];
            byte[] buffer2 = new byte[val1];
            byte[] buffer3 = new byte[val1];
            endianIo3.In.BaseStream.Position = 0L;
            endianIo2.In.BaseStream.Position = 0L;
            fixed (byte* numPtr1 = buffer1)
            fixed (byte* numPtr2 = buffer2)
            fixed (byte* numPtr3 = buffer3)
            {
                int* numPtr4 = (int*)numPtr1;
                int* numPtr5 = (int*)numPtr2;
                int* numPtr6 = (int*)numPtr3;
                int num1 = 0;
                while (num1 < length)
                {
                    int count1 = Math.Min(val1, length - num1);
                    if (endianIo3.In.BaseStream.Position + (long)val1 > endianIo3.In.BaseStream.Length)
                        buffer1 = new byte[val1];
                    else
                        endianIo3.In.Read(buffer1, 0, count1);
                    endianIo2.In.Read(buffer2, 0, count1);
                    for (int index = 0; index < count1 / 4; ++index)
                        if (numPtr4[index] != numPtr5[index] | endianIo3.In.BaseStream.Position + (long)val1 > endianIo3.In.BaseStream.Length)
                        {
                            int num2 = num1 + index * 4;
                            int count2 = 0;
                            while (index < count1 / 4 && numPtr4[index] != numPtr5[index] | endianIo3.In.BaseStream.Position + (long)val1 > endianIo3.In.BaseStream.Length)
                            {
                                numPtr6[count2 / 4] = numPtr5[index];
                                ++index;
                                count2 += 4;
                            }
                            endianIo1.Out.Write(num2);
                            endianIo1.Out.Write(count2);
                            endianIo1.Out.Write(buffer3, 0, count2);
                        }
                    num1 += val1;
                }
            }
            endianIo1.Close();
            endianIo2.Close();
            endianIo3.Close();
        }

        public static void PokePatch(string originalMap, string patchPath)
        {
            XboxDebugCommunicator debugCommunicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
            if (!debugCommunicator.Connected)
            {
                try
                {
                    debugCommunicator.Connect();
                }
                catch
                {
                }
            }
            XboxMemoryStream xboxMemoryStream = debugCommunicator.ReturnXboxMemoryStream();
            debugCommunicator.Disconnect();
            HaloReach3d.IO.EndianIO endianIo = new HaloReach3d.IO.EndianIO((Stream)xboxMemoryStream, HaloReach3d.IO.EndianType.BigEndian);
            endianIo.Open();
            HaloMap haloMap = new HaloMap(originalMap);
            haloMap.ReloadMap();
            int mapMagic = haloMap.Map_Header.mapMagic;
            haloMap.CloseIO();
            BinaryReader binaryReader = new BinaryReader((Stream)new FileStream(patchPath, FileMode.Open, FileAccess.Read));
            try
            {
                while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length - 8L)
                {
                    int num = binaryReader.ReadInt32();
                    int count = binaryReader.ReadInt32();
                    endianIo.Out.BaseStream.Position = (long)(num + mapMagic);
                    endianIo.Out.Write(binaryReader.ReadBytes(count));
                }
            }
            catch
            {
            }
            binaryReader.Close();
            endianIo.Close();
            xboxMemoryStream.Close();
        }
    }
}
