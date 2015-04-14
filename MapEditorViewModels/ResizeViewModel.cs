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
        private readonly ResizeModel model;
        #endregion

        #region Properties
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

        public ResizeViewModel(ResizeModel model)
        {
            this.model = model;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
