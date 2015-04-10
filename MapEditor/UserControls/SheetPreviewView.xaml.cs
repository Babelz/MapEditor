﻿using MapEditorViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MapEditor.UserControls
{
    /// <summary>
    /// Interaction logic for SheetPreviewView.xaml
    /// </summary>
    public partial class SheetPreviewView : UserControl
    {
        #region Fields
        private BitmapImage image;
        #endregion

        #region Properties
        public int GridOffsetX
        {
            get { return (int)GetValue(GridOffsetXProperty); }
            set { ResizeGrid(); SetValue(GridOffsetXProperty, value); }
        }

        public static readonly DependencyProperty GridOffsetXProperty =
            DependencyProperty.Register("GridOffsetX", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0));

        public int GridOffsetY
        {
            get { return (int)GetValue(GridOffsetYProperty); }
            set { ResizeGrid(); SetValue(GridOffsetYProperty, value); }
        }

        public static readonly DependencyProperty GridOffsetYProperty =
            DependencyProperty.Register("GridOffsetY", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0));

        public int CellWidth
        {
            get { return (int)GetValue(CellWidthProperty); }
            set { ResizeGrid(); SetValue(CellWidthProperty, value); }
        }

        public static readonly DependencyProperty CellWidthProperty =
            DependencyProperty.Register("CellWidth", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0));

        public int CellHeight
        {
            get { return (int)GetValue(CellHeightProperty); }
            set { ResizeGrid(); SetValue(CellHeightProperty, value); }
        }

        public static readonly DependencyProperty CellHeightProperty =
            DependencyProperty.Register("CellHeight", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0));

        public BitmapImage Image
        {
            set
            {
                ResizeGrid();

                // Capture image.
                image = value;

                previewRootCanvas.Width = image.Width;
                previewRootCanvas.Height = image.Height;
                previewRootCanvas.UpdateLayout();

                tileSheetPreviewImage.Source = image;
            }
        }
        #endregion

        public SheetPreviewView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Resize the grid. Might create new cells or remove some.
        /// </summary>
        private void ResizeGrid()
        {
            // Avoid casts.
            int cellWidth = CellWidth;
            int cellHeight = CellHeight;
            
            // If image is null, we need to wait for it to
            // get a proper value before we can resize the grid.
            if (image == null || CellWidth == 0 || CellHeight == 0) return;

            // Calculate space for the grid.
            int modColumns = (int)image.Width / CellWidth;
            int modRows = (int)image.Height / CellHeight;

            int columnSpace = (int)image.Width;
            int rowSpace = (int)image.Height;

            // If modifier is not zero, we need to overlap the image.
            if (modColumns != 0) columnSpace += CellWidth;
            if (modRows != 0) rowSpace += CellHeight;

            int columns = columnSpace / CellWidth;
            int rows = rowSpace / CellHeight;

            int spacePerColumn = columns / columns;
            int spacePerRow = rowSpace / rows;

            // Resize the grid.
            for (int i = 0; i < rows; i++)
            {
                // Create new row.
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(spacePerRow);

                // Add new row.
                tileGrid.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < columns; i++)
            {
                // Create new column.
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(columnSpace);

                // Add new column.
                tileGrid.ColumnDefinitions.Add(columnDefinition);
            }
        }
    }
}
