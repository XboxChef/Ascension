namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mBitmask32 : mBitmask
    {
        public mBitmask32()
        {
            base.Attributes = mValue.ObjectAttributes.Bitmask32;
        }
    }
}

