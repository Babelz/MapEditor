using MapEditor.Helpers;
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
    /// Interaction logic for ResizeTileLayerDialog.xaml
    /// </summary>
    public partial class ResizeTileLayerDialog : Window
    {
        #region Fields
        private readonly ResizeModel resizeModel;

        private readonly ResizeViewModel resizeViewModel;
        #endregion

        #region Properties
        public ResizeModel ResizeModel
        {
            get
            {
                return resizeModel;
            }
        }
        #endregion

        public ResizeTileLayerDialog(int maxLayerWidth, int maxLayerHeight, IEnumerable<string> layers)
        {
            // Initialize model and view model.
            resizeModel = new ResizeModel(maxLayerWidth, maxLayerHeight);
            resizeViewModel = new ResizeViewModel(resizeModel, layers);

            // Set data context.
            DataContext = resizeViewModel;

            InitializeComponent();
        }

        #region Event handlers
        private void resizeLayerButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Close();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!StringHelper.OnlyContainsDigits(textBox.Text)) textBox.Text = StringHelper.RemoveAllNonDigitCharacters(textBox.Text);
        }
        #endregion
    }
}
