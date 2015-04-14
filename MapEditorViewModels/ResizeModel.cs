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

        public bool InBounds()
        {
            return WidthInBounds() && HeightInBounds();
        }
    }
}
