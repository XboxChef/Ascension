namespace Ascension.Halo_Reach.Plugins
{
    using Ascension.Halo_Reach.Values;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class XmlParser
    {
        private int _headersize;
        private List<mRevision> _revisions;
        private string _tagclass;
        private List<mValue> _valuelist;

        public XmlParser()
        {
            ValueList = new List<mValue>();
            Revisions = new List<mRevision>();
        }

        public void ParsePlugin(string fileName)
        {
            List<mValue> list = new List<mValue>();
            XmlDocument document = new XmlDocument();
            document.Load(fileName);
            XmlElement documentElement = document.DocumentElement;
            TagClass = documentElement.Attributes["class"].Value;
            HeaderSize = int.Parse(documentElement.Attributes["headersize"].Value);
            Revisions = new List<mRevision>();
            ValueList = new List<mValue>();
            for (int i = 0; i < documentElement.ChildNodes.Count; i++)
            {
                if (documentElement.ChildNodes[i].Name != "revision")
                {
                    mValue item = ReadNode(documentElement.ChildNodes[i]);
                    if (item.Attributes != mValue.ObjectAttributes.None)
                    {
                        ValueList.Add(item);
                    }
                }
                else
                {
                    mRevision revision = new mRevision {
                        Author = documentElement.ChildNodes[i].Attributes["author"].Value,
                        Version = float.Parse(documentElement.ChildNodes[i].Attributes["version"].Value),
                        Description = documentElement.ChildNodes[i].InnerText
                    };
                    Revisions.Add(revision);
                }
            }
        }

        private mValue ReadNode(XmlNode xmlNode)
        {
            int num;
            mColorBlock block2;
            mBitOption option;
            mEnumOption option2;
            string str3 = xmlNode.Name.ToLower();
            switch (str3)
            {
                case "reflexive":
                case "structure":
                case "reflex":
                case "struct":
                case "tagblock":
                case "block":
                {
                    mTagBlock block = new mTagBlock {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Size = int.Parse(xmlNode.Attributes["size"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Values = new List<mValue>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        mValue item = ReadNode(xmlNode.ChildNodes[num]);
                        if (item.Attributes != mValue.ObjectAttributes.None)
                        {
                            block.Values.Add(item);
                        }
                    }
                    return block;
                }
                case "tagdata":
                case "void":
                    return new mTagData { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "array":
                case "bytearray":
                case "bytes":
                    return new mByteArray { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Length = int.Parse(xmlNode.Attributes["length"].Value)
                    };

                case "comment":
                case "note":
                    return new mComment { 
                        Title = xmlNode.Attributes["title"].Value,
                        Description = xmlNode.InnerText.Replace(@"\n", "\n").Replace(@"\t", "\t").Replace(@"\'", "\""),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "color8":
                case "color16":
                case "color32":
                case "colorf":
                    if (xmlNode.Name.ToLower() != "color8")
                    {
                        if (xmlNode.Name.ToLower() == "color16")
                        {
                            block2 = new mColorBlock16();
                        }
                        else if (xmlNode.Name.ToLower() == "color32")
                        {
                            block2 = new mColorBlock32();
                        }
                        else
                        {
                            block2 = new mColorBlockF();
                        }
                        break;
                    }
                    block2 = new mColorBlock8();
                    break;

                case "tagref":
                case "tag":
                case "tagid":
                    return new mTagReference { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "sid":
                case "stringid":
                case "stringidentifier":
                    return new mStringID { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "bitmask8":
                case "bit8":
                {
                    mBitmask8 bitmask = new mBitmask8 {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Options = new List<mBitOption>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        if ((xmlNode.ChildNodes[num].Name.ToLower() == "bit") | (xmlNode.ChildNodes[num].Name.ToLower() == "option"))
                        {
                            option = new mBitOption {
                                Name = xmlNode.ChildNodes[num].Attributes["name"].Value
                            };
                            try
                            {
                                option.BitIndex = int.Parse(xmlNode.ChildNodes[num].Attributes["index"].Value);
                            }
                            catch
                            {
                                option.BitIndex = int.Parse(xmlNode.ChildNodes[num].Attributes["value"].Value);
                            }
                            bitmask.Options.Add(option);
                        }
                    }
                    return bitmask;
                }
                case "bitmask16":
                case "bit16":
                {
                    mBitmask16 bitmask2 = new mBitmask16 {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Options = new List<mBitOption>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        if ((xmlNode.ChildNodes[num].Name.ToLower() == "bit") | (xmlNode.ChildNodes[num].Name.ToLower() == "option"))
                        {
                            option = new mBitOption {
                                Name = xmlNode.ChildNodes[num].Attributes["name"].Value
                            };
                            try
                            {
                                option.BitIndex = int.Parse(xmlNode.ChildNodes[num].Attributes["index"].Value);
                            }
                            catch
                            {
                                option.BitIndex = int.Parse(xmlNode.ChildNodes[num].Attributes["value"].Value);
                            }
                            bitmask2.Options.Add(option);
                        }
                    }
                    return bitmask2;
                }
                case "bitmask32":
                case "bit32":
                {
                    mBitmask32 bitmask3 = new mBitmask32 {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Options = new List<mBitOption>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        if ((xmlNode.ChildNodes[num].Name.ToLower() == "bit") | (xmlNode.ChildNodes[num].Name.ToLower() == "option"))
                        {
                            option = new mBitOption {
                                Name = xmlNode.ChildNodes[num].Attributes["name"].Value
                            };
                            try
                            {
                                option.BitIndex = int.Parse(xmlNode.ChildNodes[num].Attributes["index"].Value);
                            }
                            catch
                            {
                                option.BitIndex = int.Parse(xmlNode.ChildNodes[num].Attributes["value"].Value);
                            }
                            bitmask3.Options.Add(option);
                        }
                    }
                    return bitmask3;
                }
                case "enum8":
                {
                    mEnum8 enum2 = new mEnum8 {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Options = new List<mEnumOption>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        if (xmlNode.ChildNodes[num].Name.ToLower() == "option")
                        {
                            option2 = new mEnumOption {
                                Name = xmlNode.ChildNodes[num].Attributes["name"].Value,
                                Value = int.Parse(xmlNode.ChildNodes[num].Attributes["value"].Value)
                            };
                            enum2.Options.Add(option2);
                        }
                    }
                    return enum2;
                }
                case "enum16":
                {
                    mEnum16 enum3 = new mEnum16 {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Options = new List<mEnumOption>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        if (xmlNode.ChildNodes[num].Name.ToLower() == "option")
                        {
                            option2 = new mEnumOption {
                                Name = xmlNode.ChildNodes[num].Attributes["name"].Value,
                                Value = int.Parse(xmlNode.ChildNodes[num].Attributes["value"].Value)
                            };
                            enum3.Options.Add(option2);
                        }
                    }
                    return enum3;
                }
                case "enum32":
                {
                    mEnum32 enum4 = new mEnum32 {
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Options = new List<mEnumOption>()
                    };
                    for (num = 0; num < xmlNode.ChildNodes.Count; num++)
                    {
                        if (xmlNode.ChildNodes[num].Name.ToLower() == "option")
                        {
                            option2 = new mEnumOption {
                                Name = xmlNode.ChildNodes[num].Attributes["name"].Value,
                                Value = int.Parse(xmlNode.ChildNodes[num].Attributes["value"].Value)
                            };
                            enum4.Options.Add(option2);
                        }
                    }
                    return enum4;
                }
                case "int8":
                case "byte":
                    return new mByte { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "int16":
                case "short":
                    return new mInt16 { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "uint16":
                case "ushort":
                    return new mUInt16 { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "int32":
                case "long":
                case "int":
                    return new mInt32 { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "uint32":
                case "ulong":
                case "uint":
                    return new mUInt32 { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "single":
                case "float":
                    return new mFloat { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "string":
                case "str":
                    return new mString { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Length = int.Parse(xmlNode.Attributes["length"].Value)
                    };

                case "unic":
                case "unicode":
                    return new mUnicode { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value),
                        Length = int.Parse(xmlNode.Attributes["length"].Value)
                    };

                case "undefined":
                case "unknown":
                    return new mUndefined { 
                        Name = xmlNode.Attributes["name"].Value,
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Visible = bool.Parse(xmlNode.Attributes["visible"].Value)
                    };

                case "unused":
                case "blank":
                    return new mUnused { 
                        Offset = int.Parse(xmlNode.Attributes["offset"].Value),
                        Size = int.Parse(xmlNode.Attributes["size"].Value)
                    };

                default:
                    return new mValue { Attributes = mValue.ObjectAttributes.None };
            }
            block2.Name = xmlNode.Attributes["name"].Value;
            block2.Offset = int.Parse(xmlNode.Attributes["offset"].Value);
            block2.Visible = bool.Parse(xmlNode.Attributes["visible"].Value);
            block2.Real_Color = bool.Parse(xmlNode.Attributes["real"].Value);
            string str = xmlNode.Attributes["order"].Value;
            foreach (char ch in str)
            {
                str3 = ch.ToString().ToUpper();
                if (str3 != null)
                {
                    if (str3 != "R")
                    {
                        if (str3 == "G")
                        {
                            goto Label_07F1;
                        }
                        if (str3 == "B")
                        {
                            goto Label_0802;
                        }
                        if (str3 == "A")
                        {
                            goto Label_0813;
                        }
                        if (str3 == "N")
                        {
                            goto Label_0824;
                        }
                    }
                    else
                    {
                        block2.Color_Order.Add(ColorBlockPart.Red);
                    }
                }
                continue;
            Label_07F1:
                block2.Color_Order.Add(ColorBlockPart.Green);
                continue;
            Label_0802:
                block2.Color_Order.Add(ColorBlockPart.Blue);
                continue;
            Label_0813:
                block2.Color_Order.Add(ColorBlockPart.Alpha);
                continue;
            Label_0824:
                block2.Color_Order.Add(ColorBlockPart.None);
            }
            return block2;
        }

        public void WriteNode(XmlTextWriter xtw, mValue mVal)
        {
            mColorBlock block;
            switch (mVal.Attributes)
            {
                case mValue.ObjectAttributes.Comment:
                    xtw.WriteStartElement("comment");
                    xtw.WriteAttributeString("title", ((mComment) mVal).Title);
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteString(((mComment) mVal).Description);
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.ByteArray:
                    xtw.WriteStartElement("bytearray");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteAttributeString("length", ((mByteArray) mVal).Length.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.ColorBlock8:
                case mValue.ObjectAttributes.ColorBlock16:
                case mValue.ObjectAttributes.ColorBlock32:
                case mValue.ObjectAttributes.ColorBlockF:
                    block = (mColorBlock) mVal;
                    switch (mVal.Attributes)
                    {
                        case mValue.ObjectAttributes.ColorBlock8:
                            xtw.WriteStartElement("color8");
                            goto Label_0CDE;

                        case mValue.ObjectAttributes.ColorBlock16:
                            xtw.WriteStartElement("color16");
                            goto Label_0CDE;

                        case mValue.ObjectAttributes.ColorBlock32:
                            xtw.WriteStartElement("color32");
                            goto Label_0CDE;

                        case mValue.ObjectAttributes.ColorBlockF:
                            xtw.WriteStartElement("colorf");
                            goto Label_0CDE;
                    }
                    break;

                case mValue.ObjectAttributes.TagBlock:
                    xtw.WriteStartElement("struct");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteAttributeString("size", ((mTagBlock) mVal).Size.ToString());
                    foreach (mValue value2 in ((mTagBlock) mVal).Values)
                    {
                        WriteNode(xtw, value2);
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.TagData:
                    xtw.WriteStartElement("tagdata");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.TagReference:
                    xtw.WriteStartElement("tagref");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.StringID:
                    xtw.WriteStartElement("stringid");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Unicode:
                    xtw.WriteStartElement("unicode");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteAttributeString("length", ((mUnicode) mVal).Length.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.String:
                    xtw.WriteStartElement("string");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteAttributeString("length", ((mString) mVal).Length.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Bitmask8:
                    xtw.WriteStartElement("bitmask8");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    foreach (mBitOption option in ((mBitmask8) mVal).Options)
                    {
                        xtw.WriteStartElement("bit");
                        xtw.WriteAttributeString("name", option.Name);
                        xtw.WriteAttributeString("index", option.BitIndex.ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Bitmask16:
                    xtw.WriteStartElement("bitmask16");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    foreach (mBitOption option in ((mBitmask16) mVal).Options)
                    {
                        xtw.WriteStartElement("bit");
                        xtw.WriteAttributeString("name", option.Name);
                        xtw.WriteAttributeString("index", option.BitIndex.ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Bitmask32:
                    xtw.WriteStartElement("bitmask32");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    foreach (mBitOption option in ((mBitmask32) mVal).Options)
                    {
                        xtw.WriteStartElement("bit");
                        xtw.WriteAttributeString("name", option.Name);
                        xtw.WriteAttributeString("index", option.BitIndex.ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Float:
                    xtw.WriteStartElement("float");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Int16:
                    xtw.WriteStartElement("short");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.UInt16:
                    xtw.WriteStartElement("ushort");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Int32:
                    xtw.WriteStartElement("int");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.UInt32:
                    xtw.WriteStartElement("uint");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Byte:
                    xtw.WriteStartElement("byte");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Enum8:
                    xtw.WriteStartElement("enum8");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    foreach (mEnumOption option2 in ((mEnum8) mVal).Options)
                    {
                        xtw.WriteStartElement("option");
                        xtw.WriteAttributeString("name", option2.Name);
                        xtw.WriteAttributeString("value", option2.Value.ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Enum16:
                    xtw.WriteStartElement("enum16");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    foreach (mEnumOption option2 in ((mEnum16) mVal).Options)
                    {
                        xtw.WriteStartElement("option");
                        xtw.WriteAttributeString("name", option2.Name);
                        xtw.WriteAttributeString("value", option2.Value.ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Enum32:
                    xtw.WriteStartElement("enum32");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    foreach (mEnumOption option2 in ((mEnum32) mVal).Options)
                    {
                        xtw.WriteStartElement("option");
                        xtw.WriteAttributeString("name", option2.Name);
                        xtw.WriteAttributeString("value", option2.Value.ToString());
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Undefined:
                    xtw.WriteStartElement("undefined");
                    xtw.WriteAttributeString("name", mVal.Name);
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("visible", mVal.Visible.ToString());
                    xtw.WriteEndElement();
                    return;

                case mValue.ObjectAttributes.Unused:
                    xtw.WriteStartElement("unused");
                    xtw.WriteAttributeString("offset", mVal.Offset.ToString());
                    xtw.WriteAttributeString("size", ((mUnused) mVal).Size.ToString());
                    xtw.WriteEndElement();
                    return;

                default:
                    return;
            }
        Label_0CDE:
            xtw.WriteAttributeString("name", block.Name);
            xtw.WriteAttributeString("offset", block.Offset.ToString());
            xtw.WriteAttributeString("visible", mVal.Visible.ToString());
            xtw.WriteAttributeString("real", block.Real_Color.ToString());
            string str = "";
            using (List<ColorBlockPart>.Enumerator enumerator4 = block.Color_Order.GetEnumerator())
            {
                while (enumerator4.MoveNext())
                {
                    switch (enumerator4.Current)
                    {
                        case ColorBlockPart.None:
                            str = str + "N";
                            break;

                        case ColorBlockPart.Red:
                            str = str + "R";
                            break;

                        case ColorBlockPart.Green:
                            str = str + "G";
                            break;

                        case ColorBlockPart.Blue:
                            str = str + "B";
                            break;

                        case ColorBlockPart.Alpha:
                            str = str + "A";
                            break;
                    }
                }
            }
            xtw.WriteAttributeString("order", str);
            xtw.WriteEndElement();
        }

        public void WritePlugin(string fileName)
        {
            XmlTextWriter xtw = new XmlTextWriter(new FileStream(fileName, FileMode.Create), Encoding.ASCII) {
                Formatting = Formatting.Indented
            };
            xtw.WriteStartElement("plugin");
            xtw.WriteAttributeString("class", TagClass);
            xtw.WriteAttributeString("headersize", HeaderSize.ToString());
            foreach (mRevision revision in Revisions)
            {
                xtw.WriteStartElement("revision");
                xtw.WriteAttributeString("author", revision.Author);
                xtw.WriteAttributeString("version", revision.Version.ToString());
                xtw.WriteString(revision.Description);
                xtw.WriteEndElement();
            }
            foreach (mValue value2 in ValueList)
            {
                WriteNode(xtw, value2);
            }
            xtw.WriteEndElement();
            xtw.Close();
        }

        public int HeaderSize
        {
            get
            {
                return _headersize;
            }
            set
            {
                _headersize = value;
            }
        }

        public List<mRevision> Revisions
        {
            get
            {
                return _revisions;
            }
            set
            {
                _revisions = value;
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

        public List<mValue> ValueList
        {
            get
            {
                return _valuelist;
            }
            set
            {
                _valuelist = value;
            }
        }
    }
}

