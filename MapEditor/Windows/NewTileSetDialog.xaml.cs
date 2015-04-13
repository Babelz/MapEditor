using MapEditor.Helpers;
using MapEditorCore.TileEditor;
using MapEditorViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            string[] takenNames = tileEditor.LayerManager.Layers
                .Select(s => s.Name)
                .ToArray();

            // Initialize view model.
            newTilesetProperties = new MapEditorViewModels.NewTilesetProperties(takenNames);
            newTilesetPropertiesViewModel = new NewTilesetPropertiesViewModel(newTilesetProperties);

            // Set data context.
            DataContext = NewTilesetPropertiesViewModel;

            InitializeComponent();
        }

        #region Event handlers
        private void loadTextureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images|*.png;*.jpeg;*.jpg";

            // Loading the file failed, return.
            if (!openFileDialog.ShowDialog().Value) return;

            // Got file, load it.
            BitmapImage image = new BitmapImage();

            // We need to load (copy) it into memory so it wont block our editor side 
            // when we try to load it as a texture.
            using (FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open))
            {
                image.BeginInit();
                
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                
                image.EndInit();
            }

            sheetPreviewView.Image = image;

            // Set path for the model.
            newTilesetPropertiesViewModel.Path = openFileDialog.FileName;

            // Set height for model.
            newTilesetPropertiesViewModel.ImageWidth = (int)sheetPreviewView.Image.Width;
            newTilesetPropertiesViewModel.ImageHeight = (int)sheetPreviewView.Image.Height;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length > 0) textBox.Select(0, textBox.Text.Length);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!StringHelper.OnlyContainsDigits(textBox.Text)) textBox.Text = StringHelper.RemoveAllNonDigitCharacters(textBox.Text);
        }
        private void createTilesetButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Close();
        }
        #endregion
    }
}
