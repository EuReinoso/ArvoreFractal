using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ArvoreFractal.Scripts;

using static MgEngine.Util.MgMath;
using MgEngine.Window;
using MgEngine.Font;
using MgEngine.Time;
using MgEngine.Input;
using MgEngine.Sprites;

namespace ArvoreFractal
{
    public class ArvoreFractal : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private WindowManager _window;
        private FontManager _font;
        private Clock _clock;
        private Tree tree;
        private InputManager _key = new();
        private SpritesManager _sprites;
        public ArvoreFractal()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _clock = new(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_window = new(_graphics, 720, 1000);
            //_window.SetResolution(720, 1000);

            _window = new(_graphics, 1080, 720);
            _window.SetResolution(1080, 720);

            _sprites = new(GraphicsDevice, _window);

            _font = new(Content, "Font/");
            _font.AddFont("Default");
            _font.AddFont("Default20");
            _font.SetDefaultFont("Default");

            _clock.IsFpsLimited = false;
            //_clock.FpsLimit = 60;

            tree = new(_sprites);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _key.Update(Keyboard.GetState());
            _clock.Update(gameTime);

            // TODO: Add your update logic here


            tree.Update(_clock.Dt, _key);
            tree.PlantTree(new Vector2(_window.Center.X, (int)(_window.Height * 0.8)), ToRadians(-90) , 120);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _window.Canvas.Activate();
            _spriteBatch.Begin();

            // TODO: Add your drawing code here

            tree.Draw();

            tree.DrawInfo(_spriteBatch, _font);
            _font.DrawText(_spriteBatch, "FPS: " + _clock.Fps.ToString(), new(20, 10), Color.White);

            _spriteBatch.End();
            _window.Canvas.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}