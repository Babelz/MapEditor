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
        private readonly LayersViewModel layerViewModel;

        private readonly ILayerManager layers;
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

        public LayersView(ILayerManager layers)
        {
            this.layers = layers;

            // Set data context.
            DataContext = layerViewModel;

            InitializeComponent();

            // Set source for view.
            layersListView.ItemsSource = layerViewModel.Layers;
        }

        private void newLayerButton_Click(object sender, RoutedEventArgs e)
        {
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
