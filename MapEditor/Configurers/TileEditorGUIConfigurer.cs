using MapEditor.Helpers;
using MapEditor.Windows;
using MapEditorCore.TileEditor;
using MapEditorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using Point = Microsoft.Xna.Framework.Point;

namespace MapEditor.Configurers
{
    public sealed class TileEditorGUIConfigurer : IGUIConfigurer
    {
        /*
         * Some of the windows are used like dialogs, so no need to instantiate them at the
         * class scope level.
         */

        #region Fields
        private readonly TileEditor tileEditor;
        // Root menu item.
        private MenuItem editMenuItem;
        private MenuItem addMenuItem;

        private MenuItem editMapMenuItem;
        private MenuItem editMetaObjectsMenuItem;

        private MenuItem addLayerMenuItem;
        private MenuItem addTileSheetMenuItem;
        private MenuItem addMetadataObjectSetMenuItem;
        #endregion

        /// <summary>
        /// Creates new Tile editor GUI configurer. 
        /// </summary>
        /// <param name="tileEditor">tile editor instance that windows and user controls will need</param>
        public TileEditorGUIConfigurer(TileEditor tileEditor)
        {
            this.tileEditor = tileEditor;
        }

        #region Event handlers
        private void addLayerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewTileLayerDialog newTileLayerDialog = new NewTileLayerDialog(tileEditor);

            // Show the dialog, ask for layer properties.
            if (newTileLayerDialog.ShowDialog().Value)
            {
                // Dialog OK, create new layer.
                NewTileLayerProperties newTileLayerProperties = newTileLayerDialog.NewTileLayerProperties;

                tileEditor.AddLayer(newTileLayerProperties.Name, new Point(newTileLayerProperties.Width, newTileLayerProperties.Height));
            }
        }
        #endregion

        private void InsertUserControls(Window window)
        {
        }
        private void InsertMenuItems(Window window)
        {
            // Edit menu items.
            editMapMenuItem = new MenuItem()
            {
                Header = "Map"
            };

            editMetaObjectsMenuItem = new MenuItem()
            {
                Header = "Metadata objects"
            };

            // Add menu items.
            addLayerMenuItem = new MenuItem()
            {
                Header = "Layer"
            };

            addLayerMenuItem.Click += addLayerMenuItem_Click;

            addTileSheetMenuItem = new MenuItem()
            {
                Header = "Sheet"
            };

            addMetadataObjectSetMenuItem = new MenuItem()
            {
                Header = "Metadata object set"
            };

            // Get root.
            Grid root = LogicalTreeHelper.FindLogicalNode(window, "root") as Grid;

            // Get menu.
            Menu menu = ChildHelper.FindChild<Menu>(root, "menu");

            // Insert new menu items.
            editMenuItem = ChildHelper.FindMenuItem(menu, "editMenuItem");

            editMenuItem.Items.Add(editMapMenuItem);
            editMenuItem.Items.Add(editMetaObjectsMenuItem);

            // Insert new menu items.
            addMenuItem = ChildHelper.FindMenuItem(menu, "addMenuItem");

            addMenuItem.Items.Add(addLayerMenuItem);
            addMenuItem.Items.Add(addTileSheetMenuItem);
            addMenuItem.Items.Add(addMetadataObjectSetMenuItem);
        }
        private void InitializeToolBar(Window window)
        {

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
            InitializeToolBar(window);
            InsertMenuItems(window);

            ConfigurePropertiesWindow(window);
            ConfigureProjectExplorerWindow(window);
        }
        public void RemoveConfiguration(Window window)
        {
            editMenuItem.Items.Remove(editMapMenuItem);
            editMenuItem.Items.Remove(editMetaObjectsMenuItem);

            addMenuItem.Items.Remove(addLayerMenuItem);
            addMenuItem.Items.Remove(addTileSheetMenuItem);
            addMenuItem.Items.Remove(addMetadataObjectSetMenuItem);
        }
    }
}
