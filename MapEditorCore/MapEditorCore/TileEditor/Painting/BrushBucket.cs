using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    /// <summary>
    /// Class that contains all brushes for each tileset.
    /// </summary>
    public sealed class BrushBucket
    {
        #region Fields
        private readonly List<TileBrush> brushes;
        private readonly Tileset owner;

        private TileBrush selectedBrush;
        #endregion

        #region Properties
        public bool HasBrushSelected
        {
            get
            {
                return selectedBrush != null;
            }
        }
        public IEnumerable<TileBrush> Brushes
        {
            get
            {
                return brushes;
            }
        }
        /// <summary>
        /// Returns selected brush.
        /// </summary>
        public TileBrush SelectedBrush
        {
            get
            {
                return selectedBrush;
            }
        }
        #endregion

        public BrushBucket(Tileset owner)
        {
            this.owner = owner;
            
            // TODO: add all brushes by hand.
            brushes = new List<TileBrush>()
            {
                new SingleTileBrush(owner),
                new SingleIndexMultipleTilesBrush(2, 2, BrushResizeMode.NoResize, "2x2", owner),
                new SingleIndexMultipleTilesBrush(4, 4, BrushResizeMode.NoResize, "4x4", owner)
            };

            // Select first brush in the list.
            selectedBrush = brushes[1];
        }

        public void SelectBrush(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                selectedBrush = null;

                return;
            }

            selectedBrush = brushes.FirstOrDefault(b => b.Name == name);
        }
    }
}
