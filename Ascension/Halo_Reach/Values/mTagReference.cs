namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mTagReference : mValue
    {
        public mTagReference()
        {
            base.Attributes = mValue.ObjectAttributes.TagReference;
        }
    }
}

