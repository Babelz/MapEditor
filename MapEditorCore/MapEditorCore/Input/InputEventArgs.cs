using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapEditorCore.Input.Listener;

namespace MapEditorCore.Input
{
    public class InputEventArgs
    {
        public InputEventArgs(InputState state, int holdTime, InputListener sender)
        {
            InputState = state;
            HoldTime = holdTime;
            Listener = sender;
        }
        public int HoldTime { get; private set; }
        public InputState InputState { get; private set;  }
        public InputListener Listener { get; private set; }
    }
}
