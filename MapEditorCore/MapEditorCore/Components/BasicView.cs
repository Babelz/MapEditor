using MapEditorCore.Abstractions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditorCore.Components
{
    /// <summary>
    /// Basic camera with no zoom for the editor.
    /// </summary>
    public sealed class BasicView : IView
    {
        #region Fields
        private Point position;
        private Point size;
        #endregion

        #region Properties
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(position.X, position.Y, size.X, size.Y);
            }
        }
        public Matrix Transformation
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0.0f)) *
                              Matrix.CreateRotationZ(0.0f) *
                              Matrix.CreateScale(new Vector3(1.0f, 1.0f, 1.0f)) *
                              Matrix.CreateTranslation(new Vector3(0.0f));
            }
        }
        public int Width
        {
            get
            {
                return size.X;
            }
            set
            {
                size.X = value;
            }
        }
        public int Height
        {
            get
            {
                return size.Y;
            }
            set
            {
                size.Y = value;
            }
        }
        #endregion

        public BasicView()
        {
        }

        public void SetArea(int width, int height)
        {
            size.X = width;
            size.Y = height;
        }

        public void MoveTo(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public void MoveBy(int x, int y)
        {
            position.X += x;
            position.Y += y;
        }
    }
}
