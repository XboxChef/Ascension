namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mBitmask : mValue
    {
        private List<mBitOption> _options;

        public List<mBitOption> Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
            }
        }
    }
}

