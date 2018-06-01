namespace Ascension.Settings.Properties
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;
    using System.Windows.Forms;

    public class UIFileProperty : UITypeEditor
    {
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (((context == null) || (provider == null)) || (context.Instance == null))
            {
                return base.EditValue(provider, value);
            }
            FileDialog dialog = new OpenFileDialog {
                CheckFileExists = true,
                CheckPathExists = true,
                Title = "Select Filename",
                FileName = value as string,
                Filter = "All Files (*.*)|*.*"
            };
            if ((dialog.ShowDialog() == DialogResult.OK) && File.Exists(dialog.FileName))
            {
                value = dialog.FileName;
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return UITypeEditorEditStyle.None;
        }
    }
}

