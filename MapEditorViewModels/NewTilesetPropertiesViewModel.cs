﻿using System;
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
