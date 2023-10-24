using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace MgEngine.Obj
{
    public class AnimationManager
    {
        private Dictionary<object, Animation> _animations;
        private object _currentAnimationKey;

        public AnimationManager()
        {
            _animations = new();
        }

        public void AddAnimation(object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {   
            if (_animations.ContainsKey(actionKey))
            {
                throw new Exception($"ActionKey `{actionKey}` already exists at this Obj!");
            }

            if(_animations.Count <= 0)
            {
                _currentAnimationKey = actionKey;
            }

            _animations.Add(actionKey, new Animation(frameWidth, frameHeight, frameTimeList, row));
        }

        public void Update(float dt)
        {
            _animations[_currentAnimationKey].Update(dt);
        }

        public void SetAnimation(object actionKey)
        {
            if (_animations.ContainsKey(actionKey))
            {
                _animations[_currentAnimationKey].Reset();
                _currentAnimationKey = actionKey;
            }
            else
            {
                Console.WriteLine($"Animation Key {actionKey} does not exists!");
            }
        }

        public Animation CurrentAnimation
        {
            get{ return _animations[_currentAnimationKey]; }
        }
    }
}
