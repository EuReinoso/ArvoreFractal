using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using MgEngine.Font;
using System;
using static MgEngine.Util.MgMath;
using Microsoft.Xna.Framework.Input;
using MgEngine.Time;
using ArvoreFractal.MgEngine.Input;
using MgEngine.Sprites;

namespace ArvoreFractal.Scripts
{
    public class Tree
    {
        private SpritesManager _sprites;

        public int maxLayer;
        public int stickBranches;
        public float stickLengthDecay;
        public float stickAngle;
        public float stickAngleDecay;
        public int stickWidth;
        public int stickWidthDecay;

        private bool _replant;

        private List<Stick> _sticks;

        private int _colorMode;
        private Color[] RAINBOWCOLORS;

    public Tree(SpritesManager sprites) 
        {
            _sprites = sprites;

            _replant = true;
            _sticks = new();

            maxLayer = 3;
            stickLengthDecay = 0.8f;
            stickAngle = ToRadians(30);
            stickAngleDecay = ToRadians(0);
            stickBranches = 2;
            stickWidth = 1;
            stickWidthDecay = 1;

            _colorMode = 1;

            RAINBOWCOLORS =  new Color[]
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Indigo,
                Color.Violet
            };
        }

        public void Draw()
        {
            foreach (Stick stick in _sticks)
            {
                Color color = new Color(new Random().Next(256), new Random().Next(256), new Random().Next(256));
                _sprites.DrawLine(stick, stick.color);
            }
        }

        public void Update(float dt, InputManager key)
        {
            UpdateInput(dt, key);
        }

        private void UpdateInput(float dt, InputManager key)
        {
            if (key.KeyDown(Keys.C))
            {
                if (_colorMode + 1 <= 4)
                {
                    _colorMode += 1;
                }
                else
                {
                    _colorMode = 1;
                }

                _replant = true;
            }

            if (key.IsKeyDown(Keys.Up))
            {
                stickLengthDecay += 0.01f;
                _replant = true;
            }
            
            if (key.IsKeyDown(Keys.Down))
            {
                stickLengthDecay -= 0.01f;
                _replant = true;
            }

            if (key.IsKeyDown(Keys.LeftShift) && key.IsKeyDown(Keys.Left))
            {
                stickAngleDecay -= ToRadians(1);
                _replant = true;
            }
            else if (key.IsKeyDown(Keys.Left))
            {
                stickAngle -= ToRadians(1);
                _replant = true;
            }

            if (key.IsKeyDown(Keys.LeftShift) && key.IsKeyDown(Keys.Right))
            {
                stickAngleDecay += ToRadians(1);
                _replant = true;
            }
            else if (key.IsKeyDown(Keys.Right))
            {
                stickAngle += ToRadians(1);
                _replant = true;
            }
            
            if (key.KeyDown(Keys.W))
            {
                maxLayer += 1;
                _replant = true;
            }
            
            if (key.KeyUp(Keys.S) && maxLayer > 0)
            {
                maxLayer -= 1;
                _replant = true;
            }

            if (key.IsKeyDown(Keys.LeftShift) && key.KeyUp(Keys.A))
            {
                if (stickWidth > 1)
                {
                    stickWidth -= 1;
                    _replant = true;
                }
            }
            else if (key.KeyUp(Keys.A) && stickBranches > 0)
            {
                stickBranches -= 1;
                _replant = true;
            }

            if (key.IsKeyDown(Keys.LeftShift) && key.KeyUp(Keys.D))
            {
                stickWidth += 1;
                _replant = true;
            }
            else if (key.KeyUp(Keys.D))
            {
                stickBranches += 1;
                _replant = true;
            }


        }

        public void PlantTree(Vector2 startPoint, float startAngle, int startLenght)
        {
            if (_replant)
            {
                _sticks = new();
                Build(startPoint, startAngle, startLenght, 0, stickWidth);
                _replant = false;
            }
        }

        private void Build(Vector2 p1, float angle, int length, int layer, int width)
        {
            if (layer >= maxLayer || length <= 1)
            {
                return;
            }

            int newWidth;

            if (width - stickWidthDecay > 0)
            {
                newWidth = width - stickWidthDecay;
            }
            else
            {
                newWidth = width;
            }

            float x2 = p1.X + (int)(Math.Cos(angle) * length);
            float y2 = p1.Y + (int)(Math.Sin(angle) * length);

            Stick newStick = new(p1, new Vector2(x2, y2), newWidth);

            if (_colorMode == 1)
            {
                newStick.color = Color.White;
            }
            else if (_colorMode == 2)
            {
                newStick.color = RAINBOWCOLORS[layer % RAINBOWCOLORS.Length];
            }
            else if (_colorMode == 3)
            {
                newStick.color = RAINBOWCOLORS[(int)Math.Abs(angle) % RAINBOWCOLORS.Length];
            }
            else if (_colorMode == 4)
            {
                if (length < 10 || layer + 1 >= maxLayer)
                {
                    newStick.color = Color.Green;
                }
                else
                {
                    newStick.color = new Color(229, 170, 112);
                }
            }
            
            AddStick(newStick);

            float startAngle = angle - (stickBranches - 1) * stickAngle;

            layer += 1;
            for (int i = 0; i < stickBranches; i++)
            {
                //float newAngle = startAngle + i  * stickAngle * 2;
                float newAngle = startAngle + i * stickAngle * 2;
                newAngle += stickAngleDecay;

                Build(newStick.P2, newAngle, (int)(length * stickLengthDecay), layer, newWidth);
            }
        }

        public void DrawInfo(SpriteBatch spriteBatch, FontManager fonts)
        {
            int degrees = (int)ToDegrees(stickAngle);
            string angle = degrees.ToString();

            degrees = (int)ToDegrees(stickAngleDecay);
            string angleDecay = degrees.ToString();

            string text = "Camadas: " + maxLayer.ToString()
                        + "\n" + "Galhos: " + _sticks.Count.ToString()
                        + "\n" + "Proporcao de Tamanho: " + stickLengthDecay.ToString("F2")
                        + "\n" + "Angulo: " + angle
                        + "\n" + "Ganho de Angulo: " + angleDecay
                        + "\n" + "Grossura: " + stickWidth.ToString();
            fonts.DrawText(spriteBatch, text, new(20, 30), Color.White);

            //fonts.DrawText(spriteBatch, _stickLengthDecay.ToString("F2"), new(360- 30, 500 + 20), Color.Red, "Default20");
            //fonts.DrawText(spriteBatch, _maxLayer.ToString(), new(360 - 15, 500 + 45), Color.Green, "Default20");
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
