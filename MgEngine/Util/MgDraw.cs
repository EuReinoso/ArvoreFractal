using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MgEngine.Shape;

namespace MgEngine.Util
{
    public static class MgDraw
    {

        public static void DrawRect(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Rect rect, Color color, float rotation = 0)
        {
            rect.Rotation = rotation;

            rect.Load(graphicsDevice, color);

            rect.Draw(spriteBatch);
        }

        public static void DrawLine(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Line line, Color color)
        {
            line.Load(graphicsDevice, color);

            line.Draw(spriteBatch);
        }
    }
}
