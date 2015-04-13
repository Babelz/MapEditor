using MapEditorCore.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class LayerViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly IEnumerable<string> takenNames;

        private readonly Layer layer;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return layer.Name;
            }
            set
            {
                if (!takenNames.Contains(value))
                {
                    layer.Name = value;

                    OnPropertyChanged("Name");
                }
            }
        }
        public bool Visible
        {
            get
            {
                return layer.Visible;
            }
            set
            {
                layer.Visible = value;

                OnPropertyChanged("Visible");
            }
        }
        public bool IsDynamic
        {
            get
            {
                return layer.Type == LayerType.Dynamic;
            }
            set
            {
                if (value) layer.Type = LayerType.Dynamic;
                else       layer.Type = LayerType.Static;

                OnPropertyChanged("Dynamic");
            }
        }
        public int DrawOrder
        {
            get
            {
                return layer.DrawOrder.Value;
            }
            set
            {
                layer.DrawOrder.Value = value;

                OnPropertyChanged("DrawOrder");
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public LayerViewModel(Layer layer, IEnumerable<string> takenNames)
        {
            this.layer = layer;

            this.takenNames = takenNames;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public bool WrapsLayer(Layer layer)
        {
            return ReferenceEquals(layer, this.layer);
        }
    }
}
