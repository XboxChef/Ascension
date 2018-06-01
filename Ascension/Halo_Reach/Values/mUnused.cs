namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mUnused : mValue
    {
        private int _size;

        public mUnused()
        {
            base.Attributes = mValue.ObjectAttributes.Unused;
        }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }
    }
}

