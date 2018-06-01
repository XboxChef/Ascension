namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mColorBlock8 : mColorBlock
    {
        public mColorBlock8()
        {
            base.Attributes = mValue.ObjectAttributes.ColorBlock8;
            base.Color_Order = new List<ColorBlockPart>();
        }
    }
}

