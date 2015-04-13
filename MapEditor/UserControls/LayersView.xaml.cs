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

namespace MapEditor.UserControls
{
    /// <summary>
    /// Interaction logic for LayersView.xaml
    /// </summary>
    public partial class LayersView : UserControl
    {
        #region Fields
        private readonly LayersViewModel<TileLayer> layerViewModel;

        private readonly LayerManager<TileLayer> layers;
        #endregion

        #region Properties
        private LayersViewModel<TileLayer> LayersViewModel
        {
            get
            {
                return LayersViewModel;
            }
        }
        #endregion

        public LayersView()
        {
            // TODO: test data.
            
            layers = new LayerManager<TileLayer>();

            for (int i = 0; i < 10; i++) layers.AddLayer(new TileLayer("layer " + i.ToString(),
                new Microsoft.Xna.Framework.Point(32, 32),
                new TileEngine(new Microsoft.Xna.Framework.Point(32, 32),
                new Microsoft.Xna.Framework.Point(32, 32))));

            layerViewModel = new LayersViewModel<TileLayer>(layers);

            DataContext = layerViewModel;

            InitializeComponent();

            layersListView.ItemsSource = layerViewModel.Layers;

            Debug.Assert(layersListView.DataContext != null);
            Debug.Assert(layersListView.ItemsSource != null);
        }

        private void newLayerButton_Click(object sender, RoutedEventArgs e)
        {
            layers.AddLayer(new TileLayer("layeASasdasd",
                new Microsoft.Xna.Framework.Point(32, 32),
                new TileEngine(new Microsoft.Xna.Framework.Point(32, 32),
                new Microsoft.Xna.Framework.Point(32, 32))));
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
