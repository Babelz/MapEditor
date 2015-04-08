using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Components.LayerComponents
{
    public sealed class Grid : EditorComponent
    {
        #region Fields
        private readonly Texture2D temp;

        private Color color;
        #endregion

        public Grid(Editor editor, Texture2D texture)
            : base(editor)
        {
            this.temp = temp;

            color = Color.Black;
        }
    }
}
