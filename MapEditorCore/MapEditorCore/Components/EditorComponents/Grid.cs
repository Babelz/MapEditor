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
            // Draw grid lines from (0, 0) to n where n is map bottom right corner.
            // Drawing some rectangles across the map that overlap the view
            // should not be an performance issue. 
         
            // Get map bounds.
            Rectangle bounds = Editor.GetMapBounds();

            int topPadding = viewBounds.Height / 2;
            int leftPadding = viewBounds.Width / 2;
            
            // Calculate bounds.
            int fromX = viewBounds.Left - leftPadding;
            int toX = viewBounds.Right + leftPadding;

            int fromY = viewBounds.Top - topPadding;
            int toY = viewBounds.Bottom + topPadding;

            // Validate values.
            
            // Validate Y values.
            fromY = fromY < 0 ? 0 : fromY;
            fromY -= fromY % cellSize.Y;

            toY = toY > bounds.Bottom ? bounds.Bottom : toY;
            toY -= toY % cellSize.Y;

            // Validate X values.
            fromX = fromX < 0 ? 0 : fromX;
            fromX -= fromX % cellSize.X;

            toX = toX > bounds.Right ? bounds.Right : toX;
            toX -= toX % cellSize.X;

            // Calculate lines count.
            int verticalLines = (toX - fromX) / cellSize.X;
            int horizontalLines = (toY - fromY) / cellSize.Y;

            // Calculate widths and heights.
            int verticalWidth = LINE_THICKNESS;
            int horizontalWidth = toX - fromX;

            int verticalHeight = toY - fromY;
            int horizontalHeight = LINE_THICKNESS;

            // Draw vertical lines.
            for (int i = 0; i < verticalLines; i++) spriteBatch.Draw(texture, new Rectangle(fromX + cellSize.X * i, fromY, verticalWidth, verticalHeight), Color.Black);

            // Draw horizontal lines.
            for (int i = 0; i < horizontalLines; i++) spriteBatch.Draw(texture, new Rectangle(fromX, fromY + cellSize.Y * i, horizontalWidth, horizontalHeight), Color.Black);
        }
    }
}
