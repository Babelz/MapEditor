using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore
{
    public sealed class EditorGame : Game
    {
        #region Fields
        private readonly GraphicsDeviceManager graphics;
        
        private readonly IEditor editor;
        private readonly object window;

        private SpriteBatch spriteBatch;
        #endregion

        public EditorGame(object window, IEditor editor)
        {
            graphics = new GraphicsDeviceManager(this);

            this.editor = editor;
        }

        protected override void Initialize()
        {
            base.Initialize();

            editor.Initialize();
            editor.InitializeGUI(window);
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            editor.LoadContent();
        }
        protected override void UnloadContent()
        {
            base.UnloadContent();

            editor.UnloadContent();
            editor.DeinitializeGUI(window);
        }

        protected override void Update(GameTime gameTime)
        {
            editor.Update(gameTime);
            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(editor.BackgroundColor);

            editor.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
