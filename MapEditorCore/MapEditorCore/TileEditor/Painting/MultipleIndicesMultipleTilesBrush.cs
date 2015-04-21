using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor.Painting
{
    public sealed class MultipleIndicesMultipleTilesBrush : TileBrush
    {
        #region Properties
        public override int Width
        {
            get { throw new NotImplementedException(); }
        }
        public override int Height
        {
            get { throw new NotImplementedException(); }
        }
        public override int DisplayWidth
        {
            get { throw new NotImplementedException(); }
        }
        public override int DisplayHeight
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

        protected override PaintArgs OnPaint()
        {
            throw new NotImplementedException();
        }
    }
}
