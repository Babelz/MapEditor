using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class ResizeModel
    {
        #region Fields
        private readonly int maxWidth;
        private readonly int maxHeight;

        public string SelectedLayer;
        public int NewWidth;
        public int NewHeight;
        #endregion

        public ResizeModel(int maxWidth, int maxHeight)
        {
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
        }

        public bool WidthInBounds()
        {
            return NewWidth > 0 && NewWidth <= maxWidth;
        }

        public bool HeightInBounds()
        {
            return NewHeight > 0 && NewHeight <= maxHeight;
        }

        public bool HasLayerSelected()
        {
            return !string.IsNullOrEmpty(SelectedLayer);
        }

        public bool HasValidProperties()
        {
            return WidthInBounds() && HeightInBounds() && HasLayerSelected();
        }
    }
}
