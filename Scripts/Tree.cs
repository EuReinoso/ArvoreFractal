using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MgEngine.Font;
using System;
using static MgEngine.Util.MgMath;

namespace ArvoreFractal.Scripts
{
    public class Tree
    {
        private int _maxLayer;
        private float _stickAngle;
        private int _stickBranches;
        private float _stickAngleDecay;
        private float _stickLengthDecay;

        private GraphicsDevice _graphicsDevice;

        private List<Stick> _sticks;

        public Tree(GraphicsDevice graphicsDevice, int maxLayer, float stickLengthDecay, float stickAngle, float stickAngleDecay, int stickBranches) 
        { 
            _graphicsDevice = graphicsDevice;
            _maxLayer = maxLayer;
            _stickLengthDecay = stickLengthDecay;
            _stickAngle = stickAngle;
            _stickAngleDecay = stickAngleDecay;
            _stickBranches = stickBranches;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Stick stick in _sticks)
            {
                stick.Draw(spriteBatch);
            }
        }

        public void PlantTree(Vector2 startPoint, float startAngle, int startLenght)
        {
            _sticks = new();
            Build(startPoint, startAngle, startLenght, 0);
        }

        private void Build(Vector2 p1, float angle, int length, int layer)
        {
            if (layer >= _maxLayer || length <= 1)
            {
                return;
            }
            
            float x2 = p1.X + (int)(Math.Cos(angle) * length);
            float y2 = p1.Y + (int)(Math.Sin(angle) * length);

            Stick newStick = new(p1, new Vector2(x2, y2));
            newStick.Load(_graphicsDevice, Color.White);
            AddStick(newStick);

            float startAngle = angle - (_stickBranches - 1) * _stickAngle;

            layer += 1;
            for (int i = 0; i < _stickBranches; i++)
            {
                float newAngle = startAngle + i * _stickAngle * 2;

                Build(newStick.P2, newAngle, (int)(length * _stickLengthDecay), layer);
            }
        }

        public void DrawInfo(SpriteBatch spriteBatch, FontManager fonts)
        {
            string text = "Camadas: " + _maxLayer.ToString()
                        + "\n" + "Galhos: " + _sticks.Count.ToString()
                        + "\n" + "Angulo: " + ToDegrees(_stickAngle).ToString();
            fonts.DrawText(spriteBatch, text, new(10, 30), Color.White);
        }

        private void AddStick(Stick stick)
        {
            _sticks.Add(stick);
        }

        public void Clean()
        {
            _sticks.Clear();
        }

    }
}
