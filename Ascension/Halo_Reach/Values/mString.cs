namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mString : mValue
    {
        private int _length;

        public mString()
        {
            base.Attributes = mValue.ObjectAttributes.String;
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

