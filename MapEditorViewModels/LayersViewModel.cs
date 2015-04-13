using MapEditorCore.Abstractions;
using MapEditorCore.TileEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MapEditorViewModels
{
    /// <summary>
    /// View model for wrapping layers.
    /// </summary>
    public sealed class LayersViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly ObservableCollection<LayerViewModel> layerViewModels;

        private readonly ILayerManager layers;
        #endregion

        #region Properties
        public ObservableCollection<LayerViewModel> Layers
        {
            get
            {
                return layerViewModels;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public LayersViewModel(ILayerManager layers)
        {
            this.layers = layers;
          
            layers.LayerAdded += layers_LayerAdded;
            layers.LayerRemoved += layers_LayerRemoved;

            // Generate view models.
            layerViewModels = new ObservableCollection<LayerViewModel>();

            foreach (Layer layer in layers.Layers)
            {
                layerViewModels.Add(CreateViewModelFrom(layer));
            }
        }

        private LayerViewModel CreateViewModelFrom(Layer layer)
        {
            return new LayerViewModel(layer, layers.Layers.Select(l => l.Name));
        }

        #region Event handlers
        private void layers_LayerAdded(object sender, LayerManagerEventArgs e)
        {
            layerViewModels.Add(CreateViewModelFrom(e.Layer));

            OnPropertyChanged("Layers");
        }
        private void layers_LayerRemoved(object sender, LayerManagerEventArgs e)
        {
            LayerViewModel layerViewModel = layerViewModels.FirstOrDefault(l => l.WrapsLayer(e.Layer));

            layerViewModels.Remove(layerViewModel);

            OnPropertyChanged("Layers");
        }
        #endregion

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
