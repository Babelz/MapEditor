using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XNAControl;

namespace MapEditorCore
{
    /// <summary>
    /// Adapter class for editors. No editor should have reference to an instance
    /// of this class.
    /// </summary>
    public sealed class EditorGame : XNAControlGame
    {
        #region Fields
        private readonly SpriteBatch spriteBatch;

        private readonly Editor editor;
        #endregion

        public EditorGame(IntPtr windowHandle, Editor editor)
            : base(windowHandle, "Content")
        {
            this.editor = editor;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Just initialize the editor, XNA control has already called
            // load content, initialize etc...
            editor.Initialize(Content, spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            editor.Update(gameTime);

            // Set view size.
            editor.View.SetArea(ResolutionWidth, ResolutionHeight);
            
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
