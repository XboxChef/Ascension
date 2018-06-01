using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

public class FileDownloader
{
    private Form _downloadingform;
    private ProgressBar _progressbar;
    private bool goPause;
    private string LocalPath;
    private static int PercentProgress;
    private Stream strLocal;
    private Stream strResponse;
    private Thread thrDownload;
    private string Url;
    private HttpWebRequest webRequest;
    private HttpWebResponse webResponse;

    public FileDownloader(Form DownloadingForm, ProgressBar pb)
    {
        Downloading_Form = DownloadingForm;
        Progress_Bar = pb;
    }

    private void Download(object startPoint)
    {
        try
        {
            int num3;
            int range = Convert.ToInt32(startPoint);
            webRequest = (HttpWebRequest) WebRequest.Create(Url);
            webRequest.AddRange(range);
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            webResponse = (HttpWebResponse) webRequest.GetResponse();
            long contentLength = webResponse.ContentLength;
            strResponse = webResponse.GetResponseStream();
            strLocal = (range == 0) ? new FileStream(LocalPath, FileMode.Create, FileAccess.Write, FileShare.None) : new FileStream(LocalPath, FileMode.Append, FileAccess.Write, FileShare.None);
            byte[] buffer = new byte[0x800];
            while ((num3 = strResponse.Read(buffer, 0, buffer.Length)) > 0)
            {
                strLocal.Write(buffer, 0, num3);
                Downloading_Form.Invoke(new UpdateProgessCallback(UpdateProgress), new object[] { strLocal.Length, contentLength + range });
                if (goPause)
                {
                    return;
                }
            }
        }
        finally
        {
            strResponse.Close();
            strLocal.Close();
        }
    }

    public byte[] DownloadData(string url)
    {
        DownloadFile(url, Application.StartupPath + @"\tempdata.temp");
        BinaryReader reader = new BinaryReader(new FileStream(Application.StartupPath + @"\tempdata.temp", FileMode.Open));
        byte[] buffer = reader.ReadBytes((int) reader.BaseStream.Length);
        reader.Close();
        System.IO.File.Delete(Application.StartupPath + @"\tempdata.temp");
        return buffer;
    }

    public void DownloadFile(string url, string Path)
    {
        Url = url;
        LocalPath = Path;
        thrDownload = new Thread(new ParameterizedThreadStart(Download));
        thrDownload.Start(0);
        while (thrDownload.ThreadState != ThreadState.Stopped)
        {
            Application.DoEvents();
        }
    }

    private void UpdateProgress(long BytesRead, long TotalBytes)
    {
        PercentProgress = Convert.ToInt32((long) ((BytesRead * 100L) / TotalBytes));
        Progress_Bar.Maximum = 100;
        Progress_Bar.Minimum = 0;
        Progress_Bar.Value = PercentProgress;
    }

    public Form Downloading_Form
    {
        get
        {
            return _downloadingform;
        }
        set
        {
            _downloadingform = value;
        }
    }

    public ProgressBar Progress_Bar
    {
        get
        {
            return _progressbar;
        }
        set
        {
            _progressbar = value;
        }
    }

    private delegate void UpdateProgessCallback(long BytesRead, long TotalBytes);
}

