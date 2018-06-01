namespace HaloDevelopmentExtender
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using XDevkit;

    public class XboxDebugCommunicator
    {
        private bool _connected;
        private uint _connectioncode;
        private FileSystem _filesystem;
        private XboxConsole _xboxconsole;
        private XboxManagerClass _xboxmanager;
        private string _xboxname;
        private string _xboxtype;
        public const float Version = 1f;

        public XboxDebugCommunicator(string xboxName)
        {
            this.XboxName = xboxName;
        }

        public void Connect()
        {
            if (!this.Connected)
            {
                this.Xbox_Manager = new XboxManagerClass();
                this.Xbox_Console = this.Xbox_Manager.OpenConsole(this.XboxName);
                this.Connection_Code = this.Xbox_Console.OpenConnection(null);
                try
                {
                    this.Xbox_Type = this.Xbox_Console.ConsoleType.ToString();
                }
                catch
                {
                }
                this.Connected = true;
            }
        }

        public void Disconnect()
        {
            if (this.Connected)
            {
                this.SendTextCommand("bye");
                this.Xbox_Console.CloseConnection(this.Connection_Code);
                this.File_System = null;
                this.Connected = false;
            }
        }

        public void Freeze()
        {
            this.SendTextCommand("stop");
        }

        public void Reboot(RebootFlags reboottype)
        {
            switch (reboottype)
            {
                case RebootFlags.Warm:
                    this.SendTextCommand("reboot");
                    break;

                case RebootFlags.Cold:
                    this.SendTextCommand("reboot");
                    break;
            }
            if (reboottype == RebootFlags.Cold)
            {
                try
                {
                    this.Disconnect();
                }
                catch
                {
                    Console.WriteLine("Error Disconnecting");
                }
            }
        }

        public void RefreshFilesystem()
        {
            this.File_System = new FileSystem(this.Xbox_Console);
        }

        public XboxMemoryStream ReturnXboxMemoryStream() => 
            new XboxMemoryStream(this.Xbox_Console.DebugTarget);

        public void Screenshot(string savePath)
        {
            this.Xbox_Console.ScreenShot(savePath);
        }

        public string SendTextCommand(string Command)
        {
            string response = "";
            this.Xbox_Console.SendTextCommand(this.Connection_Code, Command, out response);
            if (response.Contains("202") | response.Contains("203"))
            {
                try
                {
                    string str2;
                    bool flag;
                    goto Label_008F;
                Label_003E:
                    str2 = "";
                    this.Xbox_Console.ReceiveSocketLine(this.Connection_Code, out str2);
                    if (str2.Length > 0)
                    {
                        if (str2[0] == '.')
                        {
                            return response;
                        }
                        response = response + Environment.NewLine + str2;
                    }
                Label_008F:
                    flag = true;
                    goto Label_003E;
                }
                catch
                {
                }
            }
            return response;
        }

        public void Unfreeze()
        {
            this.SendTextCommand("go");
        }

        public bool Connected
        {
            get
            {
                return this._connected;
            }
            set
            {
                this._connected = value;
            }
        }

        public uint Connection_Code
        {
            get
            {
                return this._connectioncode;
            }
            set
            {
                this._connectioncode = value;
            }
        }

        public FileSystem File_System
        {
            get
            {
                return this._filesystem;
            }
            set
            {
                this._filesystem = value;
            }
        }

        private XboxConsole Xbox_Console
        {
            get
            {
                return this._xboxconsole;
            }
            set
            {
                this._xboxconsole = value;
            }
        }

        private XboxManagerClass Xbox_Manager
        {
            get
            {
                return this._xboxmanager;
            }
            set
            {
                this._xboxmanager = value;
            }
        }

        public string Xbox_Type
        {
            get
            {
                return this._xboxtype;
            }
            set
            {
                this._xboxtype = value;
            }
        }

        public string XboxName
        {
            get
            {
                return this._xboxname;
            }
            set
            {
                this._xboxname = value;
            }
        }

        public class FileSystem
        {
            private string[] _drives;
            private XboxConsole _xboxconsole;

            public FileSystem(XboxConsole xboxConsole)
            {
                this.Xbox_Console = xboxConsole;
                this.Drives = this.Xbox_Console.Drives.Split(new char[] { ',' });
            }

            public void CreateDirectory(string directory)
            {
                this.Xbox_Console.MakeDirectory(directory);
            }

            public void DeleteDirectory(string directory)
            {
                string[] files = this.GetFiles(directory);
                foreach (string str in files)
                {
                    this.DeleteFile(str);
                }
                string[] directories = this.GetDirectories(directory);
                foreach (string str2 in directories)
                {
                    this.DeleteDirectory(str2);
                }
                this.Xbox_Console.RemoveDirectory(directory);
            }

            public void DeleteFile(string remoteName)
            {
                this.Xbox_Console.DeleteFile(remoteName);
            }

            public void DownloadDirectory(string localFolderToSaveIn, string remoteFolderPath)
            {
                string str4;
                string[] files = this.GetFiles(remoteFolderPath);
                string[] directories = this.GetDirectories(remoteFolderPath);
                if (remoteFolderPath[remoteFolderPath.Length - 1] == '\\')
                {
                    remoteFolderPath = remoteFolderPath.Substring(0, remoteFolderPath.Length - 1);
                }
                if (localFolderToSaveIn[localFolderToSaveIn.Length - 1] == '\\')
                {
                    localFolderToSaveIn = localFolderToSaveIn.Substring(0, localFolderToSaveIn.Length - 1);
                }
                string[] strArray3 = remoteFolderPath.Split(new char[] { '\\' });
                string str = strArray3[strArray3.Length - 1];
                string path = localFolderToSaveIn + @"\" + str + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (string str3 in files)
                {
                    strArray3 = str3.Split(new char[] { '\\' });
                    str4 = strArray3[strArray3.Length - 1];
                    this.DownloadFile(path + str4, str3);
                }
                foreach (string str5 in directories)
                {
                    strArray3 = str5.Split(new char[] { '\\' });
                    str4 = strArray3[strArray3.Length - 1];
                    this.DownloadDirectory(path, str5);
                }
            }

            public void DownloadFile(string localName, string remoteName)
            {
                this.Xbox_Console.ReceiveFile(localName, remoteName);
            }

            public string[] GetDirectories(string directory)
            {
                IXboxFiles files = this.Xbox_Console.DirectoryFiles(directory);
                List<string> list = new List<string>();
                foreach (IXboxFile file in files)
                {
                    if (file.IsDirectory)
                    {
                        list.Add(file.Name);
                    }
                }
                return list.ToArray();
            }

            public string[] GetFiles(string directory)
            {
                IXboxFiles files = this.Xbox_Console.DirectoryFiles(directory);
                List<string> list = new List<string>();
                foreach (IXboxFile file in files)
                {
                    if (!file.IsDirectory)
                    {
                        list.Add(file.Name);
                    }
                }
                return list.ToArray();
            }

            public void RenameFile(string remoteName, string newRemoteName)
            {
                this.Xbox_Console.RenameFile(remoteName, newRemoteName);
            }

            public void UploadDirectory(string localFolder, string remoteFolderToSaveIn)
            {
                string str4;
                string[] files = Directory.GetFiles(localFolder);
                string[] directories = Directory.GetDirectories(localFolder);
                if (localFolder[localFolder.Length - 1] == '\\')
                {
                    localFolder = localFolder.Substring(0, localFolder.Length - 1);
                }
                if (remoteFolderToSaveIn[remoteFolderToSaveIn.Length - 1] == '\\')
                {
                    remoteFolderToSaveIn = remoteFolderToSaveIn.Substring(0, remoteFolderToSaveIn.Length - 1);
                }
                string[] strArray3 = localFolder.Split(new char[] { '\\' });
                string str = strArray3[strArray3.Length - 1];
                string directory = remoteFolderToSaveIn + @"\" + str + @"\";
                this.CreateDirectory(directory);
                foreach (string str3 in files)
                {
                    strArray3 = str3.Split(new char[] { '\\' });
                    str4 = strArray3[strArray3.Length - 1];
                    this.UploadFile(str3, directory + str4);
                }
                foreach (string str5 in directories)
                {
                    strArray3 = str5.Split(new char[] { '\\' });
                    str4 = strArray3[strArray3.Length - 1];
                    this.UploadDirectory(str5, directory);
                }
            }

            public void UploadFile(string localName, string remoteName)
            {
                this.Xbox_Console.SendFile(localName, remoteName);
            }

            public string[] Drives
            {
                get
                {
                    return this._drives;
                }
                set
                {
                    this._drives = value;
                }
            }

            public XboxConsole Xbox_Console
            {
                get
                {
                    return this._xboxconsole;
                }
                set
                {
                    this._xboxconsole = value;
                }
            }
        }

        public enum RebootFlags
        {
            Warm,
            Cold
        }
    }
}

