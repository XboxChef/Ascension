namespace Ascension.Details
{
    using System;
    using System.Collections.Generic;

    public static class ChangeLogs
    {
        public static Change[] ChangeLog = new Change[] { 
            new Change(0.01f, ChangeType.Add, "Created UI"), new Change(0.01f, ChangeType.Add, "Built settings"), new Change(0.02f, ChangeType.Add, "Reach prebeta support added. Locale rebuilding works. Plugin generation works. Tags load. Need to do Tag Editor and finish Tag Grid."), new Change(0.021f, ChangeType.Add, "Added feature to force load maps."), new Change(0.021f, ChangeType.Add, "Added tag void support to plugin generation."), new Change(0.022f, ChangeType.Add, "Added screenshot capabilities."), new Change(0.022f, ChangeType.Add, "Added multi-tab support."), new Change(0.022f, ChangeType.Add, "Added ident swapping in tag grid."), new Change(0.022f, ChangeType.Add, "Added ident poking in tag grid."), new Change(0.022f, ChangeType.Add, "Added go-to tag for idents in tag grid."), new Change(0.022f, ChangeType.Add, "Added shortcut keys for menu strip."), new Change(0.022f, ChangeType.Add, "Tag editor can now load and save values."), new Change(0.023f, ChangeType.Add, "Tag editor can now poke changes and poke all."), new Change(0.023f, ChangeType.Add, "Added bitmap viewer. This uses your map folder for external maps such as shared.map."), new Change(0.024f, ChangeType.Fix, "Fixed Locales and String viewer to open in the current tab.."), new Change(0.024f, ChangeType.Fix, "Fixed an output message when viewing bitmaps."),
            new Change(0.024f, ChangeType.Update, "Unsupported bitmaps will now try and load regardless, so you can toy with them. Take a look at your bitmaps before saving, because they may be one of the unsupported ones."), new Change(0.024f, ChangeType.Update, "Moved alot more message boxes to our output box in our MapForm."), new Change(0.024f, ChangeType.Add, "Added clear option to output box."), new Change(0.024f, ChangeType.Add, "Added display information for all value types in tag editor."), new Change(0.024f, ChangeType.Add, "Added refresh to tag editor."), new Change(0.024f, ChangeType.Update, "Plugin path can now be set in settings."), new Change(0.024f, ChangeType.Update, "Tag Grid now doesn't reload for every tag. The currently open tag grid will switch smoothly insteead of deleting and creating a new instance."), new Change(0.025f, ChangeType.Update, "Prepared for public release, updates are now optional, login/authentication system removed."), new Change(0.025f, ChangeType.Add, "Added Reach public beta map loading."), new Change(0.025f, ChangeType.Add, "Added Reach public beta print cam debug info."), new Change(0.025f, ChangeType.Add, "Added an About window."), new Change(0.025f, ChangeType.Fix, "Fixed a bug with opening Strings & Locales in a current tab."), new Change(0.025f, ChangeType.Remove, "Removed application protection. This program will go open source under a license disallowing anyone to add onto this project, only allowing people to learn from the source."), new Change(0.026f, ChangeType.Add, "Added option to load maps as campaign or multiplayer."), new Change(0.026f, ChangeType.Add, "Added file patch format."), new Change(0.026f, ChangeType.Add, "Added tag header swap."),
            new Change(0.03f, ChangeType.Add, "Added Retail map loading."), new Change(0.03f, ChangeType.Add, "Added beta filename database and filename renaming."), new Change(0.03f, ChangeType.Add, "Added plugin editor."), new Change(0.03f, ChangeType.Add, "Added view value as."), new Change(0.03f, ChangeType.Add, "Jtag Poking, alongside Xdk poking."), new Change(0.03f, ChangeType.Remove, "Removed String Viewer so the Locale Editor can be used."), new Change(0.03f, ChangeType.Fix, "Locale editor working and ready."), new Change(0.03f, ChangeType.Fix, "Plugin Generator working and ready."), new Change(0.036f, ChangeType.Add, "AddedMeta editor display handler."), new Change(0.037f, ChangeType.Add, "Added Tag Comparison."), new Change(0.037f, ChangeType.Add, "Added Wiki."), new Change(0.038f, ChangeType.Add, "Added option to auto save pictures and where."), new Change(0.038f, ChangeType.Update, "Updated Settings for View Options."), new Change(0.038f, ChangeType.Add, "Added option to choose which ident swapper you want"), new Change(0.038f, ChangeType.Add, "Added option to auto load a map"), new Change(0.038f, ChangeType.Update, "Updated Settings for choice of XDK or Jtag."),
            new Change(0.038f, ChangeType.Add, "Added poke xex options for dev and jtag."), new Change(0.038f, ChangeType.Fix, "SID loading so its alot faster")
        };

        public static string GetAllChangeLogsString()
        {
            List<float> list = new List<float>();
            string str = "";
            for (int i = 0; i < ChangeLog.Length; i++)
            {
                if (!list.Contains(ChangeLog[i].Version))
                {
                    list.Add(ChangeLog[i].Version);
                }
            }
            foreach (float num2 in list)
            {
                str = str + "[Version : " + num2.ToString() + " ]\n";
                str = str + GetChangeLogString(num2) + "\n";
            }
            return str;
        }

        public static string GetChangeLogString() => 
            GetChangeLogString(AscensionDetails.Version);

        public static string GetChangeLogString(float Version)
        {
            string str = "";
            for (int i = 0; i < ChangeLog.Length; i++)
            {
                if ((ChangeLog[i].Version == Version) || (Version == 0f))
                {
                    str = str + ChangeLog[i].ToString() + Environment.NewLine;
                }
            }
            return str;
        }
    }
}

