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

        #endregion

        #region Properties

        #endregion

        public NewTilesetDialog()
        {
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
