namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mColorBlock16 : mColorBlock
    {
        public mColorBlock16()
        {
            base.Attributes = mValue.ObjectAttributes.ColorBlock16;
            base.Color_Order = new List<ColorBlockPart>();
        }
    }
}

