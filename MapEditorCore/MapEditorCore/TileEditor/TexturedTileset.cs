using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public TexturedTileset(string name, Texture2D texture, Point sourceSize, Point offset)
            : base(name, texture, sourceSize, offset)
        {
        }

        protected override void GenerateSources()
        {
            // Generate source array.
            sources = new Rectangle[IndicesCount.Y][];

            for (int i = 0; i < IndicesCount.Y; i++) sources[i] = new Rectangle[IndicesCount.X];

            // Generate sources.
            for (int i = 0; i < IndicesCount.Y; i++)
            {
                for (int j = 0; j < IndicesCount.X; j++)
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
