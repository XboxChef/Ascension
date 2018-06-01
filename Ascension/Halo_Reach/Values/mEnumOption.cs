namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mEnumOption
    {
        private string _name;
        private int _value;

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

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}

