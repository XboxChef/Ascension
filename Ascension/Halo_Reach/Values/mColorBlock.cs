namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mColorBlock : mValue
    {
        private List<ColorBlockPart> _colororder;
        private bool _realcolor;

        public List<ColorBlockPart> Color_Order
        {
            get
            {
                return _colororder;
            }
            set
            {
                _colororder = value;
            }
        }

        public bool Real_Color
        {
            get
            {
                return _realcolor;
            }
            set
            {
                _realcolor = value;
            }
        }
    }
}

