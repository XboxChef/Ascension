namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mTagData : mValue
    {
        private int _pointer;
        private uint _size;

        public mTagData()
        {
            base.Attributes = mValue.ObjectAttributes.TagData;
        }

        public int Pointer
        {
            get
            {
                return _pointer;
            }
            set
            {
                _pointer = value;
            }
        }

        public uint Size
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

