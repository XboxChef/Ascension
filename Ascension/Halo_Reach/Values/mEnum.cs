namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mEnum : mValue
    {
        private List<mEnumOption> _options;

        public List<mEnumOption> Options
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

