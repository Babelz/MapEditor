using MapEditor.Configurers;
using MapEditorCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    /// <summary>
    /// Project class. Wraps all project related stuffs.
    /// </summary>
    public sealed class Project : IDisposable
    {
        #region Fields
        private readonly string mapTypename;

        private readonly IGUIConfigurer configurer;
        private readonly EditorGame game;
        private readonly Editor editor;

        private string name;
        private string mapName;

        private bool disposed;
        #endregion

        #region Properties
        public IGUIConfigurer Configurer
        {
            get
            {
                return configurer;
            }
        }
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

        public Project(IGUIConfigurer configurer, EditorGame game, Editor editor, string name, string mapName, string mapTypename)
        {
            this.configurer = configurer;
            this.game = game;
            this.editor = editor;

            this.name = name;
            this.mapName = mapName;
            this.mapTypename = mapTypename;
        }

        // TODO: loading and saving... or?

        public void Dispose()
        {
            if (disposed) return;

            editor.Dispose();
            game.Dispose();

            disposed = true;
        }
    }
}
