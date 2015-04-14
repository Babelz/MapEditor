using MapEditor.Windows;
using MapEditorCore;
using MapEditorCore.Abstractions;
using MapEditorCore.TileEditor;
using MapEditorViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Point = Microsoft.Xna.Framework.Point;

namespace MapEditor.UserControls
{
    /// <summary>
    /// Interaction logic for LayersView.xaml
    /// </summary>
    public partial class LayersView : UserControl
    {
        #region Fields
        private readonly LayersViewModel layersViewModel;

        private readonly TileEditor tileEditor;

        private readonly Editor editor;

        private readonly Action createNewLayerAction;
        #endregion

        #region Properties
        private LayersViewModel LayersViewModel
        {
            get
            {
                return LayersViewModel;
            }
        }
        #endregion

        /// <summary>
        /// Creates new instance of layers view for showing tile layers.
        /// </summary>
        public LayersView(TileEditor tileEditor)
        {
            this.editor = tileEditor;

            // Get reference of concrete editor type.
            this.tileEditor = tileEditor;

            // Initialize view model.
            layersViewModel = new LayersViewModel(tileEditor);

            // Set data context.
            DataContext = layersViewModel;

            InitializeComponent();

            // Set source for view.
            layersListView.ItemsSource = layersViewModel.Layers;

            // Hook create new layer action.
            createNewLayerAction = new Action(CreateTileLayer);
        }

        #region Create new layer actions 
        private void CreateTileLayer()
        {
            // WARNING: duplication with tile editor GUI configurers addLayerMenuItem_ClickEventHandler!

            NewTileLayerDialog newTileLayerDialog = new NewTileLayerDialog(tileEditor);

            // Show the dialog, ask for layer properties.
            if (newTileLayerDialog.ShowDialog().Value)
            {
                // Dialog OK, create new layer.
                NewTileLayerProperties newTileLayerProperties = newTileLayerDialog.NewTileLayerProperties;

                editor.AddLayer(newTileLayerProperties.Name, new Point(newTileLayerProperties.Width, newTileLayerProperties.Height));
            }
        }
        #endregion

        private bool ReorderLayers()
        {
            // Get selected layer.
            LayerViewModel layerViewModel = GetSelectedLayer();

            // Return if no layer.
            if (layerViewModel == null) return false;

            // Get current draw order value.
            int current = layerViewModel.DrawOrder;

            // Check if there is a layer with this draw order.
            LayerViewModel otherLayerViewModel = layersViewModel.Layers.FirstOrDefault(l => l.DrawOrder == current + 1);

            // If there is a layer with this draw order, swap their draw orders.
            if (otherLayerViewModel != null)
            {
                SwapDrawOrders(layerViewModel, otherLayerViewModel);

                return true;
            }

            return false;
        }
        private LayerViewModel GetSelectedLayer()
        {
            return layersViewModel.Layers.ElementAt(layersListView.SelectedIndex);
        }
        private void SwapDrawOrders(LayerViewModel a, LayerViewModel b)
        {
            int aO = a.DrawOrder;
            int bO = b.DrawOrder;

            a.DrawOrder = bO;
            b.DrawOrder = aO;
        }

        #region Event handlers
        private void newLayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Show new layer dialog to the user.
            createNewLayerAction();
        }
        private void deleteLayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete selected layer.

            LayerViewModel layerViewModel = GetSelectedLayer();

            if (layerViewModel == null) return;

            // TODO: action binding.

            editor.RemoveLayer(layerViewModel.Name);
        }
        private void moveLayerUpButton_Click(object sender, RoutedEventArgs e)
        {
            // No other layers, return.
            if (layersViewModel.Layers.Count <= 1) return;

            LayerViewModel layerViewModel = GetSelectedLayer();

            if (layerViewModel == null) return;

            // Draw order was changed by basic method, return.
            if (ReorderLayers()) return;

            // Get current max draw order.
            int max = editor.Layers.Max(l => l.DrawOrder.Value);

            // Trying to rise topmost layer, just return.
            if (layerViewModel.DrawOrder > max + 1) return;
             
            // Rise the layer.
            layerViewModel.DrawOrder += 1;

            layersViewModel.NotifyDrawOrderChanged();
        }
        private void moveLayerDown_Click(object sender, RoutedEventArgs e)
        {
            // No other layers, return.
            if (layersViewModel.Layers.Count <= 1) return;

            LayerViewModel layerViewModel = GetSelectedLayer();

            if (layerViewModel == null) return;

            // Draw order was changed by basic method, return.
            if (ReorderLayers()) return;

            // Get current min draw order.
            int min = editor.Layers.Min(l => l.DrawOrder.Value);

            if (layerViewModel.DrawOrder - 1 < 0) return;

            layerViewModel.DrawOrder -= 1;

            layersViewModel.NotifyDrawOrderChanged();
        }
        private void layersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected layer view model.
            LayerViewModel LayerViewModel = null;

            foreach (object o in e.AddedItems)
            {
                LayerViewModel = o as LayerViewModel;
            }

            // Notify tile editor that new layer has been selected.
            editor.SelectLayer(LayerViewModel.Name);
        }
        #endregion
    }
}
