using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Obj;
using System.Threading;
using System;

namespace MgEngine.Shape
{
    public class Rect : Box2D
    {    

        private Vector2[] _vertices;

        public Rect(int x, int y, int width, int height) : base(x, y, width, height)
        {
            _vertices = new Vector2[4];
            CalculateVertices();
        }

        #region Properties
        public int Left { get { return X; } }
        public int Right { get { return X + Width; } }
        public int Top { get { return Y; } }
        public int Bottom { get { return Y + Height; } }
        public Vector2[] Vertices { get { return _vertices; } }
        public Rectangle Rectangle { get { return new Rectangle(X, Y, Width, Height); } }

        public new int X
        {
            get { return (int)Pos.X; }

            set 
            { 
                Pos = new Vector2(value, Pos.Y);
                CalculateVertices();
            }
        }


        public new int Y
        {
            get { return (int)Pos.Y; }

            set 
            { 
                Pos = new Vector2(Pos.X, value);
                CalculateVertices();
            }
        }

        public new int Width 
        {
            get { return base.Width; }

            set
            {
                base.Width = value;
                CalculateVertices();
            }
        }

        public new int Height
        {
            get { return base.Height; }

            set
            {
                base.Height = value;
                CalculateVertices();
            }
        }

        #endregion
        private void CalculateVertices()
        {
            _vertices[0] = new Vector2(Left, Top);
            _vertices[1] = new Vector2(Right, Top);
            _vertices[2] = new Vector2(Right, Bottom);
            _vertices[3] = new Vector2(Left, Bottom);
        }

    }
}