using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore
{
    /// <summary>
    /// Project class. Wraps all project related stuffs.
    /// </summary>
    public sealed class Project : IDisposable
    {
        #region Fields
        private readonly string mapTypename;

        private readonly EditorGame game;
        private readonly Editor editor;

        private string name;
        private string mapName;

        private bool disposed;
        #endregion

        #region Properties
        /// <summary>
        /// Typename of the map.
        /// </summary>
        public string MapTypename
        {
            get
            {
                return mapTypename;
            }
        }
        /// <summary>
        /// Name of the project.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        /// <summary>
        /// Name of the map.
        /// </summary>
        public string MapName
        {
            get
            {
                return mapName;
            }
            set
            {
                mapName = value;
            }
        }
        /// <summary>
        /// Editor used for this project.
        /// </summary>
        public Editor Editor
        {
            get
            {
                return editor;
            }
        }
        /// <summary>
        /// Game associated with this project.
        /// </summary>
        public EditorGame Game
        {
            get
            {
                return game;
            }
        }
        public bool Disposed
        {
            get
            {
                return disposed;
            }
        }
        #endregion

        public Project(EditorGame game, Editor editor, string name, string mapName, string mapTypename)
        {
            this.game = game;
            this.editor = editor;

            this.name = name;
            this.mapName = mapName;
            this.mapTypename = mapTypename;
        }

        // TODO: loading and saving...

        public void Dispose()
        {
            if (disposed) return;

            game.Dispose();

            disposed = true;
        }
    }
}
