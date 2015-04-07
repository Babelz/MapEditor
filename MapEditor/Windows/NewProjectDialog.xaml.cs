using MapEditor.Components;
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
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : Window
    {
        #region Fields
        private readonly NewProjectProperties newProjectProperties;
        
        private readonly NewProjectPropertiesViewModel newProjectPropertiesViewModel;
        #endregion

        #region Properties
        public NewProjectProperties NewProjectProperties
        {
            get
            {
                return newProjectProperties;
            }
        }

        public NewProjectPropertiesViewModel NewProjectPropertiesViewModel
        {
            get
            {
                return newProjectPropertiesViewModel;
            }
        }
        #endregion

        public NewProjectDialog()
        {
            // Initialize model and view.
            newProjectProperties = new NewProjectProperties();
            newProjectPropertiesViewModel = new NewProjectPropertiesViewModel(newProjectProperties);

            // Set data context of this window.
            DataContext = newProjectPropertiesViewModel;

            InitializeComponent();

            // Set item source of type combo box.
            mapTypeComboBox.ItemsSource = Enum.GetValues(typeof(MapType)).Cast<MapType>(); 
        }
        
        #region Event handlers
        /// <summary>
        /// Show the wanted unit for this map type. Can be tiles of pixels.
        /// </summary>
        private void mapTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the map type.
            MapType mapType = (MapType)mapTypeComboBox.SelectedValue;
            
            // Change unit.
            switch (mapType)
            {
                case MapType.Tile:
                case MapType.Hex:
                    sizeUnitTextBlock.Text = "tiles";
                    break;
                case MapType.Object:
                    sizeUnitTextBlock.Text = "pixels";
                    break;
                default:
                    throw new InvalidOperationException("Unsupported map type.");
            }
        }
        /// <summary>
        /// Validate that the input only contains digits.
        /// </summary>
        private void TextBox_ValidateInput(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            
            if (!StringHelper.OnlyContainsDigits(textBox.Text)) textBox.Text = StringHelper.RemoveAllNonDigitCharacters(textBox.Text);
        }
        /// <summary>
        /// Return positive dialog results.
        /// </summary>
        private void createProjectButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

            Close();
        }
        #endregion
    }
}
