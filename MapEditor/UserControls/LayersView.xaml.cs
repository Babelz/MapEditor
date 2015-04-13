using MapEditor.Windows;
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

        public LayersView(TileEditor tileEditor)
        {
            this.tileEditor = tileEditor;

            // Initialize view model.
            layersViewModel = new LayersViewModel(tileEditor);

            // Set data context.
            DataContext = layersViewModel;

            InitializeComponent();

            // Set source for view.
            layersListView.ItemsSource = layersViewModel.Layers;
        }

        private void newLayerButton_Click(object sender, RoutedEventArgs e)
        {
            // WARNING: duplication with tile editor GUI configurers addLayerMenuItem_ClickEventHandler!

            NewTileLayerDialog newTileLayerDialog = new NewTileLayerDialog(tileEditor);

            // Show the dialog, ask for layer properties.
            if (newTileLayerDialog.ShowDialog().Value)
            {
                // Dialog OK, create new layer.
                NewTileLayerProperties newTileLayerProperties = newTileLayerDialog.NewTileLayerProperties;

                tileEditor.AddLayer(newTileLayerDialog.Name, new Point(newTileLayerProperties.Width, newTileLayerProperties.Height));
            }
        }
        private void deleteLayerButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void moveLayerUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void moveLayerDown_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
