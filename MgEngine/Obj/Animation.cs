using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MgEngine.Obj
{
    public class Animation
    {
        private List<Rectangle> _animation = new();
        private List<int> _frameTimeList;

        private int _currentFrameIndex;
        private int _currentFrameTime;


        public Animation(int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            _frameTimeList = frameTimeList;

            for (int i = 0; i < frameTimeList.Count(); i++)
            {
                _animation.Add(new Rectangle(i * frameWidth, (row - 1) * frameHeight, frameWidth, frameHeight));

            }
        }

        public void Update(float dt)
        {
            if (_currentFrameTime >= _frameTimeList[_currentFrameIndex])
            {
                _currentFrameIndex += (int)Math.Round(1  *  dt);

                if (_currentFrameIndex > _frameTimeList.Count() - 1)
                {
                    Reset();
                }
            }

            _currentFrameTime++;
        }

        public void Reset()
        {
            _currentFrameIndex = 0;
            _currentFrameTime = 0;
        }

        public Rectangle CurrentFrame
        {
            get { return _animation[_currentFrameIndex]; }
        }
    }
}
