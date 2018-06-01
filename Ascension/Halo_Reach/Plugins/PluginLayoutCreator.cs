namespace Ascension.Halo_Reach.Plugins
{
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.Helpers;
    using HaloReach3d.Locale;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    public class PluginLayoutCreator
    {
        private HaloMap _map;
        private List<MetaHeader_DataBlock> _metaheaderdatablocks;
        private List<PluginDataBlock> _plugindatablocks;
        private List<PluginScanningRegion> _pluginscanningregions;
        private List<Tag_Ident> _tagidents;
        private List<Tag_Void> _tagvoids;

        public PluginLayoutCreator(HaloMap map)
        {
            Map = map;
            Plugin_Scanning_Regions = new List<PluginScanningRegion>();
            Plugin_Data_Blocks = new List<PluginDataBlock>();
            MetaHeader_Data_Blocks = new List<MetaHeader_DataBlock>();
            Tag_Idents = new List<Tag_Ident>();
            Tag_Voids = new List<Tag_Void>();
        }

        public void CalculateStructureSize(PluginDataBlock dataBlock)
        {
            int num2;
            int pointer = 0x3b9ac9ff;
            for (num2 = 0; num2 < Plugin_Data_Blocks.Count; num2++)
            {
                if ((Plugin_Data_Blocks[num2].Pointer > dataBlock.Pointer) && (Plugin_Data_Blocks[num2].Pointer < pointer))
                {
                    pointer = Plugin_Data_Blocks[num2].Pointer;
                }
            }
            for (num2 = 0; num2 < Plugin_Scanning_Regions.Count; num2++)
            {
                if (((Plugin_Scanning_Regions[num2].Offset + Plugin_Scanning_Regions[num2].Size) > dataBlock.Pointer) && ((Plugin_Scanning_Regions[num2].Offset + Plugin_Scanning_Regions[num2].Size) < pointer))
                {
                    pointer = Plugin_Scanning_Regions[num2].Offset + Plugin_Data_Blocks[num2].Size;
                }
            }
            if (pointer == 0x3b9ac9ff)
            {
                dataBlock.Size = 0;
            }
            else
            {
                dataBlock.Size = (pointer - dataBlock.Pointer) / dataBlock.Count;
            }
        }

        public int FindMostLikelySizeForChunk(List<PluginDataBlock> PDBList)
        {
            int[] array = new int[PDBList.Count];
            for (int i = 0; i < PDBList.Count; i++)
            {
                array[i] = PDBList[i].Size;
            }
            try
            {
                Array.Sort<int>(array);
                return array[array.Length / 2];
            }
            catch
            {
                return PDBList[0].Size;
            }
        }

        public void GeneratePlugins(string outputFolder, bool remapMetaHeaders)
        {
            if (remapMetaHeaders)
            {
                GetMetaHeaderDataBlocksMapped(true, true, true);
            }
            List<string> list = new List<string>();
            for (int i = 0; i < MetaHeader_Data_Blocks.Count; i++)
            {
                if (!list.Contains(MetaHeader_Data_Blocks[i].Tag_Class))
                {
                    list.Add(MetaHeader_Data_Blocks[i].Tag_Class);
                }
            }
            foreach (string str in list)
            {
                XmlParser parser = new XmlParser();
                mRevision item = new mRevision {
                    Author = "DarkShallFall",
                    Version = 0.1f,
                    Description = "Mapped plugin structure a new."
                };
                parser.Revisions.Add(item);
                parser.TagClass = str;
                List<PluginDataBlock> pDBList = new List<PluginDataBlock>();
                foreach (MetaHeader_DataBlock block in MetaHeader_Data_Blocks)
                {
                    if (block.Tag_Class == str)
                    {
                        pDBList.Add(block);
                    }
                }
                parser.HeaderSize = FindMostLikelySizeForChunk(pDBList);
                try
                {
                    MapPluginWithPDBList(parser.ValueList, pDBList, parser.HeaderSize, 0);
                    parser.WritePlugin(outputFolder + @"\" + str.Replace('<', '_').Replace('>', '_').Replace(" ", "") + ".asc");
                }
                catch
                {
                }
            }
        }

        public List<MetaHeader_DataBlock> GetMetaHeaderDataBlocksMapped(bool FindVoids, bool FindIdents, bool FindChildren)
        {
            int num;
            Plugin_Scanning_Regions = new List<PluginScanningRegion>();
            Plugin_Data_Blocks = new List<PluginDataBlock>();
            MetaHeader_Data_Blocks = new List<MetaHeader_DataBlock>();
            Tag_Idents = new List<Tag_Ident>();
            Tag_Voids = new List<Tag_Void>();
            PluginScanningRegion region = new PluginScanningRegion {
                Offset = Map.Map_Header.RawTableOffset + Map.Map_Header.RawTableSize
            };
            region.Size = Map.Index_Header.tagInfoHeaderOffset - region.Offset;
            Plugin_Scanning_Regions.Add(region);
            PluginScanningRegion region2 = new PluginScanningRegion {
                Offset = (Map.Map_Header.indexOffset + 0x2c) + ExtraFunctions.CalculatePadding(Map.Map_Header.indexOffset + 0x2c, 0x1000)
            };
            region2.Size = new LocaleHandler(Map).LocaleTables[0].LocaleTableIndexOffset - region2.Offset;
            Plugin_Scanning_Regions.Add(region2);
            foreach (HaloMap.TagItem item in Map.Index_Items)
            {
                MetaHeader_DataBlock block = new MetaHeader_DataBlock(item);
                Plugin_Data_Blocks.Add(block);
                MetaHeader_Data_Blocks.Add(block);
                Application.DoEvents();
            }
            Map.OpenIO();
            for (num = 0; num < Plugin_Scanning_Regions.Count; num++)
            {
                PluginScanningRegion region3 = Plugin_Scanning_Regions[num];
                Map.IO.In.BaseStream.Position = region3.Offset;
                while (Map.IO.In.BaseStream.Position < (region3.Offset + region3.Size))
                {
                    double num2 = ((Map.IO.In.BaseStream.Position - region3.Offset) / ((double) region3.Size)) * 100.0;
                    int count = Map.IO.In.ReadInt32();
                    int pointer = Map.IO.In.ReadInt32() - Map.Map_Header.mapMagic;
                    int num5 = Map.IO.In.ReadInt32();
                    if (((num5 == 0) && ((count > 0) && (count < 0x927c0))) && PointerIsInRegions(count, pointer))
                    {
                        Reflexive_DataBlock block2 = new Reflexive_DataBlock {
                            Count = count,
                            Offset = (int) (Map.IO.In.BaseStream.Position - 12L),
                            Pointer = pointer
                        };
                        Plugin_Data_Blocks.Add(block2);
                    }
                    else if ((PointerIsInRegions(pointer + Map.Map_Header.mapMagic, num5 - Map.Map_Header.mapMagic) && ((pointer + Map.Map_Header.mapMagic) > 0)) && ((pointer + Map.Map_Header.mapMagic) < 0x927c0))
                    {
                        Stream baseStream = Map.IO.In.BaseStream;
                        baseStream.Position -= 8L;
                    }
                    else if (FindIdents)
                    {
                        Stream stream2 = Map.IO.In.BaseStream;
                        stream2.Position -= 12L;
                        string str = Map.IO.In.ReadAsciiString(4);
                        Stream stream3 = Map.IO.In.BaseStream;
                        stream3.Position += 8L;
                        int num6 = Map.IO.In.ReadInt32();
                        int tagIndexByIdent = Map.GetTagIndexByIdent(num6);
                        if ((tagIndexByIdent != -1) && (Map.Index_Items[tagIndexByIdent].Class == str))
                        {
                            Tag_Ident ident = new Tag_Ident {
                                Offset = ((int) Map.IO.In.BaseStream.Position) - 0x10
                            };
                            Tag_Idents.Add(ident);
                        }
                        else
                        {
                            Stream stream4 = Map.IO.In.BaseStream;
                            stream4.Position -= 12L;
                            if (FindVoids)
                            {
                                Stream stream5 = Map.IO.In.BaseStream;
                                stream5.Position -= 4L;
                                uint num8 = Map.IO.In.ReadUInt32();
                                int num9 = Map.IO.In.ReadInt32();
                                int num10 = Map.IO.In.ReadInt32();
                                pointer = Map.IO.In.ReadInt32() - Map.Map_Header.mapMagic;
                                int num11 = Map.IO.In.ReadInt32();
                                if (((PointerIsInRegions((int) num8, pointer) && (num8 > 0)) && ((num9 == 0) && (num10 == 0))) && (num11 == 0))
                                {
                                    Tag_Void @void = new Tag_Void {
                                        Offset = ((int) Map.IO.In.BaseStream.Position) - 20
                                    };
                                    Tag_Voids.Add(@void);
                                }
                                else
                                {
                                    Stream stream6 = Map.IO.In.BaseStream;
                                    stream6.Position -= 0x10L;
                                }
                            }
                            else
                            {
                                Stream stream7 = Map.IO.In.BaseStream;
                                stream7.Position -= 4L;
                            }
                        }
                    }
                }
            }
            Map.CloseIO();
            for (num = 0; num < Plugin_Data_Blocks.Count; num++)
            {
                CalculateStructureSize(Plugin_Data_Blocks[num]);
            }
            if (FindChildren)
            {
                for (num = 0; num < Plugin_Data_Blocks.Count; num++)
                {
                    MapChildren(Plugin_Data_Blocks[num]);
                }
            }
            return MetaHeader_Data_Blocks;
        }

        public void MapChildren(PluginDataBlock dataBlock)
        {
            foreach (PluginDataBlock block in Plugin_Data_Blocks)
            {
                if ((block.Data_Block_Type == PluginDataBlock.DataBlockType.Reflexive) && ((block.Offset >= dataBlock.Pointer) && (block.Offset < (dataBlock.Pointer + (dataBlock.Count * dataBlock.Size)))))
                {
                    ((Reflexive_DataBlock) block).Offset_In_Parent = (block.Offset - dataBlock.Pointer) % dataBlock.Size;
                    dataBlock.Reflexive_Data_Blocks.Add((Reflexive_DataBlock) block);
                }
            }
            foreach (Tag_Ident ident in Tag_Idents)
            {
                if ((ident.Offset >= dataBlock.Pointer) && (ident.Offset < (dataBlock.Pointer + (dataBlock.Count * dataBlock.Size))))
                {
                    ident.Offset_In_Parent = (ident.Offset - dataBlock.Pointer) % dataBlock.Size;
                    dataBlock.Tag_Idents.Add(ident);
                }
            }
            foreach (Tag_Void @void in Tag_Voids)
            {
                if ((@void.Offset >= dataBlock.Pointer) && (@void.Offset < (dataBlock.Pointer + (dataBlock.Count * dataBlock.Size))))
                {
                    @void.Offset_In_Parent = (@void.Offset - dataBlock.Pointer) % dataBlock.Size;
                    dataBlock.Tag_Voids.Add(@void);
                }
            }
        }

        public void MapPluginWithPDBList(List<mValue> mValueList, List<PluginDataBlock> PDBList, int chunkSize, int unknownStructCount)
        {
            for (int i = 0; i < chunkSize; i += 4)
            {
                List<PluginDataBlock> pDBList = new List<PluginDataBlock>();
                foreach (PluginDataBlock block in PDBList)
                {
                    foreach (Reflexive_DataBlock block2 in block.Reflexive_Data_Blocks)
                    {
                        if (block2.Offset_In_Parent == i)
                        {
                            pDBList.Add(block2);
                        }
                    }
                }
                if (pDBList.Count != 0)
                {
                    mTagBlock item = new mTagBlock {
                        Name = "Unknown " + unknownStructCount
                    };
                    unknownStructCount++;
                    item.Offset = i;
                    item.Size = FindMostLikelySizeForChunk(pDBList);
                    item.Visible = false;
                    mValueList.Add(item);
                    MapPluginWithPDBList(item.Values, pDBList, item.Size, unknownStructCount);
                    i += 8;
                    continue;
                }
                bool flag = false;
                bool flag2 = false;
                foreach (PluginDataBlock block in PDBList)
                {
                    foreach (Tag_Ident ident in block.Tag_Idents)
                    {
                        if (ident.Offset_In_Parent == i)
                        {
                            mTagReference reference = new mTagReference {
                                Name = "Unknown",
                                Offset = i,
                                Visible = false
                            };
                            mValueList.Add(reference);
                            i += 12;
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                    foreach (Tag_Void @void in block.Tag_Voids)
                    {
                        if (@void.Offset_In_Parent == i)
                        {
                            mTagData data = new mTagData {
                                Name = "Unknown",
                                Offset = i,
                                Visible = false
                            };
                            mValueList.Add(data);
                            i += 0x10;
                            flag2 = true;
                            break;
                        }
                    }
                    if (flag2)
                    {
                        break;
                    }
                }
                if (!flag && !flag2)
                {
                    if ((chunkSize - i) >= 4)
                    {
                        mUndefined undefined = new mUndefined {
                            Name = "Unknown",
                            Offset = i,
                            Visible = false
                        };
                        mValueList.Add(undefined);
                    }
                    else if ((chunkSize - i) >= 2)
                    {
                        mInt16 num2 = new mInt16 {
                            Name = "Unknown",
                            Offset = i,
                            Visible = false
                        };
                        mValueList.Add(num2);
                        i -= 2;
                    }
                    else if ((chunkSize - i) == 1)
                    {
                        mByte num3 = new mByte {
                            Name = "Unknown",
                            Offset = i,
                            Visible = false
                        };
                        mValueList.Add(num3);
                    }
                }
            }
        }

        public bool PointerIsInRegions(int count, int pointer)
        {
            for (int i = 0; i < Plugin_Scanning_Regions.Count; i++)
            {
                if (((pointer >= Plugin_Scanning_Regions[i].Offset) && (pointer < (Plugin_Scanning_Regions[i].Offset + Plugin_Scanning_Regions[i].Size))) && (((Plugin_Scanning_Regions[i].Offset + Plugin_Scanning_Regions[i].Size) - pointer) >= count))
                {
                    return true;
                }
            }
            return false;
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

        public List<MetaHeader_DataBlock> MetaHeader_Data_Blocks
        {
            get
            {
                return _metaheaderdatablocks;
            }
            set
            {
                _metaheaderdatablocks = value;
            }
        }

        public List<PluginDataBlock> Plugin_Data_Blocks
        {
            get
            {
                return _plugindatablocks;
            }
            set
            {
                _plugindatablocks = value;
            }
        }

        public List<PluginScanningRegion> Plugin_Scanning_Regions
        {
            get
            {
                return _pluginscanningregions;
            }
            set
            {
                _pluginscanningregions = value;
            }
        }

        public List<Tag_Ident> Tag_Idents
        {
            get
            {
                return _tagidents;
            }
            set
            {
                _tagidents = value;
            }
        }

        public List<Tag_Void> Tag_Voids
        {
            get
            {
                return _tagvoids;
            }
            set
            {
                _tagvoids = value;
            }
        }

        public class MetaHeader_DataBlock : PluginLayoutCreator.PluginDataBlock
        {
            private string _tagclass;

            public MetaHeader_DataBlock(HaloMap.TagItem tag)
            {
                base.Reflexive_Data_Blocks = new List<PluginLayoutCreator.Reflexive_DataBlock>();
                base.Tag_Idents = new List<PluginLayoutCreator.Tag_Ident>();
                base.Tag_Voids = new List<PluginLayoutCreator.Tag_Void>();
                base.Data_Block_Type = PluginLayoutCreator.PluginDataBlock.DataBlockType.Meta_Header;
                base.Count = 1;
                base.Pointer = tag.Offset;
                Tag_Class = tag.Class;
            }

            public string Tag_Class
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
        }

        public class PluginDataBlock
        {
            private int _count;
            private DataBlockType _datablocktype;
            private int _offset;
            private int _pointer;
            private List<PluginLayoutCreator.Reflexive_DataBlock> _reflexivedatablocks;
            private int _size;
            private List<PluginLayoutCreator.Tag_Ident> _tagidents;
            private List<PluginLayoutCreator.Tag_Void> _tagvoids;

            public bool ContainsIdent(int offset)
            {
                foreach (PluginLayoutCreator.Tag_Ident ident in Tag_Idents)
                {
                    if (ident.Offset_In_Parent == offset)
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool ContainsVoid(int offset)
            {
                foreach (PluginLayoutCreator.Tag_Void @void in Tag_Voids)
                {
                    if (@void.Offset_In_Parent == offset)
                    {
                        return true;
                    }
                }
                return false;
            }

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

            public DataBlockType Data_Block_Type
            {
                get
                {
                    return _datablocktype;
                }
                set
                {
                    _datablocktype = value;
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

            public List<PluginLayoutCreator.Reflexive_DataBlock> Reflexive_Data_Blocks
            {
                get
                {
                    return _reflexivedatablocks;
                }
                set
                {
                    _reflexivedatablocks = value;
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

            public List<PluginLayoutCreator.Tag_Ident> Tag_Idents
            {
                get
                {
                    return _tagidents;
                }
                set
                {
                    _tagidents = value;
                }
            }

            public List<PluginLayoutCreator.Tag_Void> Tag_Voids
            {
                get
                {
                    return _tagvoids;
                }
                set
                {
                    _tagvoids = value;
                }
            }

            public enum DataBlockType
            {
                Meta_Header,
                Reflexive
            }
        }

        public class PluginScanningRegion
        {
            private int _offset;
            private int _size;

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

        public class Reflexive_DataBlock : PluginLayoutCreator.PluginDataBlock
        {
            private int _offsetinparent;

            public Reflexive_DataBlock()
            {
                base.Reflexive_Data_Blocks = new List<PluginLayoutCreator.Reflexive_DataBlock>();
                base.Tag_Idents = new List<PluginLayoutCreator.Tag_Ident>();
                base.Tag_Voids = new List<PluginLayoutCreator.Tag_Void>();
                base.Data_Block_Type = PluginLayoutCreator.PluginDataBlock.DataBlockType.Reflexive;
            }

            public int Offset_In_Parent
            {
                get
                {
                    return _offsetinparent;
                }
                set
                {
                    _offsetinparent = value;
                }
            }
        }

        public class Tag_Ident
        {
            private int _offset;
            private int _offsetinparent;

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

            public int Offset_In_Parent
            {
                get
                {
                    return _offsetinparent;
                }
                set
                {
                    _offsetinparent = value;
                }
            }
        }

        public class Tag_Void
        {
            private int _offset;
            private int _offsetinparent;

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

            public int Offset_In_Parent
            {
                get
                {
                    return _offsetinparent;
                }
                set
                {
                    _offsetinparent = value;
                }
            }
        }
    }
}

