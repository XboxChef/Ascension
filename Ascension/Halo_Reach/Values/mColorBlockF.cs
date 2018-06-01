namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mColorBlockF : mColorBlock
    {
        public mColorBlockF()
        {
            base.Attributes = mValue.ObjectAttributes.ColorBlockF;
            base.Color_Order = new List<ColorBlockPart>();
        }
    }
}

