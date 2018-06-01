namespace Ascension.Details
{
    using System;
    using System.Collections.Generic;

    public abstract class AscensionDetails
    {
        private static Dictionary<string, string> _authSettings;
        private static string _password;
        private static string _username;
        private static float _version = 0.039f;
        public static bool showUpdateBoxOnStartup;

        protected AscensionDetails()
        {
        }

        public static Dictionary<string, string> AuthSettings
        {
            get
            {
                return _authSettings;
            }
            set
            {
                _authSettings = value;
            }
        }

        public static string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public static string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        public static float Version
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

