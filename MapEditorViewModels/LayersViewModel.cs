using MapEditorCore;
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
        private readonly Editor editor;

        private ObservableCollection<LayerViewModel> layerViewModels;
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

        public LayersViewModel(Editor editor)
        {
            this.editor = editor;
          
            editor.LayerAdded += layers_LayerAdded;
            editor.LayerRemoved += layers_LayerRemoved;

            // Generate view models.
            layerViewModels = new ObservableCollection<LayerViewModel>();

            foreach (Layer layer in editor.Layers)
            {
                layerViewModels.Add(CreateViewModelFrom(layer));
            }
        }

        private LayerViewModel CreateViewModelFrom(Layer layer)
        {
            return new LayerViewModel(layer, editor.Layers.Select(l => l.Name));
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

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
