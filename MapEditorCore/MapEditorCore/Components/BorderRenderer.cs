using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Components
{
    public sealed class BorderRenderer
    {
        #region Constants
        private const int BORDER_THICKNESS = 5;
        #endregion

        #region Fields
        private readonly Texture2D temp;

        private readonly Editor editor;
        #endregion

        public BorderRenderer(Editor editor, Texture2D temp)
        {
            this.editor = editor;
            this.temp = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle bounds = editor.GetMapBounds();

            // Top.
            spriteBatch.Draw(temp, new Rectangle(0, 0, bounds.Width, BORDER_THICKNESS), Color.Black);

            // Left.
            spriteBatch.Draw(temp, new Rectangle(bounds.Width, 0, BORDER_THICKNESS, bounds.Height), Color.Black);
            
            // Bottom.
            spriteBatch.Draw(temp, new Rectangle(0, bounds.Bottom, bounds.Width, BORDER_THICKNESS), Color.Black);

            // Right.
            spriteBatch.Draw(temp, new Rectangle(0, 0, BORDER_THICKNESS, bounds.Height), Color.Black);
        }
    }
}
