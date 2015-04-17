using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MapEditor.Components
{
    public sealed class TileGridManager
    {
        #region Fields        
        private readonly List<RowDefinition> currentRows;
        private readonly List<ColumnDefinition> currentColumns;

        private readonly Grid grid;
        private readonly Border gridBorder;
        #endregion

        public TileGridManager(Grid grid, Border gridBorder)
        {
            this.grid = grid;
            this.gridBorder = gridBorder;

            currentRows = new List<RowDefinition>();
            currentColumns = new List<ColumnDefinition>();
        }


        /// <summary>
        /// Reconstruct the grid.
        /// WARNING: could use some optimization. Grid is reconstructed every time
        /// something gets changed.
        /// </summary>
        public void Reconstruct(BitmapImage image, int cellWidth, int cellHeight, int gridOffsetX, int gridOffsetY)
        {
            // Cant continue if there is no image.
            if (image == null) return;

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

            if (cellWidth == 0 || cellHeight == 0) return;

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
                for (int i = 0; i < currentRows.Count; i++) grid.RowDefinitions.Remove(currentRows[i]);

                currentRows.Clear();
            }

            if (currentColumns.Count > 0)
            {
                for (int i = 0; i < currentColumns.Count; i++) grid.ColumnDefinitions.Remove(currentColumns[i]);

                currentColumns.Clear();
            }

            // Resize the grid.
            for (int i = 0; i < rows; i++)
            {
                // Create new row.
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(spacePerRow);

                // Add new row.
                grid.RowDefinitions.Add(rowDefinition);

                currentRows.Add(rowDefinition);
            }

            for (int i = 0; i < columns; i++)
            {
                // Create new column.
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(spacePerColumn);

                // Add new column.
                grid.ColumnDefinitions.Add(columnDefinition);

                currentColumns.Add(columnDefinition);
            }

            grid.UpdateLayout();
        }
    }
}
