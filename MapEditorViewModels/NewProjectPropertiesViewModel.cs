using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class NewProjectPropertiesViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly NewProjectProperties properties;
        #endregion

        #region Properties
        public string ProjectName
        {
            get
            {
                return properties.ProjectName;
            }
            set
            {
                properties.ProjectName = value;
            }
        }
        public string MapName
        {
            get
            {
                return properties.MapName;
            }
            set
            {
                properties.MapName = value;
            }
        }
        public MapType MapType
        {
            get
            {
                return properties.MapType;
            }
            set
            {
                properties.MapType = value;
            }
        }
        public int MapHeight
        {
            get
            {
                return properties.MapHeight;
            }
            set
            {
                properties.MapHeight = value;
            }
        }
        public int MapWidth
        {
            get
            {
                return properties.TileWidth;
            }
            set
            {
                properties.TileWidth = value;

                OnPropertyChanged("TileWidth");
            }
        }
        public int TileHeight
        {
            get
            {
                return properties.TileWidth;
            }
            set
            {
                properties.TileWidth = value;

                OnPropertyChanged("TileHeight");
            }
        }
        public int TileWidth
        {
            get
            {
                return properties.TileWidth;
            }
            set
            {
                properties.TileWidth = value;

                OnPropertyChanged("TileWidth");
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public NewProjectPropertiesViewModel(NewProjectProperties properties)
        {
            this.properties = properties;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
