using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Components
{
    /// <summary>
    /// Component for rendering borders of maps.
    /// </summary>
    public sealed class BorderRenderer : EditorComponent
    {
        #region Constants
        private const int BORDER_THICKNESS = 5;
        #endregion

        #region Fields
        private readonly Texture2D texture;

        private Color color;
        #endregion

        #region Properties
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        #endregion

        public BorderRenderer(Editor editor, Texture2D texture)
            : base(editor)
        {
            this.texture = texture;

            color = Color.Black;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            Rectangle bounds = Editor.GetMapBounds();

            // Top. Add thickness to width so the corner wont get left empty.
            spriteBatch.Draw(texture, new Rectangle(0, 0, bounds.Width + BORDER_THICKNESS, BORDER_THICKNESS), color);

            // Left.
            spriteBatch.Draw(texture, new Rectangle(bounds.Width, BORDER_THICKNESS, BORDER_THICKNESS, bounds.Height), color);
            
            // Bottom.
            spriteBatch.Draw(texture, new Rectangle(0, bounds.Bottom, bounds.Width, BORDER_THICKNESS), color);

            // Right.
            spriteBatch.Draw(texture, new Rectangle(0, 0, BORDER_THICKNESS, bounds.Height), color);
        }
    }
}
