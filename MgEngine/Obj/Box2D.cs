using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Interface;

namespace MgEngine.Obj
{
    public class Box2D : IBox2D
    {
        public Vector2 Pos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Rotation { get; set; }

        public Box2D()
        {
        }
        public Box2D(int x, int y) 
        {
            X = x;
            Y = y;
        }

        public Box2D(int x, int y,  int width, int height)
        {
            X = x;
            Y = y;
            Width = width; 
            Height = height;
        }


        public int X
        {
            get { return (int)Pos.X; }

            set { Pos = new Vector2(value, Pos.Y); }
        }

        public int Y 
        {
            get { return (int)Pos.Y; }

            set { Pos = new Vector2(Pos.X, value); }
        }

        public Vector2 Center 
        { 
            get { return new Vector2((Width / 2), (Height / 2)); }
        }

        
    }

    
}
