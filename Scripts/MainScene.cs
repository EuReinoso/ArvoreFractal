using MgEngine.Font;
using MgEngine.Input;
using MgEngine.Scene;
using MgEngine.Screen;
using MgEngine.Shape;
using MgEngine.Time;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MgEngine.Util.MgMath;

namespace ArvoreFractal.Scripts
{
    public class MainScene : Scene
    {
        private Tree _tree;
        private Font _font;

        public MainScene(Window window, Camera camera) : base(window, camera)
        {
        }

        public override void Initialize()
        {
            _tree = new();
        }

        public override void LoadContent(ContentManager content)
        {
            _font = new(content, "Font/Default", new() { 20 });
        }

        public override void Update(float dt, Inputter inputter)
        {
            _tree.Update(dt, inputter);
            _tree.PlantTree(new Vector2(Window.Center.X, (int)(Window.Height * 0.8)), ToRadians(-90), 120);
        }

        public override void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            _tree.Draw(shapeBatch);
            _tree.DrawInfo(spriteBatch, _font);
        }
    }
}
