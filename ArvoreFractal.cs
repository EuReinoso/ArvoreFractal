using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MgEngine.Window;
using MgEngine.Font;
using MgEngine.Time;
using static MgEngine.Util.MgMath;
using ArvoreFractal.Scripts;


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
            _window = new(_graphics, 720, 1000);
            _window.SetResolution(720, 1000);

            _font = new(Content, "Font/");
            _font.AddFont("Default");
            _font.SetDefaultFont("Default");

            _clock.IsFpsLimited = false;
            //_clock.FpsLimit = 60;

            tree = new(GraphicsDevice, 12, 0.8f, ToRadians(22), ToRadians(0), 2);

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

            // TODO: Add your update logic here
            _clock.Update(gameTime);

            tree.PlantTree(new Vector2(360, 800), ToRadians(-90) , 120);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _window.Canvas.Activate();
            _spriteBatch.Begin();

            // TODO: Add your drawing code here

            tree.Draw(_spriteBatch);

            tree.DrawInfo(_spriteBatch, _font);
            _font.DrawText(_spriteBatch, "FPS: " + _clock.Fps.ToString(), new(10, 10), Color.White);

            _spriteBatch.End();
            _window.Canvas.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}