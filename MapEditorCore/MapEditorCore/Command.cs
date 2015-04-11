using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    /// <summary>
    /// Common interface for all editor related commands.
    /// </summary>
    public abstract class Command
    {
        #region Fields
        private readonly string description;
        #endregion
        public Command(string description)
        {
            this.description = description;
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        public abstract void Execute();
        
        /// <summary>
        /// Undoes the command.
        /// </summary>
        public abstract void Undo();

        /// <summary>
        /// Returns commands description string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return description;
        }

        public override int GetHashCode()
        {
            return description.GetHashCode();
        }
    }
}
