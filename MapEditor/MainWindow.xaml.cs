using MapEditor.Windows;
using MapEditorCore;
using MapEditorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using XNAUserControl = XNAControl.UserControl1;

namespace MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private Project project;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Event handlers

        #region File menu event handlers
        private void newProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewProjectDialog newProjectDialog = new NewProjectDialog();
            
            // Show new project dialog to the user.
            if (newProjectDialog.ShowDialog().Value)
            {
                // Dialog OK, got some correct properties for creating editor.
                
                // Dispose old project and show warning dialog to the user.
                if (project != null)
                {
                    if (MessageBox.Show("All unsaved work will be lost, continue?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                    CleanupLastProject();
                }

                // Create new project.
                NewProjectProperties properties = newProjectDialog.NewProjectProperties;
                
                switch (properties.MapType)
                {
                    case MapType.Tile:
                        project = ProjectBuilder.BuildTileMapProject(properties, xnaControl.Handle);
                        break;
                    case MapType.Object:
                    case MapType.Hex:
                    default:
                        throw new NotImplementedException("Feature is not implemented.");
                }

                // Initialize the project.
                InitializeNewProject();
            }

            // Invalid results, continue.
        }
        private void loadProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO: load project.
        }
        private void saveProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO: save project.
        }
        #endregion

        #endregion

        private void CleanupLastProject()
        {
            project.Configurer.RemoveConfiguration(this);
            project.Dispose();

            project = null;
        }
        private void InitializeNewProject()
        {
            project.Configurer.Configure(this);
        }
    }
}
