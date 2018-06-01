namespace Ascension.Helpers
{
    using System;
    using System.Collections.Generic;

    public abstract class BitHelper
    {
        protected BitHelper()
        {
        }

        public static int ConvertToWriteableInteger(List<bool> flags)
        {
            int num = 0;
            for (int i = 0; i < flags.Count; i++)
            {
                if (flags[i])
                {
                    num |= ((int) 1) << i;
                }
            }
            return num;
        }

        public static List<bool> LoadValue(object value, int bitcount)
        {
            List<bool> list = new List<bool>();
            int num = 1;
            int num2 = Convert.ToInt32(value);
            for (int i = 0; i < bitcount; i++)
            {
                bool item = (num & num2) == num;
                list.Add(item);
                num = num << 1;
            }
            return list;
        }
    }
}

