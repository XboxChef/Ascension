namespace Ascension.Halo_Reach.Game.Misc.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class TreenodeCompare : IComparer<TreeNode>
    {
        public int Compare(TreeNode x, TreeNode y) => 
            x.Text.CompareTo(y.Text);
    }
}

