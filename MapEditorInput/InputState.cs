using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorInput
{
    [Flags]
    public enum InputState
    {
        Pressed = 0,
        Down = 1,
        Released = 2,
        Up = 4
    }
}
