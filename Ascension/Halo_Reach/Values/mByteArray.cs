namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mByteArray : mValue
    {
        private int _length;

        public mByteArray()
        {
            base.Attributes = mValue.ObjectAttributes.ByteArray;
        }

        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }
    }
}

