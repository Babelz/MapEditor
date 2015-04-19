using MapEditorCore.TileEditor.Painting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    /// <summary>
    /// Class for containing and managing brushes.
    /// </summary>
    public sealed class BrushManager
    {
        #region Fields
        private readonly List<TileBrush> brushes;

        private TileBrush selectedBrush;
        #endregion

        #region Properties
        public TileBrush SelectedBrush
        {
            get
            {
                return selectedBrush;
            }
        }
        public IEnumerable<TileBrush> Brushes
        {
            get
            {
                return brushes;
            }
        }
        #endregion

        public BrushManager()
        {
            brushes = new List<TileBrush>();

            // TODO: push new brushes.
            brushes.Add(new SingleTileBrush());
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
