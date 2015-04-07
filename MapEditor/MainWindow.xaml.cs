using MapEditor.Windows;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
                // Dialog OK, create new project.
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
    }
}
