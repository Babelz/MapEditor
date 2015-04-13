using MapEditor.Helpers;
using MapEditorCore.TileEditor;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MapEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewLayerDialog.xaml
    /// </summary>
    public partial class NewTileLayerDialog : Window
    {
        #region Fields
        private readonly TileEditor tileEditor;

        private readonly NewTileLayerProperties newTileLayerProperties;

        private readonly NewTileLayerPropertiesViewModel newTileLayerPropertiesViewModel;
        #endregion

        #region Properties
        private NewTileLayerPropertiesViewModel NewTileLayerPropertiesViewModel
        {
            get
            {
                
                return newTileLayerPropertiesViewModel;
            }
        } 

        public NewTileLayerProperties NewTileLayerProperties
        {
            get
            {
                return newTileLayerProperties;
            }
        }
        #endregion

        public NewTileLayerDialog(TileEditor tileEditor)
        {
            this.tileEditor = tileEditor;

            // Initialize model and view.
            string[] takenNames = tileEditor.Layers
                .Select(s => s.Name)
                .ToArray();

            newTileLayerProperties = new NewTileLayerProperties(takenNames, tileEditor.TileEngine.MaxLayerSizeInTiles.X, tileEditor.TileEngine.MaxLayerSizeInTiles.Y);
            newTileLayerPropertiesViewModel = new NewTileLayerPropertiesViewModel(newTileLayerProperties);

            // Set data context for this window.
            DataContext = newTileLayerPropertiesViewModel;
            
            InitializeComponent();
        }

        #region Event handlers
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!StringHelper.OnlyContainsDigits(textBox.Text)) textBox.Text = StringHelper.RemoveAllNonDigitCharacters(textBox.Text);
        }
        private void createLayerButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Close();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length > 0) textBox.Select(0, textBox.Text.Length);
        }
        #endregion
    }
}
