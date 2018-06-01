namespace Ascension.Halo_Reach.Meta_Parser
{
    using Ascension.Halo_Reach.Plugins;
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    public class MetaParser
    {
        private List<Ident> _ident;
        private HaloMap _map;
        private List<StringIdentifier> _strings;
        private List<Structure> _structures;
        private List<TagData> _tagdata;
        private int _tagindex;
        private TagNameList tagNameList;

        public MetaParser(HaloMap map, int tagIndex)
        {
            Map = map;
            TagIndex = tagIndex;
            tagNameList = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)Map.Map_Header.internalName));
            Map.tagNameList = tagNameList;
        }

        public void ParseMeta(XmlParser xmlParser)
        {
            Structures = new List<Structure>();
            Idents = new List<Ident>();
            Strings = new List<StringIdentifier>();
            Tag_Data_Blocks = new List<TagData>();
            Map.OpenIO();
            ParseRecursively(xmlParser.ValueList, Map.Index_Items[TagIndex].Offset, -1);
            Map.CloseIO();
        }

        private void ParseRecursively(List<mValue> ValueList, int chunkOffset, int chunkNumber)
        {
            for (int i = 0; i < ValueList.Count; i++)
            {
                object name;
                switch (ValueList[i].Attributes)
                {
                    case mValue.ObjectAttributes.TagBlock:
                    {
                        if ((chunkOffset + ValueList[i].Offset) <= ((int) Map.IO.In.BaseStream.Length))
                        {
                            break;
                        }
                        continue;
                    }
                    case mValue.ObjectAttributes.TagData:
                    {
                        if ((chunkOffset + ValueList[i].Offset) <= ((int) Map.IO.In.BaseStream.Length))
                        {
                            goto Label_0224;
                        }
                        continue;
                    }
                    case mValue.ObjectAttributes.TagReference:
                    {
                        if ((chunkOffset + ValueList[i].Offset) <= ((int) Map.IO.In.BaseStream.Length))
                        {
                            goto Label_03B7;
                        }
                        continue;
                    }
                    case mValue.ObjectAttributes.StringID:
                    {
                        if ((chunkOffset + ValueList[i].Offset) <= ((int) Map.IO.In.BaseStream.Length))
                        {
                            goto Label_05E5;
                        }
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
                Map.IO.In.BaseStream.Position = chunkOffset + ValueList[i].Offset;
                mTagBlock block = (mTagBlock) ValueList[i];
                Structure item = new Structure {
                    Name = block.Name
                };
                if (chunkNumber != -1)
                {
                    name = item.Name;
                    item.Name = string.Concat(new object[] { name, " [", chunkNumber, "]" });
                }
                item.Size = block.Size;
                item.Count = Map.IO.In.ReadInt32();
                item.Pointer = Map.IO.In.ReadInt32() - Map.Map_Header.mapMagic;
                if (!(((item.Count <= 0) | (item.Pointer <= 0)) | (item.Count > 0x186a0)))
                {
                    item.Offset = chunkOffset + block.Offset;
                    Structures.Add(item);
                    for (int j = 0; j < item.Count; j++)
                    {
                        ParseRecursively(block.Values, item.Pointer + (j * item.Size), j);
                    }
                }
                continue;
            Label_0224:
                Map.IO.In.BaseStream.Position = chunkOffset + ValueList[i].Offset;
                mTagData data = (mTagData) ValueList[i];
                TagData data2 = new TagData {
                    Name = data.Name
                };
                if (chunkNumber != -1)
                {
                    name = data2.Name;
                    data2.Name = string.Concat(new object[] { name, " [", chunkNumber, "]" });
                }
                data2.Size = Map.IO.In.ReadInt32();
                Stream baseStream = Map.IO.In.BaseStream;
                baseStream.Position += 8L;
                data2.Pointer = Map.IO.In.ReadInt32() - Map.Map_Header.mapMagic;
                if (!((data2.Size <= 0) | (data2.Pointer <= 0)))
                {
                    data2.Offset = chunkOffset + data.Offset;
                    Tag_Data_Blocks.Add(data2);
                }
                continue;
            Label_03B7:
                Map.IO.In.BaseStream.Position = (chunkOffset + ValueList[i].Offset) + 12;
                mTagReference reference = (mTagReference) ValueList[i];
                Ident ident = new Ident {
                    Name = reference.Name
                };
                if (chunkNumber != -1)
                {
                    name = ident.Name;
                    ident.Name = string.Concat(new object[] { name, " [", chunkNumber, "]" });
                }
                ident.Offset = chunkOffset + reference.Offset;
                ident.ID = Map.IO.In.ReadInt32();
                int tagIndexByIdent = Map.GetTagIndexByIdent(ident.ID);
                if (tagIndexByIdent != -1)
                {
                    ident.TagClass = Map.Index_Items[tagIndexByIdent].Class;
                }
                else
                {
                    ident.TagClass = "Null";
                }
                if (tagIndexByIdent != -1)
                {
                    int key = Map.Index_Items[tagIndexByIdent].Ident;
                    string str = Map.Index_Items[tagIndexByIdent].Name;
                    if (Map.tagNameList.TagPaths.ContainsKey(key))
                    {
                        try
                        {
                            str = Map.tagNameList.TagPaths[key];
                        }
                        catch
                        {
                        }
                    }
                    ident.TagName = str + "." + Map.Index_Items[tagIndexByIdent].Class;
                }
                else
                {
                    ident.TagName = "Null";
                }
                Idents.Add(ident);
                continue;
            Label_05E5:
                Map.IO.In.BaseStream.Position = chunkOffset + ValueList[i].Offset;
                mStringID gid = (mStringID) ValueList[i];
                StringIdentifier identifier = new StringIdentifier {
                    Name = gid.Name
                };
                if (chunkNumber != -1)
                {
                    name = identifier.Name;
                    identifier.Name = string.Concat(new object[] { name, " [", chunkNumber, "]" });
                }
                identifier.Offset = chunkOffset + gid.Offset;
                identifier.Identifier = Map.IO.In.ReadInt32();
                tagIndexByIdent = Map.String_Table.GetStringItemIndexByID(Map, identifier.Identifier);
                identifier.StringIndex = tagIndexByIdent;
                try
                {
                    identifier.StringName = Map.String_Table.StringItems[tagIndexByIdent].Name;
                }
                catch
                {
                    identifier.StringName = "<<invalid sid>>";
                }
                Strings.Add(identifier);
            }
        }

        public List<Ident> Idents
        {
            get
            {
                return _ident;
            }
            set
            {
                _ident = value;
            }
        }

        public HaloMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public List<StringIdentifier> Strings
        {
            get
            {
                return _strings;
            }
            set
            {
                _strings = value;
            }
        }

        public List<Structure> Structures
        {
            get
            {
                return _structures;
            }
            set
            {
                _structures = value;
            }
        }

        public List<TagData> Tag_Data_Blocks
        {
            get
            {
                return _tagdata;
            }
            set
            {
                _tagdata = value;
            }
        }

        public int TagIndex
        {
            get
            {
                return _tagindex;
            }
            set
            {
                _tagindex = value;
            }
        }

        public class Ident
        {
            private int _ident;
            private string _name;
            private int _offset;
            private string _tagclass;
            private string _tagname;

            public int ID
            {
                get
                {
                    return _ident;
                }
                set
                {
                    _ident = value;
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

            public string TagClass
            {
                get
                {
                    return _tagclass;
                }
                set
                {
                    _tagclass = value;
                }
            }

            public string TagName
            {
                get
                {
                    return _tagname;
                }
                set
                {
                    _tagname = value;
                }
            }
        }

        public class StringIdentifier
        {
            private int _ident;
            private string _name;
            private int _offset;
            private int _stringindex;
            private string _stringname;

            public int Identifier
            {
                get
                {
                    return _ident;
                }
                set
                {
                    _ident = value;
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

            public int StringIndex
            {
                get
                {
                    return _stringindex;
                }
                set
                {
                    _stringindex = value;
                }
            }

            public string StringName
            {
                get
                {
                    return _stringname;
                }
                set
                {
                    _stringname = value;
                }
            }
        }

        public class Structure
        {
            private int _count;
            private string _name;
            private int _offset;
            private int _pointer;
            private int _size;

            public int Count
            {
                get
                {
                    return _count;
                }
                set
                {
                    _count = value;
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
        }

        public class TagData
        {
            private string _name;
            private int _offset;
            private int _pointer;
            private int _size;

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
        }
    }
}

