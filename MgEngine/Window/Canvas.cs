using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace MgEngine.Window
{
    public class Canvas
    {
        private GraphicsDevice _graphicsDevice;
        private RenderTarget2D _renderTarget;
        private Rectangle _destinationRectangle;
        private SpriteBatch _spriteBatch;

        public Canvas(GraphicsDevice graphicsDevice, int width, int height)
        {
            _graphicsDevice = graphicsDevice;
            _renderTarget = new(_graphicsDevice, width, height);
            _spriteBatch = new(_graphicsDevice);
        }

        public int Width { get { return _renderTarget.Width; } }
        
        public int Height { get { return _renderTarget.Height; } }
        public Vector2 Center { get{ return new Vector2(_renderTarget.Width / 2, _renderTarget.Height / 2); } }

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }

        public RenderTarget2D RenderTarget { get { return _renderTarget; } }

        public void SetResolution(int width, int height)
        {
            _renderTarget = new(_graphicsDevice, width, height);
            SetDestinationRectangle();
        }

        public void SetDestinationRectangle()
        {
            var windowSize = _graphicsDevice.PresentationParameters.Bounds;

            float scaleWidth = (float)windowSize.Width / _renderTarget.Width;
            float scaleHeight = (float)windowSize.Height / _renderTarget.Height;
            float scale = Math.Min(scaleWidth, scaleHeight);

            int newWidth = (int)(_renderTarget.Width * scale);
            int newHeight = (int)(_renderTarget.Height * scale);

            int x = (windowSize.Width - newWidth) / 2;
            int y = (windowSize.Height - newHeight) / 2;

            _destinationRectangle = new Rectangle(x, y, newWidth, newHeight);
        }

        public void Activate()
        {
            _graphicsDevice.SetRenderTarget(_renderTarget);
            _graphicsDevice.Clear(new Color(5, 5, 5));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _graphicsDevice.SetRenderTarget(null);
            _graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.BackToFront);
            spriteBatch.Draw(_renderTarget, _destinationRectangle, Color.White);
            spriteBatch.End();

        }

        public void Clear(Color color)
        {
            _graphicsDevice.Clear(color);
        }

    }
}
