using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Window
{
    public class WindowManager
    {
        private GraphicsDeviceManager _graphics;
        private Canvas _canvas;

        public WindowManager(GraphicsDeviceManager graphics, int canvasWidth = 1280, int canvasHeight = 720)
        {
            _graphics = graphics;
            _canvas = new(_graphics.GraphicsDevice, canvasWidth, canvasHeight);
            _canvas.SetDestinationRectangle();
        }

        public void SetResolution(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();

            _canvas.SetDestinationRectangle();
        }

        public void ToggleFullScreen()
        {
            _graphics.ToggleFullScreen();
            _canvas.SetDestinationRectangle();
        }

        public Canvas Canvas { get { return _canvas; } }
    }

}
