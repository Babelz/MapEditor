using MapEditor.Components;
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
        private readonly TileGridManager tileGridManager;

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

            tileGridManager = new TileGridManager(tileGrid, gridBorder);
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

            // Return if the view model is not found and hide 
            // the preview window.
            if (newTilesetPropertiesViewModel == null) return;

            Point mousePosition = e.GetPosition(previewRootCanvas);

            int offsetX = (int)mousePosition.X;
            int offsetY = (int)mousePosition.Y;
            
            newTilesetPropertiesViewModel.OffsetX = offsetX.ToString();
            newTilesetPropertiesViewModel.OffsetY = offsetY.ToString();
        }
        #endregion

        private void ReconstructGrid()
        {
            // Cant reconstruct if image is null.
            if (image == null) return;

            // Calculate columns.
            int columns = 0;
            if (CellWidth == 0) return;
            
            columns = (int)((image.Width - GridOffsetX) / CellWidth);

            // Calculate rows.
            int rows = 0;
            if (CellHeight == 0) return;
            
            rows = (int)((image.Height - GridOffsetY) / CellHeight);

            // Calculate row modulo.
            int modRows = (int)(image.Height - GridOffsetY) % CellHeight;
            modRows = modRows > 0 ? 1 : 0;

            // Calculate column modulo.
            int modColumns = (int)(image.Width - GridOffsetX) % CellWidth;
            modColumns = modColumns > 0 ? 1 : 0;

            // Calculate width and height.
            int width = (modColumns + columns) * CellWidth;
            int height = (modRows + rows) * CellHeight;

            tileGridManager.Reconstruct(width, height, CellWidth, CellHeight, GridOffsetX, GridOffsetY);

            // Set borders size.
            gridBorder.Width = width;
            gridBorder.Height = height;
        }
    }
}
