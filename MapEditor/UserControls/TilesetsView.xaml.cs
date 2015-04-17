using MapEditorCore.TileEditor;
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

        #endregion

        public TilesetsView(TileEditor editor)
        {
            InitializeComponent();
        }

        private void setsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
