namespace Ascension.Settings
{
    using Ascension.Settings.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Windows.Forms;

    public class Settings : ISettings
    {
        private bool _adjustGamma = true;
        private bool _autoloadmap = false;
        private string _betapluginpath = (Application.StartupPath + @"\plugins\");
        private ForceLoadMapType _forceloadmaptype = ForceLoadMapType.LoadAsMultiplayer;
        private double _gammaValue = 0.5;
        private LastGridViews _lastgridview = LastGridViews.Structure;
        private string _loadedmap = "";
        private bool _oldIdent = false;
        private string _p1 = "";
        private string _p1d = "";
        private string _p1m = "";
        private string _p2 = "";
        private string _p2d = "";
        private string _p2m = "";
        private string _p3 = "";
        private string _p3d = "";
        private string _p3m = "";
        private string _pluginpath = (Application.StartupPath + @"\plugins\");
        private bool _resizeScreenshots = false;
        private string _savescreen = "";
        private bool _saveScreenshots = false;
        private int _screenshotHeight = 640;
        private int _screenshotWidth = 0x480;
        private bool _showinvisibles = false;
        private bool _showUpdatesOnstartup = true;
        private bool _showXEXmanager = false;
        private XboxType _xboxtype = XboxType.Jtag;
        private string _xdkname = "";
        private string gameslist = "";
        private string imageFolder = "";
        private string mapFolder = "";
        private string mapinfoFolder = "";
        private string settingsAuthor;
        public const double SettingsVersion = 0.06;

        [Category("Extra Settings"), Description("Games for advanced poker")]
        public string Additional_Games
        {
            get
            {
                return gameslist;
            }
            set
            {
                gameslist = value;
            }
        }

        [Description("Should we adjust the gamma?"), Category("Screenshot Settings")]
        public bool AdjustGamma
        {
            get
            {
                return _adjustGamma;
            }
            set
            {
                _adjustGamma = value;
            }
        }

        [Category("Extra Settings"), Description("If set to true a map will automaticly load on program startup if its in the folder")]
        public bool Auto_Load_Map
        {
            get
            {
                return _autoloadmap;
            }
            set
            {
                _autoloadmap = value;
            }
        }

        [Editor(typeof(UIFolderProperty), typeof(UITypeEditor)), Category("Plugin Paths"), Description("The location of the plugin folder to read plugins from.")]
        public string BetaPluginPath
        {
            get
            {
                return _betapluginpath;
            }
            set
            {
                _betapluginpath = value;
            }
        }

        [Description("Determines how to load all maps except ff10_prototype. Loading as campaign will not require respawn time, but isn't recommended."), Category("Force Load Settings")]
        public ForceLoadMapType Force_Load_MapType
        {
            get
            {
                return _forceloadmaptype;
            }
            set
            {
                _forceloadmaptype = value;
            }
        }

        [Category("Screenshot Settings"), Description("Gamma value to adjust by")]
        public double GammaValue
        {
            get
            {
                return _gammaValue;
            }
            set
            {
                _gammaValue = value;
            }
        }

        [Editor(typeof(UIFolderProperty), typeof(UITypeEditor)), Category("General Settings"), Description("Location of the /maps/images/ folder with all of our Halo Reach images to obtain previews from.")]
        public string Images_Folder
        {
            get
            {
                return imageFolder;
            }
            set
            {
                imageFolder = value;
            }
        }

        [Description("Name/Ip of Xbox"), Category("Connection Settings")]
        public string IP_and_XDK_Name
        {
            get
            {
                return _xdkname;
            }
            set
            {
                _xdkname = value;
            }
        }

        [Browsable(false), Category("Meta Grid Settings"), Description("Last grid view used"), Encrypted]
        public LastGridViews LastGridView
        {
            get
            {
                return _lastgridview;
            }
            set
            {
                _lastgridview = value;
            }
        }

        [Editor(typeof(UIFolderProperty), typeof(UITypeEditor)), Category("General Settings"), Description("Location of the maps folder with all of our Halo Reach Maps to obtain Raw Resources from.")]
        public string Map_Folder
        {
            get
            {
                return mapFolder;
            }
            set
            {
                mapFolder = value;
            }
        }

        [Category("Extra Settings"), Description("This determines what map will be loaded automaticly.")]
        public string Map_Loaded
        {
            get
            {
                return _loadedmap;
            }
            set
            {
                _loadedmap = value;
            }
        }

        [Description("Location of the /maps/info/ folder with all of our Halo Reach mapinfos to obtain map name and descriptions from."), Category("General Settings"), Editor(typeof(UIFolderProperty), typeof(UITypeEditor))]
        public string MapInfo_Folder
        {
            get
            {
                return mapinfoFolder;
            }
            set
            {
                mapinfoFolder = value;
            }
        }

        [Description("Use old Ident Swapper?"), Category("Meta Editor Settings")]
        public bool Old_Ident_Swapper
        {
            get
            {
                return _oldIdent;
            }
            set
            {
                _oldIdent = value;
            }
        }

        [Description("null"), Category("Patches")]
        public string Patch_1
        {
            get
            {
                return _p1;
            }
            set
            {
                _p1 = value;
            }
        }

        [Description("null"), Category("Patches")]
        public string Patch_1_Description
        {
            get
            {
                return _p1d;
            }
            set
            {
                _p1d = value;
            }
        }

        [Category("Patches"), Description("null")]
        public string Patch_1_Map
        {
            get
            {
                return _p1m;
            }
            set
            {
                _p1m = value;
            }
        }

        [Description("null"), Category("Patches")]
        public string Patch_2
        {
            get
            {
                return _p2;
            }
            set
            {
                _p2 = value;
            }
        }

        [Category("Patches"), Description("null")]
        public string Patch_2_Description
        {
            get
            {
                return _p2d;
            }
            set
            {
                _p2d = value;
            }
        }

        [Category("Patches"), Description("null")]
        public string Patch_2_Map
        {
            get
            {
                return _p2m;
            }
            set
            {
                _p2m = value;
            }
        }

        [Category("Patches"), Description("null")]
        public string Patch_3
        {
            get
            {
                return _p3;
            }
            set
            {
                _p3 = value;
            }
        }

        [Description("null"), Category("Patches")]
        public string Patch_3_Description
        {
            get
            {
                return _p3d;
            }
            set
            {
                _p3d = value;
            }
        }

        [Category("Patches"), Description("null")]
        public string Patch_3_Map
        {
            get
            {
                return _p3m;
            }
            set
            {
                _p3m = value;
            }
        }

        [Category("Plugin Path"), Editor(typeof(UIFolderProperty), typeof(UITypeEditor)), Description("The location of the plugin folder to read plugins from.")]
        public string PluginPath
        {
            get
            {
                return _pluginpath;
            }
            set
            {
                _pluginpath = value;
            }
        }

        [Category("Screenshot Settings"), Description("Should we resize our screenshots?")]
        public bool ResizeScreenshots
        {
            get
            {
                return _resizeScreenshots;
            }
            set
            {
                _resizeScreenshots = value;
            }
        }

        [Description("Automaticly Save ScreenShots?"), Category("Screenshot Settings")]
        public bool SaveScreenshots
        {
            get
            {
                return _saveScreenshots;
            }
            set
            {
                _saveScreenshots = value;
            }
        }

        [Description("If Resize Screenshots is set to true this will be the new height"), Category("Screenshot Settings")]
        public int ScreenshotHeight
        {
            get
            {
                return _screenshotHeight;
            }
            set
            {
                _screenshotHeight = value;
            }
        }

        [Category("Screenshot Settings"), Description("Where will screenshots save."), Editor(typeof(UIFolderProperty), typeof(UITypeEditor))]
        public string ScreenShotLocation
        {
            get
            {
                return _savescreen;
            }
            set
            {
                _savescreen = value;
            }
        }

        [Category("Screenshot Settings"), Description("If Resize Screenshots is set to true this will be the new width")]
        public int ScreenshotWidth { get; set; }

        [Browsable(false), Category("General Settings"), Description("User that created this settings file")]
        public string SettingsAuthor
        {
            get
            {
                return settingsAuthor;
            }
            set
            {
                settingsAuthor = value;
            }
        }

        [Description("Show invisibles?"), Category("Meta Editor Settings")]
        public bool ShowInvisibles
        {
            get
            {
                return _showinvisibles;
            }
            set
            {
                _showinvisibles = value;
            }
        }

        [Description("If set to true and an update exists, when the program is started, the update form will be shown."), Category("General Settings")]
        public bool ShowUpdatesOnStartup
        {
            get
            {
                return _showUpdatesOnstartup;
            }
            set
            {
                _showUpdatesOnstartup = value;
            }
        }

        [Description("Chooses to poke to Jtag or Developers Kit. Program will need to be restarted if changed."), Browsable(false), Category("Connection Settings")]
        public XboxType Xbox_Type
        {
            get
            {
                return _xboxtype;
            }
            set
            {
                _xboxtype = value;
            }
        }

        [Browsable(false), Description("If set to true and an update exists, when the program is started, the update form will be shown."), Category("General Settings")]
        public bool XEXManager
        {
            get
            {
                return _showXEXmanager;
            }
            set
            {
                _showXEXmanager = value;
            }
        }

        public enum ForceLoadMapType
        {
            LoadAsCampaign = 1,
            LoadAsMultiplayer = 3
        }

        public enum LastGridViews : byte
        {
            Ident = 1,
            Strings = 2,
            Structure = 0,
            Voids = 3
        }

        public enum XboxType
        {
            Jtag = 1,
            XDK = 2
        }
    }
}

