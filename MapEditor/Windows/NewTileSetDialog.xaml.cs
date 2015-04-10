﻿using MapEditor.Helpers;
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

            // Set data context.
            DataContext = NewTilesetPropertiesViewModel;

            InitializeComponent();
        }

        #region Event handlers
        private void loadTextureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images *.png *.jpeg *.jpg";

            // Loading the file failed, return.
            if (!openFileDialog.ShowDialog().Value) return;

            // Got file, load it.
            sheetPreviewView.Image = new BitmapImage(new Uri(openFileDialog.FileName));

            // Set path for the model.
            newTilesetProperties.Path = openFileDialog.FileName;

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
