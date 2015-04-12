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
        #region Static fields
        private static int idCounter;
        #endregion

        #region Fields
        private readonly string description;
        private readonly int id;
        #endregion

        #region Properties
        public string Description
        {
            get
            {
                return description;
            }
        }
        public int ID
        {
            get
            {
                return id;
            }
        }
        #endregion

        public Command(string description)
        {
            this.description = description;

            id = idCounter;
            idCounter++;
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        public abstract void Execute();
        
        /// <summary>
        /// Undoes the command.
        /// </summary>
        public abstract void Undo();
    }
}
