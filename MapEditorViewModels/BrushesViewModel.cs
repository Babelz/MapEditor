using MapEditorCore;
using MapEditorCore.TileEditor;
using MapEditorCore.TileEditor.Painting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class BrushesViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly TilesetsViewModel tilesetsViewModel;
        private readonly TileEditor editor;

        private ObservableCollection<BrushViewModel> brushes;
        private BrushViewModel selected;
        #endregion

        #region Properties
        public ObservableCollection<BrushViewModel> Brushes
        {
            get
            {
                return brushes;
            }
        }
        public BrushViewModel Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;

                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public BrushesViewModel(TileEditor editor, TilesetsViewModel tilesetsViewModel)
        {
            this.editor = editor;
            this.tilesetsViewModel = tilesetsViewModel;

            tilesetsViewModel.PropertyChanged += tilesetsViewModel_PropertyChanged;
        }

        #region Event handlers
        private void tilesetsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
            {
                // WARNING: see TilesetsViewModel.cs - Selected property.
                BrushBucket brushBucket = editor.GetBrushBucketForSelectedTileset();

                if (brushBucket != null)
                {
                    // Generate view models, set selected.
                    brushes = new ObservableCollection<BrushViewModel>(brushBucket.Brushes.Select(b => new BrushViewModel(b)));
                    selected = brushes.FirstOrDefault(b => ReferenceEquals(b, brushBucket.SelectedBrush));
                }
                else
                {
                    // Clear data.
                    brushes = null;
                    selected = null;
                }

                if (PropertyChanged != null)
                {
                    // Notify users.
                    PropertyChanged(this, new PropertyChangedEventArgs("Brushes"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
                }
            }
        }
        #endregion
    }
}
