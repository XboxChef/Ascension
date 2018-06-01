namespace Ascension.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public abstract class Security
    {
        protected Security()
        {
        }

        private static byte[] GetHash(string str)
        {
            byte[] buffer = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(str));
            for (int i = 0; i < (buffer.Length - 1); i++)
            {
                buffer[i] = (byte) (buffer[i] ^ buffer[i + 1]);
            }
            return buffer;
        }

        private static string GetOperatingSystemInfo()
        {
            string str = "";
            return (((((str + Environment.MachineName + Environment.NewLine) + Environment.ProcessorCount + Environment.NewLine) + Environment.SystemDirectory + Environment.NewLine) + Environment.OSVersion.Version + Environment.NewLine) + Environment.UserName + Environment.NewLine);
        }

        public static byte[] GetUserKeyBin() => 
            GetHash(GetOperatingSystemInfo());
    }
}

