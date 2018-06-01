namespace Ascension.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ISettingList<T> : List<T> where T: ISettings
    {
        public void Read(BinaryReader br)
        {
            int num = br.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                T item = (T) Activator.CreateInstance(typeof(T));
                item.Read(br);
                base.Add(item);
            }
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(base.Count);
            for (int i = 0; i < base.Count; i++)
            {
                base[i].Write(bw);
            }
        }
    }
}

