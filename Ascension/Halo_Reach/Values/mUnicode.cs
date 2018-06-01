namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mUnicode : mValue
    {
        private int _length;

        public mUnicode()
        {
            base.Attributes = mValue.ObjectAttributes.Unicode;
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

