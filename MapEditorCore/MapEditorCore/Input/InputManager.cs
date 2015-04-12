using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditorCore.Input.Listener;

namespace MapEditorCore.Input
{
    public sealed class InputManager
    {
        #region Vars
        public List<InputListener> Listeners
        {
            get;
            private set;
        }
        #endregion

        #region Ctor
        public InputManager(KeyboardInputListener keylistener, MouseInputListener mil)
        {
            Listeners = new List<InputListener>();

            if (keylistener != null)
                Listeners.Add(keylistener);
            
            if (mil != null)
                Listeners.Add(mil);
        }
        #endregion

        public void Update(GameTime gameTime)
        {
            Listeners.ForEach(l => l.Update(gameTime));
        }
    }
}
