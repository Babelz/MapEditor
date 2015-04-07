using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore
{
    /// <summary>
    /// Adapter class for editors. No editor should have reference to an instance
    /// of this class.
    /// </summary>
    public sealed class EditorGame : XNAControl.XNAControlGame
    {
        #region Fields
        private readonly Editor editor;
        
        private SpriteBatch spriteBatch;
        #endregion

        public EditorGame(IntPtr windowHandle, Editor editor)
            : base(windowHandle, "Content")
        {
            this.editor = editor;

            // Just initialize the editor, XNA control has already called
            // load content, initialize etc...
            editor.Initialize(Content, spriteBatch);
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
