using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Obj;
using System.Threading;
using System;

namespace MgEngine.Shape
{
    public class Rect : Box2D
    {    
        private Texture2D _texture;

        private Color _color;

        public Rect(int x, int y, int width, int height) : base(x, y, width, height)
        {

        }

        public Rectangle Rectangle 
        {
            get { return new Rectangle(X, Y, Width, Height); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {   
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Pos, Rectangle, _color, Rotation, Center, 1, SpriteEffects.None, 1);
            }
            else
            {
                throw new Exception("Attempt to Draw an Rect Without Texture, call Load first.");
            }
        }

        public void SetColor(GraphicsDevice graphicsDevice, Color color)
        {
            try
            {
                _color = color;

                _texture = new Texture2D(graphicsDevice, Width, Height);

                Color[] textureData = new Color[Width * Height];
                for (int i = 0; i < textureData.Length; i++)
                {
                    textureData[i] = color;
                }

                _texture.SetData(textureData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Load(GraphicsDevice graphicsDevice, Color color)
        {
            SetColor(graphicsDevice, color);
        }
    }
}