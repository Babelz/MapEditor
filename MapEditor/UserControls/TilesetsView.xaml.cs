using MapEditor.Components;
using MapEditor.Helpers;
using MapEditorCore.TileEditor;
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
        private readonly BrushesViewModel brushesViewModel;
        private readonly TileGridManager tileGridManager;
        private readonly TileEditor editor;

        private readonly TilesetsViewModel tilesetsViewModel;
        #endregion

        #region Properties
        private TilesetsViewModel TilesetsViewModel
        {
            get
            {
                return tilesetsViewModel;
            }
        }
        #endregion

        public TilesetsView(TileEditor editor, BrushesViewModel brushesViewModel)
        {
            this.editor = editor;
            this.brushesViewModel = brushesViewModel;

            brushesViewModel.PropertyChanged += brushesViewModel_PropertyChanged;

            // Initialize view model.
            tilesetsViewModel = new TilesetsViewModel(editor);

            // Set data context.
            DataContext = tilesetsViewModel;

            InitializeComponent();

            setsListView.ItemsSource = tilesetsViewModel.Tilesets;

            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(setsListView.ItemsSource);
            collectionView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));

            tileGridManager = new TileGridManager(tileGrid, gridBorder);
        }

        #region Event handlers
        private void brushesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(brushesViewModel.Selected == null) return;

            Tileset selectedTileSet = brushesViewModel.Selected.Tileset;

            ReconstructGrid(selectedTileSet);
        }
        private void setsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get selected set.
            TilesetViewModel tilesetViewModel = null;

            foreach (object o in e.AddedItems)
            {
                tilesetViewModel = o as TilesetViewModel;
            }

            // No set, return.
            if (tilesetViewModel == null)
            {
                editor.SelectTileset(string.Empty);

                return;
            }

            // Notify editor.
            editor.SelectTileset(tilesetViewModel.Name);

            ReconstructGrid(editor.Tilesets.FirstOrDefault(t => t.Name == tilesetViewModel.Name));
        }
        private void sheetCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Check if there is brush selected.
            if (tilesetsViewModel.Selected == null) return;
            if (brushesViewModel.Selected == null) return;

            // Get tileset and mouse position.
            Point position = Mouse.GetPosition(gridBorder);

            Tileset tileset = editor.Tilesets.FirstOrDefault(t => t.Name == tilesetsViewModel.Selected.Name);

            brushesViewModel.Selected.SelectIndex((int)position.X, (int)position.Y);
        }
        #endregion

        private void ReconstructGrid(Tileset tileset)
        {
            int width = 0;
            int height = 0;

            sheetImage.Source = null;

            if (tileset != null)
            {
                // Calculate area size.
                width = tileset.SourceSize.X * tileset.IndicesCount.X;
                height = tileset.SourceSize.Y * tileset.IndicesCount.Y;

                // Reconstruct grid.
                string pathToImage = editor.GetTexturePath(tileset.Texture);

                BitmapImage image = ImageHelper.LoadToMemory(pathToImage);

                sheetImage.Source = image;
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
        }
    }
}
