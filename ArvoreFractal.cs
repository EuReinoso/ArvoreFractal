using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ArvoreFractal.Scripts;

using static MgEngine.Util.MgMath;
using MgEngine.Font;
using MgEngine.Time;
using MgEngine.Input;
using MgEngine.Screen;
using MgEngine.Shape;

namespace ArvoreFractal
{
    public class ArvoreFractal : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Window _window;
        private Font _font;
        private Clock _clock;
        private Inputter _inputter;
        private MainScene _scene;
        private Camera _camera;
        private ShapeBatch _shapeBatch;

        public ArvoreFractal()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _clock = new(this);
            _inputter = new();

        }

        protected override void Initialize()
        {
            _spriteBatch = new(GraphicsDevice);

            _shapeBatch = new(GraphicsDevice);

            _window = new(_graphics, _spriteBatch, 1080, 720);
            _window.SetResolution(1080, 720);
            _window.SetBackGroundColor(Color.Black);

            _font = new(Content, "Font/Default", new() { 20});

            _clock.IsFpsLimited = false;
            //_clock.FpsLimit = 60;

            _scene = new(_window, _camera);
            _scene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _scene.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputter.Update(Keyboard.GetState(), Mouse.GetState());

            _clock.Update(gameTime);

            _scene.Update(_clock.Dt, _inputter);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _window.Begin();
            _shapeBatch.Begin();

            _scene.Draw(_spriteBatch, _shapeBatch);

            _font.DrawText(_spriteBatch, "FPS: " + _clock.Fps.ToString(), new(20, 10), 20, Color.White);

            _window.End();

            base.Draw(gameTime);
        }
    }
}