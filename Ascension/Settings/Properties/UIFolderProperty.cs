namespace Ascension.Settings.Properties
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;
    using System.Windows.Forms;

    public class UIFolderProperty : UITypeEditor
    {
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (((context == null) || (provider == null)) || (context.Instance == null))
            {
                return base.EditValue(provider, value);
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                ShowNewFolderButton = false
            };
            if ((dialog.ShowDialog() == DialogResult.OK) && Directory.Exists(dialog.SelectedPath))
            {
                value = dialog.SelectedPath + @"\";
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

