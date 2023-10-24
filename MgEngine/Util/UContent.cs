using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MgEngine.Util
{
    public static class UContent
    {
        public static Texture2D GetTexture2D(ContentManager content, string path)
        {
            try
            {
                return content.Load<Texture2D>(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
