using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditorInput.Buffer;
using MapEditorInput.State;

namespace MapEditorInput.Listener
{
    public class MouseInputListener : InputListener
    {
        #region Vars

        private readonly MouseInputBuffer mouseBuffer;
        private KeyTimer timer = new KeyTimer();
        #endregion

        #region Properties

        public MouseStateProvider State
        {
            get;
            private set;
        }

        #endregion
        public MouseInputListener()
        {
            State = new MouseStateProvider();
            mouseBuffer = new MouseInputBuffer(this);
            InputBuffer = mouseBuffer;
        }

        private void TriggerEvents(Dictionary<string, Mapping> maps, InputState inputState)
        {
            foreach (var mapping in maps)
            {
                InputEventArgs args = new InputEventArgs(inputState, timer.GetHoldTime(mapping.Key), this);
                mapping.Value.Callback.Invoke(args);
            }
        }
        

        public override void Update(GameTime gameTime)
        {
            State.Update();
            InputBuffer.Update();
            timer.Update(InputBuffer, gameTime);
            TriggerEvents(InputBuffer.ReleasedMappings, InputState.Released);
            TriggerEvents(InputBuffer.PressedMappings, InputState.Pressed);
            TriggerEvents(InputBuffer.DownMappings, InputState.Down);
        }
    }
}
