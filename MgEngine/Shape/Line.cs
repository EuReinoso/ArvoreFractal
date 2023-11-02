using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace MgEngine.Shape
{
    public class Line
    {
        private bool _initialized;

        private Vector2 _p1;
        private Vector2 _p2;
        public int Width;
        
        private Vector2[] _vertices;

        #region Properties
        public Vector2 P1 
        {
            get { return _p1;}

            set 
            { 
                _p1 = value;
                CalculateVertices();
            }
        }

        public Vector2 P2
        {
            get { return _p2; }

            set
            {
                _p2 = value;
                CalculateVertices();
            }
        }

        public int Length
        {
            get { return (int)Vector2.Distance(_p1, _p2); }
        }

        public Vector2[] Vertices { get { return _vertices; } }

        #endregion


        public Line(Vector2 p1, Vector2 p2, int width = 1)
        {
            _p1 = p1;
            _p2 = p2;
            Width = width;
            _initialized = true;

            _vertices = new Vector2[4];
            CalculateVertices();
        }

        public Line(int p1x, int p1y, int p2x, int p2y, int width = 1)
        {
            _p1 = new Vector2(p1x, p1y);
            _p2 = new Vector2(p2x, p2y);
            Width = width;
            _initialized = true;
            CalculateVertices();
        }

        private void CalculateVertices()
        {
            if (!_initialized)
            {
                return;
            }

            float halfWidth = Width / 2f;

            Vector2 e1 = _p1 - _p2;
            e1.Normalize();
            e1 *= halfWidth;

            Vector2 e2 = -e1;
            Vector2 n1 = new Vector2(-e1.Y, e1.X);
            Vector2 n2 = -n1;

            _vertices[0] = _p1 + n1 + e2;
            _vertices[1] = _p2 + n1 + e1;
            _vertices[2] = _p2 + n2 + e1;
            _vertices[3] = _p1 + n2 + e2;
        }

    }
}