using MapEditor.Helpers;
using MapEditor.UserControls;
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
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;
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
        private MenuItem viewMenuItem;
        private MenuItem windowsMenuItem;

        private MenuItem editMapMenuItem;
        private MenuItem editMetaObjectsMenuItem;

        private MenuItem addLayerMenuItem;
        private MenuItem addTilesetMenuItem;
        private MenuItem addTileAnimationMenuItem;
        private MenuItem addMetadataObjectSetMenuItem;

        private MenuItem viewLayersMenuItem;

        // Tool windows.
        private LayersView layersView;
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
        private void addTilesetMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewTilesetDialog newTilesetDialog = new NewTilesetDialog(tileEditor);

            // Show the dialog, ask for tileset properties.
            if (newTilesetDialog.ShowDialog().Value)
            {
                // Dialog OK, create new tileset.
                NewTilesetProperties newTilesetProperties = newTilesetDialog.NewTilesetProperties;

                tileEditor.AddTileset(newTilesetProperties.Name, newTilesetProperties.Path, new Point(newTilesetProperties.TileWidth, newTilesetProperties.TileHeight),
                                                                                            new Point(newTilesetProperties.OffsetX, newTilesetProperties.OffsetY));
            }
        }
        #endregion

        private void InsertUserControls(Window window)
        {
            layersView = new LayersView(tileEditor);
            
            // TODO: anchor to properties view.
            Grid root = LogicalTreeHelper.FindLogicalNode(window, "root") as Grid;

            DockingManager dockingManager = LogicalTreeHelper.FindLogicalNode(root, "dockingManager") as DockingManager;

            LayoutAnchorable layoutAnchorable = dockingManager.FindName("propertiesView") as LayoutAnchorable;

            StackPanel stackPanel = layoutAnchorable.Content as StackPanel;

            stackPanel.Children.Add(layersView);
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

            addTilesetMenuItem = new MenuItem()
            {
                Header = "Tileset"
            };

            addTilesetMenuItem.Click += addTilesetMenuItem_Click;

            addTileAnimationMenuItem = new MenuItem()
            {
                Header = "Tile animation"
            };

            addMetadataObjectSetMenuItem = new MenuItem()
            {
                Header = "Metadata object set"
            };

            viewLayersMenuItem = new MenuItem()
            {
                Header = "Layers"
            };

            viewLayersMenuItem.Click += viewLayersMenuItem_Click;
            
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
            addMenuItem.Items.Add(addTilesetMenuItem);
            addMenuItem.Items.Add(addTileAnimationMenuItem);
            addMenuItem.Items.Add(addMetadataObjectSetMenuItem);

            // Insert new menu items.
            viewMenuItem = ChildHelper.FindMenuItem(menu, "viewMenuItem");
            windowsMenuItem = ChildHelper.FindMenuItem(viewMenuItem.Items, "windowsMenuItem");

            windowsMenuItem.Items.Add(viewLayersMenuItem);
        }

        private void viewLayersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            layersView.Visibility = layersView.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
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
            addMenuItem.Items.Remove(addTilesetMenuItem);
            addMenuItem.Items.Remove(addTileAnimationMenuItem);
            addMenuItem.Items.Remove(addMetadataObjectSetMenuItem);
        }
    }
}
