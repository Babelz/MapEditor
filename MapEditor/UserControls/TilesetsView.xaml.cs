using MapEditor.Components;
using MapEditor.Helpers;
using MapEditor.Windows;
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

using Point = Microsoft.Xna.Framework.Point;
using TileBrush = MapEditorCore.TileEditor.Painting.TileBrush;

namespace MapEditor.UserControls
{
    /// <summary>
    /// Interaction logic for SheetsView.xaml
    /// </summary>
    public partial class TilesetsView : UserControl
    {
        #region Fields
        // Rectangles of the selection grid.
        private readonly List<Rectangle> rectangles;

        private readonly TilesetsViewModel tilesetsViewModel;
        private readonly BrushesViewModel brushesViewModel;
        
        private readonly TileGridManager tileGridManager;
        private readonly TileGridManager selectionGridManager;

        private readonly TileEditor editor;
        #endregion

        public TilesetsView(TileEditor editor, BrushesViewModel brushesViewModel, TilesetsViewModel tilesetsViewModel)
        {
            this.editor = editor;
            this.brushesViewModel = brushesViewModel;
            this.tilesetsViewModel = tilesetsViewModel;

            brushesViewModel.PropertyChanged += brushesViewModel_PropertyChanged;

            // Set data context.
            DataContext = tilesetsViewModel;

            InitializeComponent();

            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(setsListView.ItemsSource);
            collectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));

            tileGridManager = new TileGridManager(tileGrid, gridBorder);
            selectionGridManager = new TileGridManager(selectionGrid, selectionBorder);

            rectangles = new List<Rectangle>();

            // Hide view by default.
            tilesetView.Visibility = Visibility.Hidden;
        }

        #region Event handlers
        private void brushesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected") ReconstructSelectionGrid();
        }
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
            if (tilesetViewModel == null)
            {
                tilesetView.Visibility = Visibility.Hidden;

                return;
            }

            tilesetView.Visibility = Visibility.Visible;
            
            // Reconstruct grid and selection grid.
            ReconstructGrid(editor.Tilesets.FirstOrDefault(t => t.Name == tilesetViewModel.Name));

            ReconstructSelectionGrid();
        }
        private void sheetCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Check if there is brush selected and tile set selected.
            if (tilesetsViewModel.Selected == null) return;
            if (brushesViewModel.Selected == null) return;

            // Get mouse position
            System.Windows.Point position = Mouse.GetPosition(gridBorder);
            int positionX = (int)position.X / tilesetsViewModel.Selected.TileWidth;
            int positionY = (int)position.Y / tilesetsViewModel.Selected.TileHeight;

            // Get bucket.
            BrushBucket brushBucket = editor.GetBrushBucketForSelectedTileset();

            // Keep selected brush in bounds.
            TileBrush brush = brushBucket.SelectedBrush;

            if (positionX + brush.DisplayWidth > tilesetsViewModel.Selected.Columns) positionX = tilesetsViewModel.Selected.Columns - brush.DisplayWidth;
            if (positionY + brush.DisplayHeight > tilesetsViewModel.Selected.Rows) positionY = tilesetsViewModel.Selected.Rows - brush.DisplayHeight;

            // Select wanted index.
            brush.SelectIndex(positionX, positionY);
            
            // Reconstruct selection grid.
            ReconstructSelectionGrid();
        }
        private void newSetButton_Click(object sender, RoutedEventArgs e)
        {
            NewTilesetDialog newTilesetDialog = new NewTilesetDialog(editor);

            if (newTilesetDialog.ShowDialog().Value)
            {
                // Got properties, create new tileset.
                NewTilesetProperties newTilesetProperties = newTilesetDialog.NewTilesetProperties;

                editor.AddTileset(newTilesetProperties.Name, newTilesetProperties.Path, new Point(newTilesetProperties.TileWidth, newTilesetProperties.TileHeight),
                                                                                        new Point(newTilesetProperties.OffsetX, newTilesetProperties.OffsetY));


                // Add new tileset to view model.
                tilesetsViewModel.Tilesets.Add(new TilesetViewModel(editor.Tilesets.FirstOrDefault(t => t.Name == newTilesetProperties.Name)));
            }
        }
        private void deleteSetButton_Click(object sender, RoutedEventArgs e)
        {
            editor.RemoveTileset(tilesetsViewModel.Selected.Name);

            tilesetsViewModel.Tilesets.Remove(tilesetsViewModel.Selected);
            tilesetsViewModel.Selected = null;
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

            // Set border size.
            gridBorder.Width = width;
            gridBorder.Height = height;

            // Set image size. Adjust it to the size of the view.
            sheetImage.Width = Math.Min(width, sheetImage.Source.Width);
            sheetImage.Height = Math.Min(height, sheetImage.Source.Height);
        }
        private void ReconstructSelectionGrid()
        {
            int width = 0;
            int height = 0;

            // Can only reconstruct if there is a brush and layer selected.
            if (brushesViewModel.Selected != null && tilesetsViewModel.Selected != null)
            {
                width = brushesViewModel.Selected.Width * tilesetsViewModel.Selected.TileWidth;
                height = brushesViewModel.Selected.Height * tilesetsViewModel.Selected.TileHeight;
            }

            BrushBucket bucket = editor.GetBrushBucketForSelectedTileset();
            TileBrush brush = bucket.SelectedBrush;

            selectionGridManager.Reconstruct(width,
                                             height,
                                             tilesetsViewModel.Selected.TileWidth,
                                             tilesetsViewModel.Selected.TileHeight,
                                             brush.SelectedIndexX * tilesetsViewModel.Selected.TileWidth,
                                             brush.SelectedIndexY * tilesetsViewModel.Selected.TileHeight);
            
            // Resize view.
            selectionBorder.Width = width;
            selectionBorder.Height = height;

            // "Generate" cells. Fill the grid with colored rectangles.

            // Remove old rectangles.
            if (rectangles.Count > 0)
            {
                for (int i = 0; i < rectangles.Count; i++) selectionGrid.Children.Remove(rectangles[i]);

                rectangles.Clear();
            }

            // Generate new rectangles.
            for (int i = 0; i < selectionGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < selectionGrid.ColumnDefinitions.Count; j++)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        Fill = Brushes.Transparent,
                        Stroke = Brushes.Red
                    };

                    // Add new rect to lists.
                    rectangles.Add(rectangle);
                    selectionGrid.Children.Add(rectangle);

                    // Set its row and column.
                    Grid.SetRow(rectangle, i);
                    Grid.SetColumn(rectangle, j);
                }
            }
        }
    }
}
