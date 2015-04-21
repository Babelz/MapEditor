using MapEditorCore.TileEditor.Painting;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorViewModels
{
    public sealed class BrushViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly TileBrush brush;
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return brush.Name;
            }
        }
        public bool CanResize
        {
            get
            {
                return brush.ResizeMode == BrushResizeMode.CanResize;
            }
        }
        public int Width
        {
            get
            {
                return brush.DisplayWidth;
            }
            set
            {
                brush.Resize(value, brush.Height);

                OnPropertyChanged("Width");
            }
        }
        public int Height
        {
            get
            {
                return brush.DisplayHeight;
            }
            set
            {
                brush.Resize(brush.Width, value);

                OnPropertyChanged("Height");
            }
        }
        public Color Color
        {
            get
            {
                return brush.Color;
            }
            set
            {
                brush.Color = value;

                OnPropertyChanged("Color");
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public BrushViewModel(TileBrush brush)
        {
            this.brush = brush;
        }

        public bool WrapsBrush(TileBrush brush)
        {
            return ReferenceEquals(this.brush, brush);
        }

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
