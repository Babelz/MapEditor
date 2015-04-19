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

        public TilesetsView(TileEditor editor)
        {
            this.editor = editor;

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

            ReconstructGrid(tilesetViewModel.Tileset);
        }
        #endregion

        private Tileset GetSelectedSet()
        {
            return setsListView.SelectedValue as Tileset;
        }

        private void ReconstructGrid(Tileset tileset)
        {
            // Reconstruct grid.
            string pathToImage = editor.GetTexturePath(tileset.Texture);

            BitmapImage image = ImageHelper.LoadToMemory(pathToImage);
            
            sheetImage.Source = image;

            // Keep offset at zero, we dont want to move the grid but the image instead.
            tileGridManager.Reconstruct(tileset.IndicesCount.X * tileset.SourceSize.X,
                                        tileset.IndicesCount.Y * tileset.SourceSize.Y, 
                                        tileset.SourceSize.X, 
                                        tileset.SourceSize.Y, 
                                        0, 
                                        0);

            Canvas.SetLeft(sheetImage, -tileset.Offset.X);
            Canvas.SetTop(sheetImage, -tileset.Offset.Y);

            // Set view size.
            sheetCanvas.Width = tileset.Texture.Width - tileset.Offset.X;
            sheetCanvas.Height = tileset.Texture.Height - tileset.Offset.Y;

            gridBorder.Width = tileset.IndicesCount.X * tileset.SourceSize.X;
            gridBorder.Height = tileset.IndicesCount.Y * tileset.SourceSize.Y;
        }
    }
}
