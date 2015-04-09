using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class NewTileLayerProperties
    {
        #region Fields
        private readonly string[] takenNames;
        
        public readonly int maxWidth;
        public readonly int maxHeight;

        public string Name;

        public int Width;
        public int Height;
        #endregion

        public NewTileLayerProperties(string[] takenNames, int maxWidth, int maxHeight)
        {
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;

            this.takenNames = takenNames;
        }

        public bool HasValidProperties()
        {
            return !string.IsNullOrEmpty(Name) && Width != 0 && Height != 0 && HasUniqueName() &&
                   WidthInBounds() && HeightInBounds();
        }
        public bool HasUniqueName()
        {
            if (takenNames.Length == 0) return true;
            
            for (int i = 0; i < takenNames.Length; i++) if (takenNames[i] == Name) return false;

            return true;
        }

        public bool WidthInBounds()
        {
            return Width <= maxWidth && Width > 0;
        }
        public bool HeightInBounds()
        {
            return Height <= maxHeight && Height > 0;
        }
    }
}
