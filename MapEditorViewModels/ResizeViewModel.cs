using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class ResizeViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly IEnumerable<string> layers;

        private readonly ResizeModel model;
        #endregion

        #region Properties
        public IEnumerable<string> Layers
        {
            get
            {
                return layers;
            }
        }
        public string SelectedLayer
        {
            get
            {
                return model.SelectedLayer;
            }
            set
            {
                model.SelectedLayer = value;

                OnPropertyChanged("SelectedLayer");
                OnPropertyChanged("HasValidProperties");
            }
        }
        public int Width
        {
            get
            {
                return model.NewWidth;
            }
            set
            {
                model.NewWidth = value;

                OnPropertyChanged("WidthInBounds");
                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("InBounds");
                OnPropertyChanged("Width");
            }
        }
        public int Height
        {
            get
            {
                return model.NewHeight;
            }
            set
            {
                model.NewHeight = value;


                OnPropertyChanged("HeightInBounds");
                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("InBounds");
                OnPropertyChanged("Height");
            }
        }
        public bool WidthInBounds
        {
            get
            {
                return !model.WidthInBounds();
            }
        }
        public bool HeightInBounds
        {
            get
            {
                return !model.HeightInBounds();
            }
        }
        public bool HasValidProperties
        {
            get
            {
                return model.HasValidProperties();
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public ResizeViewModel(ResizeModel model, IEnumerable<string> layers)
        {
            this.model = model;
            this.layers = layers;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
