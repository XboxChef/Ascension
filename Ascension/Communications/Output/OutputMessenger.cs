namespace Ascension.Communications.Output
{
    using Ascension.Forms;
    using System;
    using System.Windows.Forms;

    public abstract class OutputMessenger
    {
        protected OutputMessenger()
        {
        }

        private static void OutputMessage(string Message, MapForm form)
        {
            DateTime time = form.Initialized_Time;
            TimeSpan span = DateTime.Now.Subtract(time);
            string str = span.Hours.ToString();
            string str2 = span.Minutes.ToString();
            string str3 = span.Seconds.ToString();
            while (str.Length != 2)
            {
                str = "0" + str;
            }
            while (str2.Length != 2)
            {
                str2 = "0" + str2;
            }
            while (str3.Length != 2)
            {
                str3 = "0" + str3;
            }
            bool flag = form.rtbOutputBox.SelectionStart >= (form.rtbOutputBox.Text.Length - 1);
            string[] strArray = Message.Split(new char[] { '\n' });
            foreach (string str4 in strArray)
            {
                if (str4.Replace(" ", "") != "")
                {
                    string text = form.rtbOutputBox.Text;
                    form.rtbOutputBox.Text = text + "[" + str + ":" + str2 + ":" + str3 + "] " + str4 + "\n";
                }
            }
            if (flag)
            {
                form.rtbOutputBox.SelectionStart = form.rtbOutputBox.Text.Length - 1;
                form.rtbOutputBox.ScrollToCaret();
            }
        }

        public static void OutputMessage(string Message, Control reporter)
        {
            try
            {
                Control parent = reporter;
                while (parent.GetType() != typeof(MapForm))
                {
                    parent = parent.Parent;
                }
                OutputMessage(Message, (MapForm) parent);
            }
            catch
            {
            }
        }
    }
}

