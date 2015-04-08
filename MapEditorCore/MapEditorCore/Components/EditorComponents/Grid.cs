using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Components.EditorComponents
{
    public sealed class Grid : EditorComponent
    {
        #region Constants
        private const int LINE_THICKNESS = 5;
        #endregion

        #region Fields
        private readonly Texture2D texture;
        private readonly Point cellSize;

        private Color color;
        #endregion

        public Grid(Editor editor, Texture2D texture, Point cellSize)
            : base(editor)
        {
            this.texture = texture;
            this.cellSize = cellSize;

            color = Color.Black;
        }

        protected override void OnDraw(SpriteBatch spriteBatch, Rectangle viewBounds)
        {
            Rectangle bounds = Editor.GetMapBounds();

            // Draw grid lines from (0, 0) to n where n is map bottom right corner.
            // Drawing some rectangles across the map that overlap the view
            // should not be an performance issue. 
            // WARNING: grid lines overlap the view, performance?
            // WARNING: grid draws all lines.
            // TODO: fix grids.

            int verticalLines = bounds.Width / cellSize.X;
            int horizontalLines = bounds.Height / cellSize.Y;

            // Draw vertical lines.
            for (int i = 0; i < verticalLines; i++) spriteBatch.Draw(texture, new Rectangle(i * cellSize.X, 0, LINE_THICKNESS, bounds.Height), color);
                
            // Draw horizontal lines.
            for (int i = 0; i < horizontalLines; i++) spriteBatch.Draw(texture, new Rectangle(0, i * cellSize.Y, bounds.Width, LINE_THICKNESS), color);
        }
    }
}
