using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MgEngine.Shape
{
    public class Line
    {
        public Vector2 P1;

        public Vector2 P2;

        public int Width;

        private Texture2D _texture;

        private Color _color;


        public Line(Vector2 p1, Vector2 p2, int width = 1)
        {
            P1 = p1;
            P2 = p2;
            Width = width;
        }

        public int Length
        {
            get { return (int)Vector2.Distance(P1, P2); }
        }

        public Line(int p1x, int p1y, int p2x, int p2y, int width = 1)
        {
            P1 = new Vector2(p1x, p1y);
            P2 = new Vector2(p2x, p2y);
            Width = width;
        }
        private void SetColor(GraphicsDevice graphicsDevice, Color color)
        {
            _color = color;

            _texture = new Texture2D(graphicsDevice, Width, Length);

            Color[] textureData = new Color[Width * Length];
            for (int i = 0; i < textureData.Length; i++)
            {
                textureData[i] = color;
            }

            _texture.SetData(textureData);
        }

        public void Load(GraphicsDevice graphicsDevice, Color color)
        {
            SetColor(graphicsDevice, color);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //float rotation = -(float)Math.Atan2(P2.Y - P1.Y, P2.X - P1.X);
            float rotation = -(float)Math.Atan2(P2.X - P1.X, P2.Y - P1.Y);

            Vector2 origin = new Vector2(0, Width / 2);

            spriteBatch.Draw(_texture, P1, null, _color, rotation, origin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}