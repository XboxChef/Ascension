// Decompiled with JetBrains decompiler
// Type: Ascension.Halo_Reach.Game.Tag_Editor.Classes.TagEditorSettings
// Assembly: Ascension, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EF346614-614E-466E-A94D-08ACEFC3D738
// Assembly location: C:\Users\SilentSerenity\Desktop\Ascension\Ascension.exe

using Ascension.Settings;

namespace Ascension.Halo_Reach.Game.Tag_Editor.Classes
{
    public abstract class TagEditorSettings
    {
        private static TagEditorSettings.VisibilitySettings _visibilitysettings = new TagEditorSettings.VisibilitySettings();

        public static TagEditorSettings.VisibilitySettings Visibility_Settings
        {
            get
            {
                return TagEditorSettings._visibilitysettings;
            }
            set
            {
                TagEditorSettings._visibilitysettings = value;
            }
        }

        public class VisibilitySettings
        {
            private bool _bitmasks = true;
            private bool _bytearray = true;
            private bool _color = true;
            private bool _comments = true;
            private bool _enum = true;
            private bool _ident = true;
            private bool _tagblock = true;
            private bool _tagdata = true;
            private bool _undefined = true;
            private bool _unused = false;
            private bool _other = true;
            private bool _stringid = true;
            private bool _invisibles = AppSettings.Settings.ShowInvisibles;

            public bool Bitmasks
            {
                get
                {
                    return _bitmasks;
                }
                set
                {
                    _bitmasks = value;
                }
            }

            public bool ByteArray
            {
                get
                {
                    return _bytearray;
                }
                set
                {
                    _bytearray = value;
                }
            }

            public bool Color
            {
                get
                {
                    return _color;
                }
                set
                {
                    _color = value;
                }
            }

            public bool Comments
            {
                get
                {
                    return _comments;
                }
                set
                {
                    _comments = value;
                }
            }

            public bool Enum
            {
                get
                {
                    return _enum;
                }
                set
                {
                    _enum = value;
                }
            }

            public bool Ident
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

            public bool TagBlock
            {
                get
                {
                    return _tagblock;
                }
                set
                {
                    _tagblock = value;
                }
            }

            public bool TagData
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

            public bool Undefined
            {
                get
                {
                    return _undefined;
                }
                set
                {
                    _undefined = value;
                }
            }

            public bool Unused
            {
                get
                {
                    return _unused;
                }
                set
                {
                    _unused = value;
                }
            }

            public bool Other
            {
                get
                {
                    return _other;
                }
                set
                {
                    _other = value;
                }
            }

            public bool StringID
            {
                get
                {
                    return _stringid;
                }
                set
                {
                    _stringid = value;
                }
            }

            public bool Invisibles
            {
                get
                {
                    return _invisibles;
                }
                set
                {
                    _invisibles = value;
                }
            }
        }
    }
}
