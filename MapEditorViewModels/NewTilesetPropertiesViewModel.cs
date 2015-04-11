using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class NewTilesetPropertiesViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly NewTilesetProperties properties;

        // Just used for validating the offset.
        private int imageWidth;
        private int imageHeight;
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
        public string Path
        {
            get
            {
                return properties.Path;
            }
            set
            {
                properties.Path = value;

                // Reset offset for new image.
                OffsetX = "0";
                OffsetY = "0";

                OnPropertyChanged("Path");
                OnPropertyChanged("HasValidProperties");
            }
        }
        public string OffsetX
        {
            get
            {
                return properties.OffsetX.ToString();
            }
            set
            {
                int.TryParse(value, out properties.OffsetX);
                
                // Keep offset in bounds.
                if (properties.OffsetX >= imageWidth && properties.OffsetX > 0) properties.OffsetX = imageWidth - 1;

                OnPropertyChanged("OffsetX");
                OnPropertyChanged("HasValidProperties");
            }
        }
        public string OffsetY
        {
            get
            {
                return properties.OffsetY.ToString();
            }
            set
            {
                int.TryParse(value, out properties.OffsetY);

                // Keep offset in bounds.
                if (properties.OffsetY >= imageHeight && properties.OffsetY > 0) properties.OffsetY = imageHeight - 1;

                OnPropertyChanged("OffsetY");
                OnPropertyChanged("HasValidProperties");
            }
        }
        public string TileWidth
        {
            get
            {
                return properties.TileWidth.ToString();
            }
            set
            {
                int.TryParse(value, out properties.TileWidth);

                OnPropertyChanged("TileWidth");
                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("HasValidTileWidth");
            }
        }
        public string TileHeight
        {
            get
            {
                return properties.TileHeight.ToString();
            }
            set
            {
                int.TryParse(value, out properties.TileHeight);

                OnPropertyChanged("TileHeight");
                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("HasValidTileHeight");
            }
        }
        public bool HasUniqueName
        {
            get
            {
                return !properties.HasUniqueName();
            }
        }
        public bool HasValidProperties
        {
            get
            {
                return properties.HasValidProperties();
            }
        }
        public bool HasValidTileWidth
        {
            get
            {
                return !(properties.TileWidth > 0);
            }
        }
        public bool HasValidTileHeight
        {
            get
            {
                return !(properties.TileHeight > 0);
            }
        }
        public int ImageWidth
        {
            set
            {
                imageWidth = value;
            }
        }
        public int ImageHeight
        {
            set
            {
                imageHeight = value;
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public NewTilesetPropertiesViewModel(NewTilesetProperties properties)
        {
            this.properties = properties;
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
