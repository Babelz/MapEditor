using MapEditor.Configurers;
using MapEditorCore;
using MapEditorCore.TileEditor;
using MapEditorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = Microsoft.Xna.Framework.Point;
using Project = MapEditorCore.Project;

namespace MapEditor
{
    public static class ProjectBuilder
    {
        /// <summary>
        /// Build new tile map project from given project properties.
        /// </summary>
        public static Project BuildTileMapProject(NewProjectProperties properties, IntPtr windowHandle)
        {
            TileEngine tileEngine = new TileEngine(new Point(properties.MapWidth, properties.MapHeight),
                                                   new Point(properties.TileWidth, properties.TileHeight));

            TileEditor editor = new TileEditor(tileEngine);
            EditorGame game = new EditorGame(windowHandle, editor);
            
            return new Project(new TileEditorGUIConfigurer(editor),
                               game, editor, 
                               properties.ProjectName, 
                               properties.MapName, 
                               properties.MapType.ToString());
        }
    }
}
