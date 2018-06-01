namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mColorBlock32 : mColorBlock
    {
        public mColorBlock32()
        {
            base.Attributes = mValue.ObjectAttributes.ColorBlock32;
            base.Color_Order = new List<ColorBlockPart>();
        }
    }
}

