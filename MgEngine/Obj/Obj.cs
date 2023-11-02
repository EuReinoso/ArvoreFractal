using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

using MgEngine.Sprites;
using MgEngine.Shape;

namespace MgEngine.Obj
{
    public class Obj : Box2D
    {
        private Texture2D _currentTexture;
        private Rectangle _currentFrame;
        private AnimationManager _animations;
        private Dictionary<object, Texture2D> _textures;
        private Surface _surface;
        private SpritesManager _sprites;

        public Obj(SpritesManager sprites, bool animated = false)
        {
            _sprites = sprites;
            _surface = new Surface(sprites.GraphicsDevice);
            
            if (animated)
            {
                InitAnimation();
            }
        }

        public Rect Rect
        {
            get { return new Rect(X, Y, Width, Height); }
        }
            
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentTexture == null)
            {
                throw new Exception("Attempting to Draw an Obj without Texture");
            }

            RenderTarget2D renderTarget = _surface.DrawTextureOnSurface(_currentTexture, new Rectangle(0, 0, Width, Height), _currentFrame);

            _sprites.SetMainRenderTarget();

            spriteBatch.Draw(renderTarget, Pos, new Rectangle(0, 0, Width, Height), Color.White, Rotation, Center, 1, SpriteEffects.None, 1);

            //ToDo: Fazer esse codigo funcionar ao usar resizing
            //spriteBatch.Draw(_currentTexture, Rect.Rectangle, _currentFrame, Color.White, Rotation, Center, SpriteEffects.None, 1);
        }

        public void DrawRect(Color color)
        {
           _sprites.DrawRect(Rect, color);
        }

        #region Animation
        public void LoadTexture(ContentManager content, string path)
        {
            try
            {
                _currentTexture = content.Load<Texture2D>(path);
                _currentFrame = new Rectangle(0, 0, _currentTexture.Width, _currentTexture.Height);
                Width = _currentFrame.Width;
                Height = _currentFrame.Height;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InitAnimation()
        {
            _animations = new();
            _textures = new();
        }


        public void AddAnimation(Texture2D texture, object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            try
            {
                _textures.Add(actionKey, texture);
                _animations.AddAnimation(actionKey, frameWidth, frameHeight, frameTimeList, row);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetAction(object actionKey)
        {
            _currentTexture = _textures[actionKey];
            _animations.SetAnimation(actionKey);
            _currentFrame = _animations.CurrentAnimation.CurrentFrame;
            Width = _currentFrame.Width;
            Height = _currentFrame.Height;
        }

        public void Animate(float dt)
        {
            if (_animations != null)
            {
                try
                {
                    _animations.Update(dt);
                    _currentFrame = _animations.CurrentAnimation.CurrentFrame;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                    
                }
            }
            else
            {
                throw new Exception("Attempt to Animate without InitAnimation!");
            }
        }
        #endregion

    }
}
