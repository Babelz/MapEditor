using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditorCore
{
    /// <summary>
    /// Singleton class. Handles and contains commands.
    /// </summary>
    public sealed class CommandQueue
    {
        #region Singleton
        private static readonly CommandQueue instance = new CommandQueue(128);
        private static readonly object padLock = new object();

        public static CommandQueue Instance 
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region Fields
        private readonly Command[] commands;

        private int commandPointer;
        private int commandsCount;

        private Action directionAction;
        #endregion

        #region Properties
        public IEnumerable<Command> Commands
        {
            get
            {
                List<Command> commands = new List<Command>();

                // Count.
                int i = 0;
                // Index.
                int j = commandPointer;

                while (i < commandsCount)
                {
                    commands.Add(this.commands[j]);
                    
                    j--;

                    if (j < 0) j = commandsCount;

                    i++;
                }

                return commands;
            }
        }
        #endregion

        private CommandQueue(int maxCommands)
        {
            commands = new Command[maxCommands];
        }

        public void EnqueueCommand(Command command)
        {
            commands[commandPointer] = command;
            commandPointer++;

            if (commandsCount < commands.Length) commandsCount++;

            if (commandPointer > commands.Length) commandPointer = 0;
        }
        public void UndoCommand()
        {
            commands[commandPointer].Undo();
            commandPointer--;

            if (commandsCount > 0) commandsCount--;

            if (commandPointer < 0) commandPointer = commands.Length;
        }
        public void ReExecuteCommand()
        {
            commands[commandPointer].Execute();
            commandPointer++;

            if (commandsCount < commands.Length) commandsCount++;

            if (commandPointer > commands.Length) commandPointer = 0;
        }
    }
}
