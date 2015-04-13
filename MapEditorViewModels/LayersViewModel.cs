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
    public sealed class LayersViewModel<T> : INotifyPropertyChanged where T : Layer
    {
        #region Fields
        private readonly ObservableCollection<LayerViewModel<T>> layerViewModels;

        private readonly LayerManager<T> layers;
        #endregion

        #region Properties
        public ObservableCollection<LayerViewModel<T>> Layers
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

        public LayersViewModel(LayerManager<T> layers)
        {
            this.layers = layers;
            
            layers.LayerAdded += layers_LayerAdded;
            layers.LayerRemoved += layers_LayerRemoved;

            // Generate view models.
            layerViewModels = new ObservableCollection<LayerViewModel<T>>();

            foreach (T layer in layers.Layers)
            {
                layerViewModels.Add(CreateViewModelFrom(layer));
            }
        }

        private LayerViewModel<T> CreateViewModelFrom(T layer)
        {
            return new LayerViewModel<T>(layer, layers.Layers.Select(l => l.Name));
        }

        #region Event handlers
        private void layers_LayerAdded(T layer)
        {
            layerViewModels.Add(CreateViewModelFrom(layer));

            OnPropertyChanged("Layers");
        }
        private void layers_LayerRemoved(T layer)
        {
            LayerViewModel<T> layerViewModel = layerViewModels.FirstOrDefault(l => l.WrapsLayer(layer));

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
