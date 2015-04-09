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

        public string Name;

        public int Width;
        public int Height;
        #endregion

        public NewTileLayerProperties(string[] takenNames)
        {
            this.takenNames = takenNames;
        }

        public bool HasValidProperties()
        {
            return !string.IsNullOrEmpty(Name) && Width != 0 && Height != 0 && HasUniqueName();
        }
        public bool HasUniqueName()
        {
            for (int i = 0; i < takenNames.Length; i++) if (takenNames[i] == Name) return true;

            return false;
        }
    }
}
