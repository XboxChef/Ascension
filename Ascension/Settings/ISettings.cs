namespace Ascension.Settings
{
    using Ascension.Update;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;

    public abstract class ISettings
    {
        protected ISettings()
        {
        }

        public virtual void Read(BinaryReader br)
        {
            foreach (PropertyInfo info in base.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(EncryptedAttribute), false);
                if (customAttributes.Length > 0)
                {
                    info.SetValue(this, ReadValue(br, info.PropertyType, ((EncryptedAttribute) customAttributes[0]).Encrypted), null);
                }
                else
                {
                    info.SetValue(this, ReadValue(br, info.PropertyType, false), null);
                }
            }
        }

        private object ReadValue(BinaryReader br, System.Type type, bool encrypted)
        {
            if (type == typeof(int))
            {
                return br.ReadInt32();
            }
            if (type == typeof(double))
            {
                return br.ReadDouble();
            }
            if (type == typeof(bool))
            {
                return br.ReadBoolean();
            }
            if (type == typeof(string))
            {
                if (!encrypted)
                {
                    return br.ReadString();
                }
                byte count = br.ReadByte();
                byte[] bytes = RC4Engine.Decrypt(br.ReadBytes(count));
                return Encoding.ASCII.GetString(bytes);
            }
            if (type == typeof(byte[]))
            {
                return br.ReadBytes(br.ReadInt32());
            }
            if (type.IsEnum)
            {
                System.Type underlyingType = Enum.GetUnderlyingType(type);
                if (underlyingType == typeof(byte))
                {
                    return br.ReadByte();
                }
                if (underlyingType == typeof(short))
                {
                    return br.ReadInt16();
                }
                if (underlyingType == typeof(int))
                {
                    return br.ReadInt32();
                }
                if (underlyingType == typeof(long))
                {
                    return br.ReadInt64();
                }
                MessageBox.Show("Unable to read this enum type! Type: " + underlyingType.ToString());
            }
            else
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(ISettingList<>)))
                {
                    object obj2 = Activator.CreateInstance(type, new object[0]);
                    obj2.GetType().GetMethod("Read").Invoke(obj2, new object[] { br });
                    return obj2;
                }
                MessageBox.Show("Cannot read type " + type.ToString());
            }
            return null;
        }

        public virtual void Write(BinaryWriter bw)
        {
            foreach (PropertyInfo info in base.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(EncryptedAttribute), false);
                if (customAttributes.Length > 0)
                {
                    WriteValue(bw, info.GetValue(this, null), ((EncryptedAttribute) customAttributes[0]).Encrypted);
                }
                else
                {
                    WriteValue(bw, info.GetValue(this, null), false);
                }
            }
        }

        private void WriteValue(BinaryWriter bw, object value, bool encrypted)
        {
            System.Type enumType = value.GetType();
            if (enumType == typeof(int))
            {
                bw.Write((int) value);
            }
            else if (enumType == typeof(double))
            {
                bw.Write((double) value);
            }
            else if (enumType == typeof(bool))
            {
                bw.Write((bool) value);
            }
            else if (enumType == typeof(string))
            {
                if (!encrypted)
                {
                    bw.Write((string) value);
                }
                else
                {
                    byte[] bytes = Encoding.ASCII.GetBytes((string) value);
                    bw.Write((byte) bytes.Length);
                    bw.Write(RC4Engine.Encrypt(bytes));
                }
            }
            else if (enumType == typeof(byte[]))
            {
                bw.Write(((byte[]) value).Length);
                bw.Write((byte[]) value);
            }
            else if (enumType.IsEnum)
            {
                System.Type underlyingType = Enum.GetUnderlyingType(enumType);
                if (underlyingType == typeof(byte))
                {
                    bw.Write((byte) value);
                }
                else if (underlyingType == typeof(short))
                {
                    bw.Write((short) value);
                }
                else if (underlyingType == typeof(int))
                {
                    bw.Write((int) value);
                }
                else if (underlyingType == typeof(long))
                {
                    bw.Write((long) value);
                }
                else
                {
                    MessageBox.Show("Unable to write this enum type! Type: " + underlyingType.ToString());
                }
            }
            else if (enumType.IsGenericType && (enumType.GetGenericTypeDefinition() == typeof(ISettingList<>)))
            {
                enumType.GetMethod("Write").Invoke(value, new object[] { bw });
            }
            else
            {
                MessageBox.Show("Cannot write type " + enumType);
            }
        }

        public class EncryptedAttribute : Attribute
        {
            public readonly bool Encrypted;

            public EncryptedAttribute()
            {
                Encrypted = true;
            }

            public EncryptedAttribute(bool encrypted)
            {
                Encrypted = true;
                Encrypted = encrypted;
            }
        }
    }
}

