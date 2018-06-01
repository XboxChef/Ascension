namespace Ascension.Halo_Reach.Game.Tag_Editor.Classes
{
    using Ascension.Halo_Reach.Game.Tag_Editor.Controls;
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class TagEditorHandler
    {
        public static void LoadPluginUI(List<mValue> ValueList, Panel parentPanel, List<string> sid)
        {
            int y = 1;
            foreach (mValue value2 in ValueList)
            {
                uiTagBlock block;
                uiBitmask bitmask;
                uiByteArray array;
                uiColor color;
                uiComment comment;
                uiEnum enum2;
                uiStringID gid;
                uiIdent ident;
                uiTagData data;
                uiValue value3;
                if (value2.Visible | TagEditorSettings.Visibility_Settings.Invisibles)
                {
                    switch (value2.Attributes)
                    {
                        case mValue.ObjectAttributes.Comment:
                            if (TagEditorSettings.Visibility_Settings.Comments)
                            {
                                goto Label_0226;
                            }
                            break;

                        case mValue.ObjectAttributes.ByteArray:
                            if (TagEditorSettings.Visibility_Settings.ByteArray)
                            {
                                goto Label_0188;
                            }
                            break;

                        case mValue.ObjectAttributes.ColorBlock8:
                        case mValue.ObjectAttributes.ColorBlock16:
                        case mValue.ObjectAttributes.ColorBlock32:
                        case mValue.ObjectAttributes.ColorBlockF:
                            if (TagEditorSettings.Visibility_Settings.Color)
                            {
                                goto Label_01D7;
                            }
                            break;

                        case mValue.ObjectAttributes.TagBlock:
                            if (TagEditorSettings.Visibility_Settings.TagBlock)
                            {
                                goto Label_00D1;
                            }
                            break;

                        case mValue.ObjectAttributes.TagData:
                            if (TagEditorSettings.Visibility_Settings.TagData)
                            {
                                goto Label_0363;
                            }
                            break;

                        case mValue.ObjectAttributes.TagReference:
                            if (TagEditorSettings.Visibility_Settings.Ident)
                            {
                                goto Label_0314;
                            }
                            break;

                        case mValue.ObjectAttributes.StringID:
                            if (TagEditorSettings.Visibility_Settings.StringID)
                            {
                                goto Label_02C4;
                            }
                            break;

                        case mValue.ObjectAttributes.Unicode:
                        case mValue.ObjectAttributes.String:
                        case mValue.ObjectAttributes.Float:
                        case mValue.ObjectAttributes.Int16:
                        case mValue.ObjectAttributes.UInt16:
                        case mValue.ObjectAttributes.Int32:
                        case mValue.ObjectAttributes.UInt32:
                        case mValue.ObjectAttributes.Byte:
                        case mValue.ObjectAttributes.None:
                            if (TagEditorSettings.Visibility_Settings.Other)
                            {
                                goto Label_03B2;
                            }
                            break;

                        case mValue.ObjectAttributes.Bitmask8:
                        case mValue.ObjectAttributes.Bitmask16:
                        case mValue.ObjectAttributes.Bitmask32:
                            if (TagEditorSettings.Visibility_Settings.Bitmasks)
                            {
                                goto Label_013D;
                            }
                            break;

                        case mValue.ObjectAttributes.Enum8:
                        case mValue.ObjectAttributes.Enum16:
                        case mValue.ObjectAttributes.Enum32:
                            if (TagEditorSettings.Visibility_Settings.Enum)
                            {
                                goto Label_0275;
                            }
                            break;

                        case mValue.ObjectAttributes.Undefined:
                            if (TagEditorSettings.Visibility_Settings.Undefined)
                            {
                                value3 = new uiValue(value2) {
                                    Location = new Point(0, y)
                                };
                                y += value3.Height;
                                parentPanel.Controls.Add(value3);
                            }
                            break;

                        case mValue.ObjectAttributes.Slider:
                            if (TagEditorSettings.Visibility_Settings.Other)
                            {
                                uiSlider slider = new uiSlider(value2) {
                                    Location = new Point(0, y)
                                };
                                y += slider.Height;
                                parentPanel.Controls.Add(slider);
                            }
                            break;
                    }
                }
                continue;
            Label_00D1:
                block = new uiTagBlock((mTagBlock) value2);
                block.Location = new Point(6, y);
                LoadPluginUI(((mTagBlock) value2).Values, block.returnValuePanel(), sid);
                block.ResizeTagBlock();
                y += block.Height + 2;
                parentPanel.Controls.Add(block);
                continue;
            Label_013D:
                bitmask = new uiBitmask((mBitmask) value2);
                bitmask.Location = new Point(0, y);
                y += bitmask.Height;
                parentPanel.Controls.Add(bitmask);
                continue;
            Label_0188:
                array = new uiByteArray((mByteArray) value2);
                array.Location = new Point(0, y);
                y += array.Height;
                parentPanel.Controls.Add(array);
                continue;
            Label_01D7:
                color = new uiColor((mColorBlock) value2);
                color.Location = new Point(0, y);
                y += color.Height;
                parentPanel.Controls.Add(color);
                continue;
            Label_0226:
                comment = new uiComment((mComment) value2);
                comment.Location = new Point(0, y);
                y += comment.Height;
                parentPanel.Controls.Add(comment);
                continue;
            Label_0275:
                enum2 = new uiEnum((mEnum) value2);
                enum2.Location = new Point(0, y);
                y += enum2.Height;
                parentPanel.Controls.Add(enum2);
                continue;
            Label_02C4:
                gid = new uiStringID((mStringID) value2, sid);
                gid.Location = new Point(0, y);
                y += gid.Height;
                parentPanel.Controls.Add(gid);
                continue;
            Label_0314:
                ident = new uiIdent((mTagReference) value2);
                ident.Location = new Point(0, y);
                y += ident.Height;
                parentPanel.Controls.Add(ident);
                continue;
            Label_0363:
                data = new uiTagData((mTagData) value2);
                data.Location = new Point(0, y);
                y += data.Height;
                parentPanel.Controls.Add(data);
                continue;
            Label_03B2:
                value3 = new uiValue(value2);
                value3.Location = new Point(0, y);
                y += value3.Height;
                parentPanel.Controls.Add(value3);
            }
        }

        public static void LoadPluginUIAndValues(HaloMap Map, List<mValue> ValueList, Panel parentPanel, int parentOffset, List<string> sid)
        {
            int y = 1;
            if (!((parentOffset <= 0) | (parentOffset > Map.Map_Header.fileSize)))
            {
                EndianIO iO = Map.IO;
                foreach (mValue value2 in ValueList)
                {
                    uiTagBlock block;
                    uiBitmask bitmask;
                    uiByteArray array;
                    uiColor color;
                    uiComment comment;
                    uiEnum enum2;
                    uiStringID gid;
                    uiIdent ident;
                    uiTagData data;
                    uiValue value3;
                    uiSlider slider;
                    if (!((Map.IO != null) && Map.IO.Opened))
                    {
                        Map.OpenIO();
                    }
                    if (value2.Visible | TagEditorSettings.Visibility_Settings.Invisibles)
                    {
                        switch (value2.Attributes)
                        {
                            case mValue.ObjectAttributes.Comment:
                                if (TagEditorSettings.Visibility_Settings.Comments)
                                {
                                    goto Label_02A1;
                                }
                                break;

                            case mValue.ObjectAttributes.ByteArray:
                                if (TagEditorSettings.Visibility_Settings.ByteArray)
                                {
                                    goto Label_01EF;
                                }
                                break;

                            case mValue.ObjectAttributes.ColorBlock8:
                            case mValue.ObjectAttributes.ColorBlock16:
                            case mValue.ObjectAttributes.ColorBlock32:
                            case mValue.ObjectAttributes.ColorBlockF:
                                if (TagEditorSettings.Visibility_Settings.Color)
                                {
                                    goto Label_0248;
                                }
                                break;

                            case mValue.ObjectAttributes.TagBlock:
                                if (TagEditorSettings.Visibility_Settings.TagBlock)
                                {
                                    goto Label_0120;
                                }
                                break;

                            case mValue.ObjectAttributes.TagData:
                                if (TagEditorSettings.Visibility_Settings.TagData)
                                {
                                    goto Label_03FD;
                                }
                                break;

                            case mValue.ObjectAttributes.TagReference:
                                if (TagEditorSettings.Visibility_Settings.Ident)
                                {
                                    goto Label_03A4;
                                }
                                break;

                            case mValue.ObjectAttributes.StringID:
                                if (TagEditorSettings.Visibility_Settings.StringID)
                                {
                                    goto Label_0349;
                                }
                                break;

                            case mValue.ObjectAttributes.Unicode:
                            case mValue.ObjectAttributes.String:
                            case mValue.ObjectAttributes.Float:
                            case mValue.ObjectAttributes.Int16:
                            case mValue.ObjectAttributes.UInt16:
                            case mValue.ObjectAttributes.Int32:
                            case mValue.ObjectAttributes.UInt32:
                            case mValue.ObjectAttributes.Byte:
                            case mValue.ObjectAttributes.None:
                                if (TagEditorSettings.Visibility_Settings.Other)
                                {
                                    goto Label_0456;
                                }
                                break;

                            case mValue.ObjectAttributes.Bitmask8:
                            case mValue.ObjectAttributes.Bitmask16:
                            case mValue.ObjectAttributes.Bitmask32:
                                if (TagEditorSettings.Visibility_Settings.Bitmasks)
                                {
                                    goto Label_0196;
                                }
                                break;

                            case mValue.ObjectAttributes.Enum8:
                            case mValue.ObjectAttributes.Enum16:
                            case mValue.ObjectAttributes.Enum32:
                                if (TagEditorSettings.Visibility_Settings.Enum)
                                {
                                    goto Label_02F0;
                                }
                                break;

                            case mValue.ObjectAttributes.Undefined:
                                if (TagEditorSettings.Visibility_Settings.Undefined)
                                {
                                    value3 = new uiValue(value2);
                                    value3.LoadValue(Map, parentOffset);
                                    value3.Location = new Point(0, y);
                                    y += value3.Height;
                                    parentPanel.Controls.Add(value3);
                                }
                                break;

                            case mValue.ObjectAttributes.Slider:
                                if (TagEditorSettings.Visibility_Settings.Other)
                                {
                                    goto Label_04AA;
                                }
                                break;
                        }
                    }
                    continue;
                Label_0120:
                    block = new uiTagBlock((mTagBlock) value2);
                    block.Location = new Point(6, y);
                    LoadPluginUI(((mTagBlock) value2).Values, block.returnValuePanel(), sid);
                    block.LoadStructure(Map, parentOffset);
                    block.ResizeTagBlock();
                    y += block.Height + 2;
                    parentPanel.Controls.Add(block);
                    continue;
                Label_0196:
                    bitmask = new uiBitmask((mBitmask) value2);
                    bitmask.LoadValue(Map, parentOffset);
                    bitmask.Location = new Point(0, y);
                    y += bitmask.Height;
                    parentPanel.Controls.Add(bitmask);
                    continue;
                Label_01EF:
                    array = new uiByteArray((mByteArray) value2);
                    array.LoadValue(Map, parentOffset);
                    array.Location = new Point(0, y);
                    y += array.Height;
                    parentPanel.Controls.Add(array);
                    continue;
                Label_0248:
                    color = new uiColor((mColorBlock) value2);
                    color.LoadValue(Map, parentOffset);
                    color.Location = new Point(0, y);
                    y += color.Height;
                    parentPanel.Controls.Add(color);
                    continue;
                Label_02A1:
                    comment = new uiComment((mComment) value2);
                    comment.Location = new Point(0, y);
                    y += comment.Height;
                    parentPanel.Controls.Add(comment);
                    continue;
                Label_02F0:
                    enum2 = new uiEnum((mEnum) value2);
                    enum2.LoadValue(Map, parentOffset);
                    enum2.Location = new Point(0, y);
                    y += enum2.Height;
                    parentPanel.Controls.Add(enum2);
                    continue;
                Label_0349:
                    gid = new uiStringID((mStringID) value2, sid);
                    gid.LoadValue(Map, parentOffset);
                    gid.Location = new Point(0, y);
                    y += gid.Height;
                    parentPanel.Controls.Add(gid);
                    continue;
                Label_03A4:
                    ident = new uiIdent((mTagReference) value2);
                    ident.LoadValue(Map, parentOffset);
                    ident.Location = new Point(0, y);
                    y += ident.Height;
                    parentPanel.Controls.Add(ident);
                    continue;
                Label_03FD:
                    data = new uiTagData((mTagData) value2);
                    data.LoadValue(Map, parentOffset);
                    data.Location = new Point(0, y);
                    y += data.Height;
                    parentPanel.Controls.Add(data);
                    continue;
                Label_0456:
                    value3 = new uiValue(value2);
                    value3.LoadValue(Map, parentOffset);
                    value3.Location = new Point(0, y);
                    y += value3.Height;
                    parentPanel.Controls.Add(value3);
                    continue;
                Label_04AA:
                    slider = new uiSlider(value2);
                    slider.LoadValue(Map, parentOffset);
                    slider.Location = new Point(0, y);
                    y += slider.Height;
                    parentPanel.Controls.Add(slider);
                }
            }
        }

        public static void LoadPluginValues(HaloMap Map, Panel parentPanel, int parentOffset)
        {
            if (!((parentOffset <= 0) | (parentOffset > Map.Map_Header.fileSize)))
            {
                EndianIO iO = Map.IO;
                for (int i = 0; i < parentPanel.Controls.Count; i++)
                {
                    if (!((Map.IO != null) && Map.IO.Opened))
                    {
                        Map.OpenIO();
                    }
                    switch (parentPanel.Controls[i].Name)
                    {
                        case "uiTagBlock":
                            ((uiTagBlock) parentPanel.Controls[i]).LoadStructure(Map, parentOffset);
                            break;

                        case "uiBitmask":
                            ((uiBitmask) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiValue":
                            ((uiValue) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiSlider":
                            ((uiSlider) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiEnum":
                            ((uiEnum) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiIdent":
                            ((uiIdent) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiByteArray":
                            ((uiByteArray) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiTagData":
                            ((uiTagData) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiColor":
                            ((uiColor) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;

                        case "uiStringID":
                            ((uiStringID) parentPanel.Controls[i]).LoadValue(Map, parentOffset);
                            break;
                    }
                }
                Map.CloseIO();
            }
        }

        public static void Panels(Panel parentPanel)
        {
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                string name = parentPanel.Controls[i].Name;
                if ((name != null) && (name == "uiTagBlock"))
                {
                    ((uiTagBlock) parentPanel.Controls[i]).VIZ();
                }
            }
        }

        public static void PokeValues(EndianIO IO, Panel parentPanel, int parentOffset, int magic, bool onlyChanged)
        {
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                uiBitmask bitmask;
                uiValue value2;
                uiEnum enum2;
                uiIdent ident;
                switch (parentPanel.Controls[i].Name)
                {
                    case "uiTagBlock":
                    {
                        ((uiTagBlock) parentPanel.Controls[i]).PokeStructure(IO, magic, onlyChanged);
                        continue;
                    }
                    case "uiBitmask":
                    {
                        bitmask = (uiBitmask) parentPanel.Controls[i];
                        if (!onlyChanged)
                        {
                            break;
                        }
                        if (bitmask.Editted)
                        {
                            bitmask.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiValue":
                    {
                        value2 = (uiValue) parentPanel.Controls[i];
                        if (!onlyChanged)
                        {
                            goto Label_019C;
                        }
                        if (value2.Editted)
                        {
                            value2.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiEnum":
                    {
                        enum2 = (uiEnum) parentPanel.Controls[i];
                        if (!onlyChanged)
                        {
                            goto Label_01EF;
                        }
                        if (enum2.Editted)
                        {
                            enum2.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiIdent":
                    {
                        ident = (uiIdent) parentPanel.Controls[i];
                        if (!onlyChanged)
                        {
                            goto Label_0243;
                        }
                        if (ident.Editted)
                        {
                            ident.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiByteArray":
                    {
                        uiByteArray array = (uiByteArray) parentPanel.Controls[i];
                        if (array.Editted | !onlyChanged)
                        {
                            array.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiColor":
                    {
                        uiColor color = (uiColor) parentPanel.Controls[i];
                        if (color.Editted | !onlyChanged)
                        {
                            color.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiTagData":
                    {
                        uiTagData data = (uiTagData) parentPanel.Controls[i];
                        if (data.Editted | !onlyChanged)
                        {
                            data.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    case "uiStringID":
                    {
                        uiStringID gid = (uiStringID) parentPanel.Controls[i];
                        if (gid.Editted)
                        {
                            gid.SaveValue(IO, parentOffset + magic);
                        }
                        continue;
                    }
                    default:
                    {
                        continue;
                    }
                }
                bitmask.SaveValue(IO, parentOffset + magic);
                continue;
            Label_019C:
                value2.SaveValue(IO, parentOffset + magic);
                continue;
            Label_01EF:
                enum2.SaveValue(IO, parentOffset + magic);
                continue;
            Label_0243:
                ident.SaveValue(IO, parentOffset + magic);
            }
        }

        public static void SaveChangedValues(HaloMap Map, Panel parentPanel, int parentOffset)
        {
            EndianIO iO = Map.IO;
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                if (!((Map.IO != null) && Map.IO.Opened))
                {
                    Map.OpenIO();
                }
                switch (parentPanel.Controls[i].Name)
                {
                    case "uiTagBlock":
                        ((uiTagBlock) parentPanel.Controls[i]).SaveStructure(Map, parentOffset);
                        break;

                    case "uiBitmask":
                    {
                        uiBitmask bitmask = (uiBitmask) parentPanel.Controls[i];
                        if (bitmask.Editted)
                        {
                            bitmask.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiValue":
                    {
                        uiValue value2 = (uiValue) parentPanel.Controls[i];
                        if (value2.Editted)
                        {
                            value2.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiEnum":
                    {
                        uiEnum enum2 = (uiEnum) parentPanel.Controls[i];
                        if (enum2.Editted)
                        {
                            enum2.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiIdent":
                    {
                        uiIdent ident = (uiIdent) parentPanel.Controls[i];
                        if (ident.Editted)
                        {
                            ident.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiByteArray":
                    {
                        uiByteArray array = (uiByteArray) parentPanel.Controls[i];
                        if (array.Editted)
                        {
                            array.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiColor":
                    {
                        uiColor color = (uiColor) parentPanel.Controls[i];
                        if (color.Editted)
                        {
                            color.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiTagData":
                    {
                        uiTagData data = (uiTagData) parentPanel.Controls[i];
                        if (data.Editted)
                        {
                            data.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                    case "uiStringID":
                    {
                        uiStringID gid = (uiStringID) parentPanel.Controls[i];
                        if (gid.Editted)
                        {
                            gid.SaveValue(Map.IO, parentOffset);
                        }
                        break;
                    }
                }
            }
            Map.CloseIO();
        }

        public static void VVA(Panel parentPanel, bool viewing)
        {
            for (int i = 0; i < parentPanel.Controls.Count; i++)
            {
                switch (parentPanel.Controls[i].Name)
                {
                    case "uiValue":
                        ((uiValue) parentPanel.Controls[i]).VVA(viewing);
                        break;

                    case "uiTagBlock":
                        ((uiTagBlock) parentPanel.Controls[i]).VVA(viewing);
                        break;
                }
            }
        }
    }
}

