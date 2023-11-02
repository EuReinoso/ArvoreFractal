using MgEngine.Shape;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArvoreFractal.Scripts
{
    public class Stick : Line
    {
        public Color color;

        public Stick(Vector2 p1, Vector2 p2, int width = 1) : base(p1, p2, width)
        {

        }
    }
}
