// Decompiled with JetBrains decompiler
// Type: Ascension.Update.RC4Engine
// Assembly: Ascension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FED46AAE-3FB0-4F65-8290-2B0ADCC6551A
// Assembly location: C:\Users\SilentSerenity\Downloads\0.0375\Ascension.exe

using System;
using System.Text;

namespace Ascension.Update
{
    public abstract class RC4Engine
    {
        private static string m_sEncryptionKey = "";
        protected static byte[] m_nBox = new byte[RC4Engine.m_nBoxLen];
        public static long m_nBoxLen = (long)byte.MaxValue;

        public static byte[] Encrypt(byte[] data)
        {
            long index1 = 0;
            long index2 = 0;
            byte[] numArray1 = data;
            byte[] numArray2 = new byte[numArray1.Length];
            byte[] numArray3 = new byte[RC4Engine.m_nBoxLen];
            RC4Engine.m_nBox.CopyTo((Array)numArray3, 0);
            for (long index3 = 0; index3 < (long)numArray1.Length; ++index3)
            {
                index1 = (index1 + 1L) % RC4Engine.m_nBoxLen;
                index2 = (index2 + (long)numArray3[index1]) % RC4Engine.m_nBoxLen;
                byte num1 = numArray3[index1];
                numArray3[index1] = numArray3[index2];
                numArray3[index2] = num1;
                byte num2 = numArray1[index3];
                byte num3 = numArray3[(long)((int)numArray3[index1] + (int)numArray3[index2]) % RC4Engine.m_nBoxLen];
                numArray2[index3] = (byte)((uint)num2 ^ (uint)num3);
            }
            return numArray2;
        }

        public static byte[] Decrypt(byte[] data)
        {
            return RC4Engine.Encrypt(data);
        }

        public static string Encrypt(string Key, string Data)
        {
            RC4Engine.EncryptionKey = Key;
            return Encoding.ASCII.GetString(RC4Engine.Encrypt(Encoding.ASCII.GetBytes(Data)));
        }

        public static string Decrypt(string Key, string Data)
        {
            return RC4Engine.Encrypt(Key, Data);
        }

        public static string EncryptionKey
        {
            get
            {
                return RC4Engine.m_sEncryptionKey;
            }
            set
            {
                if (!(RC4Engine.m_sEncryptionKey != value))
                    return;
                RC4Engine.m_sEncryptionKey = value;
                long index1 = 0;
                Encoding ascii = Encoding.ASCII;
                Encoding unicode = Encoding.Unicode;
                byte[] bytes = Encoding.Convert(unicode, ascii, unicode.GetBytes(RC4Engine.m_sEncryptionKey));
                char[] chars = new char[ascii.GetCharCount(bytes, 0, bytes.Length)];
                ascii.GetChars(bytes, 0, bytes.Length, chars, 0);
                long length = (long)RC4Engine.m_sEncryptionKey.Length;
                RC4Engine.m_nBox = new byte[RC4Engine.m_nBoxLen];
                for (long index2 = 0; index2 < RC4Engine.m_nBoxLen; ++index2)
                    RC4Engine.m_nBox[index2] = (byte)index2;
                for (long index2 = 0; index2 < RC4Engine.m_nBoxLen; ++index2)
                {
                    index1 = (index1 + (long)RC4Engine.m_nBox[index2] + (long)chars[index2 % length]) % RC4Engine.m_nBoxLen;
                    byte num = RC4Engine.m_nBox[index2];
                    RC4Engine.m_nBox[index2] = RC4Engine.m_nBox[index1];
                    RC4Engine.m_nBox[index1] = num;
                }
            }
        }

        public static byte[] BinaryEncryptionKey
        {
            set
            {
                int length = value.Length;
                RC4Engine.m_nBox = new byte[RC4Engine.m_nBoxLen];
                for (int index = 0; (long)index < RC4Engine.m_nBoxLen; ++index)
                    RC4Engine.m_nBox[index] = (byte)index;
                int index1 = 0;
                for (int index2 = 0; (long)index2 < RC4Engine.m_nBoxLen; ++index2)
                {
                    index1 = (index1 + (int)RC4Engine.m_nBox[index2] + (int)value[index2 % length]) % (int)RC4Engine.m_nBoxLen;
                    byte num = RC4Engine.m_nBox[index2];
                    RC4Engine.m_nBox[index2] = RC4Engine.m_nBox[index1];
                    RC4Engine.m_nBox[index1] = num;
                }
            }
        }
    }
}
