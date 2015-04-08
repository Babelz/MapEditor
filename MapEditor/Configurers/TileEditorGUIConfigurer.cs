using MapEditor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MapEditor.Configurers
{
    public sealed class TileEditorGUIConfigurer : IGUIConfigurer
    {
        #region Fields
        // Root menu item.
        private MenuItem editMenuItem;

        // Menu items added.
        private MenuItem editLayersMenuItem;
        private MenuItem editSheetsMenuItem;
        #endregion

        public TileEditorGUIConfigurer()
        {
        }

        private void InsertUserControls(Window window)
        {
        }
        private void InsertMenuItems(Window window)
        {
            editLayersMenuItem = new MenuItem()
            {
                Header = "Layers"
            };

            editSheetsMenuItem = new MenuItem()
            {
                Header = "Sheets"
            };

            // Get root.
            Grid root = LogicalTreeHelper.FindLogicalNode(window, "root") as Grid;

            // Get menu.
            Menu menu = ChildHelper.FindChild<Menu>(root, "menu");

            // Insert new menu items.
            editMenuItem = ChildHelper.FindMenuItem(menu, "editMenuItem");

            editMenuItem.Items.Add(editLayersMenuItem);
            editMenuItem.Items.Add(editSheetsMenuItem);
        }
        private void ConfigurePropertiesWindow(Window window)
        {
        }
        private void ConfigureProjectExplorerWindow(Window window)
        {
        }

        public void Configure(Window window)
        {
            InsertUserControls(window);
            InsertMenuItems(window);

            ConfigurePropertiesWindow(window);
            ConfigureProjectExplorerWindow(window);
        }
        public void RemoveConfiguration(Window window)
        {
            editMenuItem.Items.Remove(editLayersMenuItem);
            editMenuItem.Items.Remove(editSheetsMenuItem);
        }
    }
}
