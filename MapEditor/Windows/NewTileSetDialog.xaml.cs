using MapEditorCore.TileEditor;
using MapEditorViewModels;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace MapEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewTilesetDialog.xaml
    /// </summary>
    public partial class NewTilesetDialog : Window
    {
        #region Fields
        private readonly TileEditor tileEditor;

        private readonly NewTilesetProperties newTilesetProperties;

        private readonly NewTilesetPropertiesViewModel newTilesetPropertiesViewModel;
        #endregion

        #region Properties
        private NewTilesetPropertiesViewModel NewTilesetPropertiesViewModel
        {
            get
            {
                return newTilesetPropertiesViewModel;
            }
        }

        public NewTilesetProperties NewTilesetProperties
        {
            get
            {
                return newTilesetProperties;
            }
        }
        #endregion

        public NewTilesetDialog(TileEditor tileEditor)
        {
            this.tileEditor = tileEditor;

            // Get taken names.
            string[] takenNames = new string[1];

            // Initialize view model.
            newTilesetProperties = new MapEditorViewModels.NewTilesetProperties(takenNames);
            newTilesetPropertiesViewModel = new NewTilesetPropertiesViewModel(newTilesetProperties);

            InitializeComponent();
        }

        #region Event handlers
        private void loadTextureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png)|*.png|(*.jpeg)|*.jpeg|(*.jpg)|*.jpg";

            // Loading the file failed, return.
            if (!openFileDialog.ShowDialog().Value) return;

            // Got file, load it.
            tilesetImagePreview.Source = new BitmapImage(new Uri(openFileDialog.FileName));

        }
        #endregion
    }
}
