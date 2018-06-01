namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mComment : mValue
    {
        private string _description;
        private string _title;

        public mComment()
        {
            base.Attributes = mValue.ObjectAttributes.Comment;
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
    }
}

