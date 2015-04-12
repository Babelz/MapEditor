using MapEditorCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    /// <summary>
    /// Command that wraps its execute and undo actions inside delegates.
    /// </summary>
    public sealed class DelegateCommand : Command
    {
        #region Fields
        private readonly Action execute;
        private readonly Action undo;
        #endregion

        public DelegateCommand(string description, Action execute, Action undo)
            : base(description)
        {
            this.execute = execute;
            this.undo = undo;
        }

        public override void Execute()
        {
            execute();
        }

        public override void Undo()
        {
            undo();
        }
    }
}
