using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class NewTileLayerPropertiesViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly NewTileLayerProperties properties;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return properties.Name;
            }
            set
            {
                properties.Name = value;

                OnPropertyChanged("Name");
                OnPropertyChanged("HasUniqueName");
                OnPropertyChanged("HasValidProperties");
            }
        }
        public string Width
        {
            get
            {
                return properties.Width.ToString();
            }
            set
            {
                int.TryParse(value, out properties.Width);

                OnPropertyChanged("Width");
                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("WidthInBounds");
            }
        }
        public string Height
        {
            get
            {
                return properties.Height.ToString();
            }
            set
            {
                int.TryParse(value, out properties.Height);

                OnPropertyChanged("Height");
                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("HeightInBounds");
            }
        }
        public bool HasValidProperties
        {
            get
            {
                return properties.HasValidProperties();
            }
        }
        public bool HasUniqueName
        {
            get
            {
                return !properties.HasUniqueName();
            }
        }
        public bool WidthInBounds
        {
            get
            {
                return !properties.WidthInBounds();
            }
        }
        public bool HeightInBounds
        {
            get
            {
                return !properties.HeightInBounds();
            }
        }
        public string MaxWidth
        {
            get
            {
                return "Max width: " + properties.maxWidth.ToString();
            }
        }
        public string MaxHeight
        {
            get
            {
                return "Max height: " + properties.maxHeight.ToString();
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public NewTileLayerPropertiesViewModel(NewTileLayerProperties properties)
        {
            this.properties = properties;
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
