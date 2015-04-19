using MapEditorCore.TileEditor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class TilesetsViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly TileEditor editor;

        private readonly ObservableCollection<TilesetViewModel> tilesetViewModels;
        
        private TilesetViewModel selected;
        #endregion

        #region Properties
        public ObservableCollection<TilesetViewModel> Tilesets
        {
            get
            {
                return tilesetViewModels;
            }
        }
        public TilesetViewModel Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;

                OnPropertyChanged("Selected");
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public TilesetsViewModel(TileEditor editor)
        {
            this.editor = editor;

            editor.TilesetAdded += editor_TilesetAdded;
            editor.TilesetRemoved += editor_TilesetRemoved;

            // Generate view models.
            tilesetViewModels = new ObservableCollection<TilesetViewModel>();

            foreach (Tileset tileset in editor.Tilesets)
            {
                tilesetViewModels.Add(CreateTilesetViewModelFrom(tileset));
            }
        }

        #region Event handlers
        private void editor_TilesetRemoved(object sender, TilesetEventArgs e)
        {
            TilesetViewModel tilesetViewModel = tilesetViewModels.FirstOrDefault(t => t.WrapsTileset(e.Tileset));

            tilesetViewModels.Remove(tilesetViewModel);

            OnPropertyChanged("Tilesets");
        }
        private void editor_TilesetAdded(object sender, TilesetEventArgs e)
        {
            tilesetViewModels.Add(CreateTilesetViewModelFrom(e.Tileset));

            OnPropertyChanged("Tilesets");
        }
        #endregion

        private TilesetViewModel CreateTilesetViewModelFrom(Tileset tileset)
        {
            return new TilesetViewModel(tileset);
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
