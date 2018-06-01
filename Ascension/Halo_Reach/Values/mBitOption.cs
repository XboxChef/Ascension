namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mBitOption
    {
        private int _bitindex;
        private string _name;

        public int BitIndex
        {
            get
            {
                return _bitindex;
            }
            set
            {
                _bitindex = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}

