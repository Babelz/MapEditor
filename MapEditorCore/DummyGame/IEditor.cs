using MapEditorCore.Abstractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    public interface IEditor
    {
        #region Properties
        IEnumerable<Layer> Layers
        {
            get;
        }
        Color BackgroundColor
        {
            get;
            set;
        }
        #endregion

        void Initialize();
        void LoadContent();
        void UnloadContent();

        void MakeLayerActive(string name);
        void AddLayer(string name, Point size);
        void RemoveLayer(string name);

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
