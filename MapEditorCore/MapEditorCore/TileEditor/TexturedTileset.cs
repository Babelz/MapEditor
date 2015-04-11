using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.TileEditor
{
    public sealed class TexturedTileset : Tileset
    {
        #region Fields
        private Rectangle[][] sources;
        #endregion

        protected override void GenerateSources()
        {
            // Calculate sources.
            int areaWidth = Texture.Width - Offset.X;
            int areaHeight = Texture.Height - Offset.Y;

            int columns = areaWidth / SourceSize.X;
            int rows = areaHeight / SourceSize.Y;

            // Generate source array.
            sources = new Rectangle[rows][];

            for (int i = 0; i < rows; i++) sources[i] = new Rectangle[columns];

            // Generate sources.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Add offset to position.
                    int x = j * SourceSize.X + Offset.X;
                    int y = i * SourceSize.Y + Offset.Y;

                    sources[i][j] = new Rectangle(x, y, SourceSize.X, SourceSize.Y);
                }
            }
        }

        public override Rectangle GetSource(Point sourceIndex)
        {
            return sources[sourceIndex.Y][sourceIndex.X];
        }
    }
}
