namespace Ascension.Halo_Reach.Values
{
    using System;

    public class mValue
    {
        private ObjectAttributes _attributes;
        private string _name;
        private int _offset;
        private bool _visible;

        public ObjectAttributes Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                _attributes = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }

        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }

        public enum ObjectAttributes
        {
            Comment,
            ByteArray,
            ColorBlock8,
            ColorBlock16,
            ColorBlock32,
            ColorBlockF,
            TagBlock,
            TagData,
            TagReference,
            StringID,
            Unicode,
            String,
            Bitmask8,
            Bitmask16,
            Bitmask32,
            Float,
            Int16,
            UInt16,
            Int32,
            UInt32,
            Byte,
            Enum8,
            Enum16,
            Enum32,
            Undefined,
            Unused,
            Slider,
            None
        }
    }
}

