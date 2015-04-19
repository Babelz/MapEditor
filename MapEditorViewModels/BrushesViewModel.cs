using MapEditorCore.TileEditor;
using MapEditorCore.TileEditor.Painting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class BrushesViewModel : INotifyPropertyChanged
    {
        #region Fields
        private IEnumerable<TileBrush> brushes;

        private TileBrush selected;
        #endregion

        #region Properties
        public IEnumerable<TileBrush> Brushes
        {
            get
            {
                return brushes;
            }
        }
        public TileBrush Selected
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

        public BrushesViewModel(IEnumerable<TileBrush> brushes)
        {
            this.brushes = brushes;
        }
    }
}
