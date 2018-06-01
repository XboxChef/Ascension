namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mUndefined : mValue
    {
        public mUndefined()
        {
            base.Attributes = mValue.ObjectAttributes.Undefined;
        }
    }
}

