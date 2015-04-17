using MapEditorCore.TileEditor;
using MapEditorViewModels;
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
        }
        #endregion

        private void rootLayoutAnchorable_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
