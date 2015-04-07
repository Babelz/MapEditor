using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    /// <summary>
    /// New project properties view model. 
    /// </summary>
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

                OnPropertyChanged("HasValidProperties");
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

                OnPropertyChanged("HasValidProperties");
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

                OnPropertyChanged("HasValidProperties");
                OnPropertyChanged("RequiresTileProperties");
            }
        }
        /// <summary>
        /// Height of the map. Will get converted to int.
        /// </summary>
        public string MapHeight
        {
            get
            {
                return properties.MapHeight.ToString();
            }
            set
            {
                int.TryParse(value, out properties.MapHeight);

                OnPropertyChanged("HasValidProperties");
            }
        }
        /// <summary>
        /// Width of the map. Will get converted to int.
        /// </summary>
        public string MapWidth
        {
            get
            {
                return properties.MapWidth.ToString();
            }
            set
            {
                int.TryParse(value, out properties.MapWidth);

                OnPropertyChanged("HasValidProperties");
            }
        }
        /// <summary>
        /// Tile height. Required for tile based maps. Will get converted to int.
        /// </summary>
        public string TileHeight
        {
            get
            {
                return properties.TileHeight.ToString();
            }
            set
            {
                int.TryParse(value, out properties.TileHeight);
                
                OnPropertyChanged("HasValidProperties");
            }
        }
        /// <summary>
        /// Tile width. Required for tile based maps. Will get converted to int.
        /// </summary>
        public string TileWidth
        {
            get
            {
                return properties.TileWidth.ToString();
            }
            set
            {
                int.TryParse(value, out properties.TileWidth);

                OnPropertyChanged("HasValidProperties");
            }
        }
        /// <summary>
        /// Returns bool whether given properties meet the requirements for this type map.
        /// </summary>
        public bool HasValidProperties
        {
            get
            {
                switch (MapType)
                {
                    case MapType.Tile:
                    case MapType.Hex:
                        return properties.HasValidTileMapProperties();
                    case MapType.Object:
                        return properties.HasValidMapProperties();
                    default:
                        throw new InvalidOperationException("Unsupported map type.");
                }
            }
        }
        /// <summary>
        /// Returns bool whether this map is tile based and still requires tile properties.
        /// </summary>
        public bool RequiresTileProperties
        {
            get
            {
                return MapType != MapType.Object;
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
