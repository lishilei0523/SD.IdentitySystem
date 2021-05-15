using System.Windows;
using System.Windows.Controls;

namespace SD.Infrastructure.WPF.CustomControls
{
    /// <summary>
    /// TreeListViewItem
    /// </summary>
    public class TreeListViewItem : TreeViewItem
    {
        private int _level = -1;
        public int Level
        {
            get
            {
                if (this._level == -1)
                {
                    TreeListViewItem parent = ItemsControl.ItemsControlFromItemContainer(this) as TreeListViewItem;
                    this._level = parent?.Level + 1 ?? 0;
                }
                return this._level;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new TreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is TreeListViewItem;
        }
    }
}
