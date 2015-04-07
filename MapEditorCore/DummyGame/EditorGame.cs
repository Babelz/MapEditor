using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore
{
    public sealed class EditorGame : XNAControl.XNAControlGame
    {
        #region Fields
        private readonly IEditor editor;
        
        private SpriteBatch spriteBatch;
        #endregion

        public EditorGame(IntPtr windowHandle, IEditor editor)
            : base(windowHandle, "Content")
        {
            this.editor = editor;

            // TODO: hax. XNAControl calls these override methods inside
            // its own constructor. Cant allow that cause editor is still 
            // null at that point...
            Initialize();
            LoadContent();
        }

        /*
         * THESE TWO METHODS!
         * are not overrides from xna game class. Since XNA control
         * is calling these methods at constructor, we need to create 
         * our own initialize and load content methods that we will
         * call in the constructor. 
         * 
         * TODO: maybe get the source and modify it to serve your needs better?
         */

        private new void Initialize()
        {
            editor.Initialize();
        }
        private new void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            editor.LoadContent();
        }

        /*
         * UNLOAD, DRAW AND UPDATE 
         * are overrides from xna game class. 
         */

        protected override void UnloadContent()
        {
            base.UnloadContent();

            editor.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            editor.Update(gameTime);
            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(editor.BackgroundColor);

            editor.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
