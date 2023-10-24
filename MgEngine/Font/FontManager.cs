using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MgEngine.Font
{
    public class FontManager
    {
        private string _path;
        private ContentManager _content;
        private Dictionary<string, SpriteFont> _fonts;
        private string _defaultFontName;

        public FontManager(ContentManager content, string path)
        {
            _content = content;
            _path = path;
            _fonts = new();
        }

        public void AddFont(string fontName)
        {
            _fonts.Add(fontName, LoadFont(fontName));
        }

        public SpriteFont LoadFont(string name)
        {
            return _content.Load<SpriteFont>(_path + name);
        }

        public void DrawText(SpriteBatch spriteBatch, string text, Vector2 Pos, Color color, string fontName = null, float rotation = 0)
        {
            if (_fonts.Count <= 0 )
            {
                throw new Exception("Attempting to Draw an font without AddFont!");
            }

            if (fontName == null)
            {
                if (_defaultFontName != null)
                {
                    fontName = _defaultFontName;
                }
                else
                {
                    throw new Exception("Attempting to Draw an font without SetDefaultFont!");
                }
            }

            spriteBatch.DrawString(_fonts[fontName], text, Pos, color , rotation, new Vector2(0, 0), 1, SpriteEffects.None, 1);
        }

        public void SetDefaultFont(string fontName)
        {
            if (!_fonts.ContainsKey(fontName))
            {
                throw new Exception("Attempting to SetDefaultFont, this font does not have been added!");
            }

            _defaultFontName = fontName;
        }

    }
}
