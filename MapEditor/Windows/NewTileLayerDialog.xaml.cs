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
using System.Windows.Shapes;

namespace MapEditor.Windows
{
    /// <summary>
    /// Interaction logic for NewLayerDialog.xaml
    /// </summary>
    public partial class NewTileLayerDialog : Window
    {
        #region Fields
        private readonly NewTileLayerProperties newTileLayerProperties;

        private readonly NewTileLayerPropertiesViewModel newTileLayerPropertiesViewModel;

        private readonly TileEditor tileEditor;
        #endregion

        #region Properties
        private NewTileLayerPropertiesViewModel NewTileLayerPropertiesViewModel
        {
            get
            {
                return newTileLayerPropertiesViewModel;
            }
        } 

        public NewTileLayerProperties NewTileLayerProperties
        {
            get
            {
                return newTileLayerProperties;
            }
        }
        #endregion

        public NewTileLayerDialog(TileEditor tileEditor)
        {
            // Initialize model and view.
            string[] takenNames = tileEditor.Layers
                .Select(s => s.Name)
                .ToArray();

            newTileLayerProperties = new NewTileLayerProperties(takenNames);
            newTileLayerPropertiesViewModel = new NewTileLayerPropertiesViewModel(newTileLayerProperties);

            //newTileLayerProperties = new NewTileLayerProperties()
            this.tileEditor = tileEditor;

            // Set data context for this window.

            DataContext = newTileLayerPropertiesViewModel;
            
            InitializeComponent();
        }
    }
}
