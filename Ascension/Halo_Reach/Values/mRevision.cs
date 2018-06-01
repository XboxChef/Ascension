namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mRevision
    {
        private string _author;
        private string _description;
        private float _version;

        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
            }
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

        public float Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
    }
}

