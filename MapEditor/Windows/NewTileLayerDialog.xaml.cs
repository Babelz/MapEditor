﻿using MapEditorCore.TileEditor;
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
        #endregion

        public NewTileLayerDialog(TileEditor tileEditor)
        {
            this.tileEditor = tileEditor;
            
            InitializeComponent();
        }
    }
}
