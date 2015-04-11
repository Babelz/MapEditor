using MapEditorViewModels;
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
        private readonly List<RowDefinition> currentRows;
        private readonly List<ColumnDefinition> currentColumns;

        private BitmapImage image;
        #endregion

        #region Properties
        public int GridOffsetX
        {
            get { return (int)GetValue(GridOffsetXProperty); }
            set { SetValue(GridOffsetXProperty, value); }
        }

        public static readonly DependencyProperty GridOffsetXProperty =
            DependencyProperty.Register("GridOffsetX", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0, Changed));

        public int GridOffsetY
        {
            get { return (int)GetValue(GridOffsetYProperty); }
            set { SetValue(GridOffsetYProperty, value); }
        }

        public static readonly DependencyProperty GridOffsetYProperty =
            DependencyProperty.Register("GridOffsetY", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0, Changed));

        public int CellWidth
        {
            get { return (int)GetValue(CellWidthProperty); }
            set { SetValue(CellWidthProperty, value); }
        }

        public static readonly DependencyProperty CellWidthProperty =
            DependencyProperty.Register("CellWidth", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0, Changed));

        public int CellHeight
        {
            get { return (int)GetValue(CellHeightProperty); }
            set { SetValue(CellHeightProperty, value); }
        }

        public static readonly DependencyProperty CellHeightProperty =
            DependencyProperty.Register("CellHeight", typeof(int), typeof(SheetPreviewView), new PropertyMetadata(0, Changed));

        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                // Capture image.
                image = value;

                previewRootCanvas.Width = image.Width;
                previewRootCanvas.Height = image.Height;
                previewRootCanvas.UpdateLayout();

                tileSheetPreviewImage.Source = image;

                // Resize grid.
                ReconstructGrid();
            }
        }
        #endregion

        public SheetPreviewView()
        {
            InitializeComponent();

            currentRows = new List<RowDefinition>();
            currentColumns = new List<ColumnDefinition>();
        }

        #region Static methods
        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) 
        {
            SheetPreviewView sheetPreviewView = d as SheetPreviewView;

            sheetPreviewView.ReconstructGrid();
        }
        #endregion

        #region Event handlers
        private void previewRootCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Update view model.
            NewTilesetPropertiesViewModel newTilesetPropertiesViewModel = DataContext as NewTilesetPropertiesViewModel;

            // Return of the view model is not found.
            if (newTilesetPropertiesViewModel == null) return;

            Point mousePosition = e.GetPosition(previewRootCanvas);

            int offsetX = (int)mousePosition.X;
            int offsetY = (int)mousePosition.Y;

            newTilesetPropertiesViewModel.OffsetX = offsetX.ToString();
            newTilesetPropertiesViewModel.OffsetY = offsetY.ToString();
        }
        #endregion

        // TODO: could use the mouse to set offset values? Bet some people would find it to be a nice feature.

        /// <summary>
        /// Reconstruct the grid.
        /// WARNING: could use some optimization. Grid is reconstructed every time
        /// something gets changed.
        /// </summary>
        private void ReconstructGrid()
        {
            // To avoid casts.
            int gridOffsetX = GridOffsetX;
            int gridOffsetY = GridOffsetY;

            int cellWidth = CellWidth;
            int cellHeight = CellHeight;

            // Move view of the grid to new location.
            Canvas.SetTop(gridBorder, gridOffsetY);
            Canvas.SetLeft(gridBorder, gridOffsetX);

            gridBorder.Width = image.Width;
            gridBorder.Height = image.Height;

            // Resize the grid canvas.
            double newWidth = gridBorder.Width - Canvas.GetLeft(gridBorder);
            double newHeight = gridBorder.Height - Canvas.GetTop(gridBorder);

            // Check if new size properties are valid.
            if (newWidth > 0.0) gridBorder.Width = newWidth;
            if (newHeight > 0.0) gridBorder.Height = newHeight; 

            // If image is null, we need to wait for it to
            // get a proper value before we can resize the grid.
            if (image == null || cellWidth == 0 || cellHeight == 0) return;

            // Calculate space for the grid.
            int modColumns = (int)image.Width % cellWidth;
            int modRows = (int)image.Height % cellHeight;

            int columnSpace = (int)image.Width;
            int rowSpace = (int)image.Height;

            // If modifier is not zero, we need to overlap the image.
            if (modColumns != 0) columnSpace += cellWidth;
            if (modRows != 0) rowSpace += cellHeight;

            int columns = columnSpace / cellWidth;
            int rows = rowSpace / cellHeight;

            // Return to avoid divide by zero.
            if (columns == 0 || rows == 0) return;

            int spacePerColumn = columnSpace / columns;
            int spacePerRow = rowSpace / rows;

            // Remove old rows and columns.
            if (currentRows.Count > 0)
            {
                for (int i = 0; i < currentRows.Count; i++) tileGrid.RowDefinitions.Remove(currentRows[i]);

                currentRows.Clear();
            }

            if (currentColumns.Count > 0)
            {
                for (int i = 0; i < currentColumns.Count; i++) tileGrid.ColumnDefinitions.Remove(currentColumns[i]);

                currentColumns.Clear();
            }

            // Resize the grid.
            for (int i = 0; i < rows; i++)
            {
                // Create new row.
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(spacePerRow);

                // Add new row.
                tileGrid.RowDefinitions.Add(rowDefinition);

                currentRows.Add(rowDefinition);
            }

            for (int i = 0; i < columns; i++)
            {
                // Create new column.
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(spacePerColumn);

                // Add new column.
                tileGrid.ColumnDefinitions.Add(columnDefinition);
                
                currentColumns.Add(columnDefinition);
            }

            tileGrid.UpdateLayout();
        }
    }
}
