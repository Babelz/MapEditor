using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore.Input.State
{
    public interface IStateProvider
    {
        /// <summary>
        /// Päivittää statea
        /// </summary>
        void Update();
    }
}
