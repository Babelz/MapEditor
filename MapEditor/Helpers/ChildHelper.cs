using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace MapEditor.Helpers
{
    /// <summary>
    /// Static helper class. Contains static methods.
    /// </summary>
    public static class ChildHelper
    {
        /// <summary>
        /// Find and returns all children of given parent.
        /// </summary>
        public static IEnumerable<DependencyObject> GetChildren(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++) yield return VisualTreeHelper.GetChild(parent, i);
        }
        /// <summary>
        /// Looks for child with given name from given parent. Returns 
        /// null if child with given name was not found.
        /// </summary>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            Type type = typeof(T);

            List<DependencyObject> childs = GetChildren(parent)
                .Where(c => c.GetType() == type)
                .ToList();

            foreach (DependencyObject child in childs)
            {
                FrameworkElement element = child as FrameworkElement;

                if (element.Name == childName) return child as T;
            }

            return null;
        }

        public static MenuItem FindMenuItem(Menu parent, string childName)
        {
            for (int i = 0; i < parent.Items.Count; i++)
            {
                MenuItem child = parent.Items.GetItemAt(i) as MenuItem;

                if (child != null && child.Name == childName) return child;
            }

            return null;
        }
    }
}
