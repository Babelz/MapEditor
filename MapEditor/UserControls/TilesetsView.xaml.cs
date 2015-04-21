using MapEditor.Components;
using MapEditor.Helpers;
using MapEditorCore.TileEditor;
using MapEditorCore.TileEditor.Painting;
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
    /// Interaction logic for SheetsView.xaml
    /// </summary>
    public partial class TilesetsView : UserControl
    {
        #region Fields
        private readonly TilesetsViewModel tilesetsViewModel;
        private readonly BrushesViewModel brushesViewModel;
        
        private readonly TileGridManager tileGridManager;

        private readonly TileEditor editor;
        #endregion

        public TilesetsView(TileEditor editor, BrushesViewModel brushesViewModel, TilesetsViewModel tilesetsViewModel)
        {
            this.editor = editor;
            this.brushesViewModel = brushesViewModel;
            this.tilesetsViewModel = tilesetsViewModel;

            // Set data context.
            DataContext = tilesetsViewModel;

            InitializeComponent();

            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(setsListView.ItemsSource);
            collectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));

            tileGridManager = new TileGridManager(tileGrid, gridBorder);
        }

        #region Event handlers
        private void setsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Editor has been notified (inside the tilesets view model selected get), just do grid processing.
            
            // Get selected set.
            TilesetViewModel tilesetViewModel = null;

            foreach (object o in e.AddedItems)
            {
                tilesetViewModel = o as TilesetViewModel;
            }

            // No set, return.
            if (tilesetViewModel == null) return;
            
            ReconstructGrid(editor.Tilesets.FirstOrDefault(t => t.Name == tilesetViewModel.Name));
        }
        private void sheetCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Check if there is brush selected and tile set selected.
            if (tilesetsViewModel.Selected == null) return;
            if (brushesViewModel.Selected == null) return;

            // Get tileset and mouse position.
            Point position = Mouse.GetPosition(gridBorder);

            // Get bucket.
            BrushBucket brushBucket = editor.GetBrushBucketForSelectedTileset();

            // Select wanted index.
            brushBucket.SelectedBrush.SelectIndex((int)position.X / tilesetsViewModel.Selected.TileWidth,
                                                  (int)position.Y / tilesetsViewModel.Selected.TileHeight);
        }
        #endregion

        private void ReconstructGrid(Tileset tileset)
        {
            int width = 0;
            int height = 0;

            sheetImage.Source = null;

            // If we have tileset selected, we can reconstruct the grid. Else, just reset it.
            if (tileset != null)
            {
                // Reconstruct grid.
                string pathToImage = editor.GetTexturePath(tileset.Texture);

                BitmapImage image = ImageHelper.LoadToMemory(pathToImage);

                sheetImage.Source = image;

                // Calculate area size.
                width = tileset.SourceSize.X * tileset.IndicesCount.X;
                height = tileset.SourceSize.Y * tileset.IndicesCount.Y;
            }

            // Keep offset at zero, we don't want to move the grid but the image instead.
            tileGridManager.Reconstruct(width,
                                        height, 
                                        tileset.SourceSize.X, 
                                        tileset.SourceSize.Y, 
                                        0, 
                                        0);

            Canvas.SetLeft(sheetImage, -tileset.Offset.X);
            Canvas.SetTop(sheetImage, -tileset.Offset.Y);

            // Set view size.
            sheetCanvas.Width = width;
            sheetCanvas.Height = height;

            gridBorder.Width = width;
            gridBorder.Height = height;

            sheetImage.Width = Math.Min(width, sheetImage.Source.Width);
            sheetImage.Height = Math.Min(height, sheetImage.Source.Height);
        }
    }
}
