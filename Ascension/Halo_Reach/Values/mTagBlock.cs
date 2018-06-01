namespace Ascension.Halo_Reach.Values
{
    using System;
    using System.Collections.Generic;

    public class mTagBlock : mValue
    {
        private int _chunkcount;
        private int _memorypointer;
        private int _pointer;
        private int _size;
        private List<mValue> _values;

        public mTagBlock()
        {
            base.Attributes = mValue.ObjectAttributes.TagBlock;
            Values = new List<mValue>();
        }

        public int ChunkCount
        {
            get
            {
                return _chunkcount;
            }
            set
            {
                _chunkcount = value;
            }
        }

        public int MemoryPointer
        {
            get
            {
                return _memorypointer;
            }
            set
            {
                _memorypointer = value;
            }
        }

        public int Pointer
        {
            get
            {
                return _pointer;
            }
            set
            {
                _pointer = value;
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public List<mValue> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }
    }
}

